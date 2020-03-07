<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmWeeklyDiaryDE
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmWeeklyDiaryDE))
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle16 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle17 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle15 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle18 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle21 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle22 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle19 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle20 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.PanelEx1 = New DevComponents.DotNetBar.PanelEx()
        Me.SuperTabControl1 = New DevComponents.DotNetBar.SuperTabControl()
        Me.SuperTabControlPanel1 = New DevComponents.DotNetBar.SuperTabControlPanel()
        Me.PanelEx4 = New DevComponents.DotNetBar.PanelEx()
        Me.btnPrintWD = New DevComponents.DotNetBar.ButtonX()
        Me.btnSaveWD = New DevComponents.DotNetBar.ButtonX()
        Me.lblSelectedDate = New DevComponents.DotNetBar.LabelX()
        Me.LabelX10 = New DevComponents.DotNetBar.LabelX()
        Me.btnGenerateWD = New DevComponents.DotNetBar.ButtonX()
        Me.MonthCalendarAdv1 = New DevComponents.Editors.DateTimeAdv.MonthCalendarAdv()
        Me.dgvWeeklyDiary = New DevComponents.DotNetBar.Controls.DataGridViewX()
        Me.IDDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DiaryDateDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.WorkDoneDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RemarksDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.WeeklyDiaryBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.WeeklyDiaryDataSet1 = New FingerprintInformationSystem.WeeklyDiaryDataSet()
        Me.tabWD = New DevComponents.DotNetBar.SuperTabItem()
        Me.SuperTabControlPanel3 = New DevComponents.DotNetBar.SuperTabControlPanel()
        Me.PanelEx3 = New DevComponents.DotNetBar.PanelEx()
        Me.LabelX9 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX8 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX37 = New DevComponents.DotNetBar.LabelX()
        Me.btnSaveOfficeDetails = New DevComponents.DotNetBar.ButtonX()
        Me.LabelX7 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX6 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX5 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX4 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX3 = New DevComponents.DotNetBar.LabelX()
        Me.txtODRemarks = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.dtTo = New DevComponents.Editors.DateTimeAdv.DateTimeInput()
        Me.dtFrom = New DevComponents.Editors.DateTimeAdv.DateTimeInput()
        Me.txtDesignation = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.txtUnit = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.dgvOfficeDetails = New DevComponents.DotNetBar.Controls.DataGridViewX()
        Me.UnitDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FromDateDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ToDateDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DesignationDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RemarksDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OfficeDetailsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.tabOD = New DevComponents.DotNetBar.SuperTabItem()
        Me.SuperTabControlPanel2 = New DevComponents.DotNetBar.SuperTabControlPanel()
        Me.PanelEx2 = New DevComponents.DotNetBar.PanelEx()
        Me.txtOldPassword = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.lblOldPassword = New DevComponents.DotNetBar.LabelX()
        Me.lblPEN = New DevComponents.DotNetBar.LabelX()
        Me.btnCancelPassword = New DevComponents.DotNetBar.ButtonX()
        Me.btnSavePassword = New DevComponents.DotNetBar.ButtonX()
        Me.btnCancelName = New DevComponents.DotNetBar.ButtonX()
        Me.btnSaveName = New DevComponents.DotNetBar.ButtonX()
        Me.lblChangeName = New DevComponents.DotNetBar.LabelX()
        Me.txtName = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.txtPassword2 = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.txtPassword1 = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.lblPassword2 = New DevComponents.DotNetBar.LabelX()
        Me.lblPassword1 = New DevComponents.DotNetBar.LabelX()
        Me.lblChangePassword = New DevComponents.DotNetBar.LabelX()
        Me.LabelX2 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX1 = New DevComponents.DotNetBar.LabelX()
        Me.tabPass = New DevComponents.DotNetBar.SuperTabItem()
        Me.RibbonBar1 = New DevComponents.DotNetBar.RibbonBar()
        Me.btnNewEntry = New DevComponents.DotNetBar.ButtonItem()
        Me.btnEdit = New DevComponents.DotNetBar.ButtonItem()
        Me.btnDelete = New DevComponents.DotNetBar.ButtonItem()
        Me.btnReload = New DevComponents.DotNetBar.ButtonItem()
        Me.btnOpenFolder = New DevComponents.DotNetBar.ButtonItem()
        Me.btnPrintAll = New DevComponents.DotNetBar.ButtonItem()
        Me.btnCL = New DevComponents.DotNetBar.ButtonItem()
        Me.btnOnlineBackup = New DevComponents.DotNetBar.ButtonItem()
        Me.btnRestore = New DevComponents.DotNetBar.ButtonItem()
        Me.CircularProgress1 = New DevComponents.DotNetBar.CircularProgressItem()
        Me.bgwUpload = New System.ComponentModel.BackgroundWorker()
        Me.SocRegisterTableAdapter1 = New FingerprintInformationSystem.FingerPrintDataSetTableAdapters.SOCRegisterTableAdapter()
        Me.FingerPrintDataSet1 = New FingerprintInformationSystem.FingerPrintDataSet()
        Me.AuthenticationTableAdapter1 = New FingerprintInformationSystem.WeeklyDiaryDataSetTableAdapters.AuthenticationTableAdapter()
        Me.WeeklyDiaryTableAdapter1 = New FingerprintInformationSystem.WeeklyDiaryDataSetTableAdapters.WeeklyDiaryTableAdapter()
        Me.PersonalDetailsTableAdapter1 = New FingerprintInformationSystem.WeeklyDiaryDataSetTableAdapters.PersonalDetailsTableAdapter()
        Me.OfficeDetailsTableAdapter1 = New FingerprintInformationSystem.WeeklyDiaryDataSetTableAdapters.OfficeDetailsTableAdapter()
        Me.bgwDownload = New System.ComponentModel.BackgroundWorker()
        Me.bgwAutoUpload = New System.ComponentModel.BackgroundWorker()
        Me.PanelEx1.SuspendLayout()
        CType(Me.SuperTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuperTabControl1.SuspendLayout()
        Me.SuperTabControlPanel1.SuspendLayout()
        Me.PanelEx4.SuspendLayout()
        CType(Me.dgvWeeklyDiary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.WeeklyDiaryBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.WeeklyDiaryDataSet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuperTabControlPanel3.SuspendLayout()
        Me.PanelEx3.SuspendLayout()
        CType(Me.dtTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvOfficeDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OfficeDetailsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuperTabControlPanel2.SuspendLayout()
        Me.PanelEx2.SuspendLayout()
        CType(Me.FingerPrintDataSet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelEx1
        '
        Me.PanelEx1.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.PanelEx1.Controls.Add(Me.SuperTabControl1)
        Me.PanelEx1.Controls.Add(Me.RibbonBar1)
        Me.PanelEx1.DisabledBackColor = System.Drawing.Color.Empty
        Me.PanelEx1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelEx1.Location = New System.Drawing.Point(0, 0)
        Me.PanelEx1.Name = "PanelEx1"
        Me.PanelEx1.Size = New System.Drawing.Size(988, 576)
        Me.PanelEx1.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx1.Style.GradientAngle = 90
        Me.PanelEx1.TabIndex = 0
        '
        'SuperTabControl1
        '
        Me.SuperTabControl1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        '
        '
        '
        '
        '
        '
        Me.SuperTabControl1.ControlBox.CloseBox.Name = ""
        '
        '
        '
        Me.SuperTabControl1.ControlBox.MenuBox.Name = ""
        Me.SuperTabControl1.ControlBox.Name = ""
        Me.SuperTabControl1.ControlBox.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.SuperTabControl1.ControlBox.MenuBox, Me.SuperTabControl1.ControlBox.CloseBox})
        Me.SuperTabControl1.Controls.Add(Me.SuperTabControlPanel1)
        Me.SuperTabControl1.Controls.Add(Me.SuperTabControlPanel3)
        Me.SuperTabControl1.Controls.Add(Me.SuperTabControlPanel2)
        Me.SuperTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SuperTabControl1.ForeColor = System.Drawing.Color.Black
        Me.SuperTabControl1.Location = New System.Drawing.Point(0, 65)
        Me.SuperTabControl1.Name = "SuperTabControl1"
        Me.SuperTabControl1.ReorderTabsEnabled = True
        Me.SuperTabControl1.SelectedTabFont = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.SuperTabControl1.SelectedTabIndex = 0
        Me.SuperTabControl1.Size = New System.Drawing.Size(988, 511)
        Me.SuperTabControl1.TabFont = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SuperTabControl1.TabIndex = 1
        Me.SuperTabControl1.Tabs.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.tabWD, Me.tabOD, Me.tabPass})
        Me.SuperTabControl1.Text = "SuperTabControl1"
        '
        'SuperTabControlPanel1
        '
        Me.SuperTabControlPanel1.Controls.Add(Me.PanelEx4)
        Me.SuperTabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SuperTabControlPanel1.Location = New System.Drawing.Point(0, 28)
        Me.SuperTabControlPanel1.Name = "SuperTabControlPanel1"
        Me.SuperTabControlPanel1.Size = New System.Drawing.Size(988, 483)
        Me.SuperTabControlPanel1.TabIndex = 1
        Me.SuperTabControlPanel1.TabItem = Me.tabWD
        '
        'PanelEx4
        '
        Me.PanelEx4.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.PanelEx4.Controls.Add(Me.btnPrintWD)
        Me.PanelEx4.Controls.Add(Me.btnSaveWD)
        Me.PanelEx4.Controls.Add(Me.lblSelectedDate)
        Me.PanelEx4.Controls.Add(Me.LabelX10)
        Me.PanelEx4.Controls.Add(Me.btnGenerateWD)
        Me.PanelEx4.Controls.Add(Me.MonthCalendarAdv1)
        Me.PanelEx4.Controls.Add(Me.dgvWeeklyDiary)
        Me.PanelEx4.DisabledBackColor = System.Drawing.Color.Empty
        Me.PanelEx4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelEx4.Location = New System.Drawing.Point(0, 0)
        Me.PanelEx4.Name = "PanelEx4"
        Me.PanelEx4.Size = New System.Drawing.Size(988, 483)
        Me.PanelEx4.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx4.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx4.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx4.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx4.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx4.Style.GradientAngle = 90
        Me.PanelEx4.TabIndex = 4
        '
        'btnPrintWD
        '
        Me.btnPrintWD.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnPrintWD.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnPrintWD.Image = CType(resources.GetObject("btnPrintWD.Image"), System.Drawing.Image)
        Me.btnPrintWD.Location = New System.Drawing.Point(430, 106)
        Me.btnPrintWD.Name = "btnPrintWD"
        Me.btnPrintWD.Size = New System.Drawing.Size(121, 43)
        Me.btnPrintWD.TabIndex = 2
        Me.btnPrintWD.Text = "Print in Word"
        '
        'btnSaveWD
        '
        Me.btnSaveWD.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnSaveWD.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnSaveWD.Image = CType(resources.GetObject("btnSaveWD.Image"), System.Drawing.Image)
        Me.btnSaveWD.Location = New System.Drawing.Point(430, 57)
        Me.btnSaveWD.Name = "btnSaveWD"
        Me.btnSaveWD.Size = New System.Drawing.Size(121, 43)
        Me.btnSaveWD.TabIndex = 1
        Me.btnSaveWD.Text = "Save Records"
        '
        'lblSelectedDate
        '
        Me.lblSelectedDate.AutoSize = True
        '
        '
        '
        Me.lblSelectedDate.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblSelectedDate.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSelectedDate.ForeColor = System.Drawing.Color.Red
        Me.lblSelectedDate.Location = New System.Drawing.Point(125, 23)
        Me.lblSelectedDate.Name = "lblSelectedDate"
        Me.lblSelectedDate.Size = New System.Drawing.Size(38, 24)
        Me.lblSelectedDate.TabIndex = 39
        Me.lblSelectedDate.Text = "Date"
        '
        'LabelX10
        '
        Me.LabelX10.AutoSize = True
        '
        '
        '
        Me.LabelX10.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX10.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX10.Location = New System.Drawing.Point(12, 25)
        Me.LabelX10.Name = "LabelX10"
        Me.LabelX10.Size = New System.Drawing.Size(107, 20)
        Me.LabelX10.TabIndex = 38
        Me.LabelX10.Text = "Week starting on"
        '
        'btnGenerateWD
        '
        Me.btnGenerateWD.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnGenerateWD.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnGenerateWD.Image = CType(resources.GetObject("btnGenerateWD.Image"), System.Drawing.Image)
        Me.btnGenerateWD.Location = New System.Drawing.Point(430, 8)
        Me.btnGenerateWD.Name = "btnGenerateWD"
        Me.btnGenerateWD.Size = New System.Drawing.Size(121, 43)
        Me.btnGenerateWD.TabIndex = 0
        Me.btnGenerateWD.Text = "Generate"
        '
        'MonthCalendarAdv1
        '
        Me.MonthCalendarAdv1.AnnuallyMarkedDates = New Date() {New Date(2020, 1, 2, 0, 0, 0, 0), New Date(2020, 1, 26, 0, 0, 0, 0), New Date(2020, 5, 1, 0, 0, 0, 0), New Date(2020, 8, 15, 0, 0, 0, 0), New Date(2020, 10, 2, 0, 0, 0, 0), New Date(2020, 12, 25, 0, 0, 0, 0)}
        Me.MonthCalendarAdv1.AutoSize = True
        '
        '
        '
        Me.MonthCalendarAdv1.BackgroundStyle.Class = "MonthCalendarAdv"
        Me.MonthCalendarAdv1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.MonthCalendarAdv1.Colors.AnnualMarker.TextColor = System.Drawing.Color.Red
        Me.MonthCalendarAdv1.Colors.DayMarker.TextColor = System.Drawing.Color.Red
        Me.MonthCalendarAdv1.Colors.MonthlyMarker.TextColor = System.Drawing.Color.Red
        Me.MonthCalendarAdv1.Colors.Selection.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.MonthCalendarAdv1.Colors.WeeklyMarker.TextColor = System.Drawing.Color.Red
        '
        '
        '
        Me.MonthCalendarAdv1.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.MonthCalendarAdv1.ContainerControlProcessDialogKey = True
        Me.MonthCalendarAdv1.DisplayMonth = New Date(2020, 1, 5, 0, 0, 0, 0)
        Me.MonthCalendarAdv1.FirstDayOfWeek = System.DayOfWeek.Monday
        Me.MonthCalendarAdv1.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MonthCalendarAdv1.Location = New System.Drawing.Point(238, 13)
        Me.MonthCalendarAdv1.Name = "MonthCalendarAdv1"
        '
        '
        '
        Me.MonthCalendarAdv1.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2
        Me.MonthCalendarAdv1.NavigationBackgroundStyle.BackColorGradientAngle = 90
        Me.MonthCalendarAdv1.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.MonthCalendarAdv1.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.MonthCalendarAdv1.Size = New System.Drawing.Size(170, 131)
        Me.MonthCalendarAdv1.TabIndex = 36
        Me.MonthCalendarAdv1.TabStop = False
        Me.MonthCalendarAdv1.WeeklyMarkedDays = New System.DayOfWeek() {System.DayOfWeek.Sunday}
        '
        'dgvWeeklyDiary
        '
        Me.dgvWeeklyDiary.AllowUserToAddRows = False
        Me.dgvWeeklyDiary.AllowUserToDeleteRows = False
        Me.dgvWeeklyDiary.AllowUserToOrderColumns = True
        Me.dgvWeeklyDiary.AutoGenerateColumns = False
        Me.dgvWeeklyDiary.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgvWeeklyDiary.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle12.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle12.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvWeeklyDiary.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle12
        Me.dgvWeeklyDiary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvWeeklyDiary.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.IDDataGridViewTextBoxColumn1, Me.DiaryDateDataGridViewTextBoxColumn, Me.WorkDoneDataGridViewTextBoxColumn, Me.RemarksDataGridViewTextBoxColumn1})
        Me.dgvWeeklyDiary.DataSource = Me.WeeklyDiaryBindingSource
        DataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle16.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle16.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle16.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle16.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvWeeklyDiary.DefaultCellStyle = DataGridViewCellStyle16
        Me.dgvWeeklyDiary.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.dgvWeeklyDiary.EnableHeadersVisualStyles = False
        Me.dgvWeeklyDiary.GridColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.dgvWeeklyDiary.Location = New System.Drawing.Point(0, 155)
        Me.dgvWeeklyDiary.MultiSelect = False
        Me.dgvWeeklyDiary.Name = "dgvWeeklyDiary"
        DataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle17.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle17.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle17.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle17.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvWeeklyDiary.RowHeadersDefaultCellStyle = DataGridViewCellStyle17
        Me.dgvWeeklyDiary.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvWeeklyDiary.RowTemplate.Height = 40
        Me.dgvWeeklyDiary.SelectAllSignVisible = False
        Me.dgvWeeklyDiary.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvWeeklyDiary.Size = New System.Drawing.Size(988, 328)
        Me.dgvWeeklyDiary.TabIndex = 1
        Me.dgvWeeklyDiary.TabStop = False
        '
        'IDDataGridViewTextBoxColumn1
        '
        Me.IDDataGridViewTextBoxColumn1.DataPropertyName = "ID"
        Me.IDDataGridViewTextBoxColumn1.HeaderText = "ID"
        Me.IDDataGridViewTextBoxColumn1.Name = "IDDataGridViewTextBoxColumn1"
        Me.IDDataGridViewTextBoxColumn1.Visible = False
        Me.IDDataGridViewTextBoxColumn1.Width = 50
        '
        'DiaryDateDataGridViewTextBoxColumn
        '
        Me.DiaryDateDataGridViewTextBoxColumn.DataPropertyName = "DiaryDate"
        DataGridViewCellStyle13.Format = "dd/MM/yyyy dddd"
        Me.DiaryDateDataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle13
        Me.DiaryDateDataGridViewTextBoxColumn.HeaderText = "Diary Date"
        Me.DiaryDateDataGridViewTextBoxColumn.Name = "DiaryDateDataGridViewTextBoxColumn"
        Me.DiaryDateDataGridViewTextBoxColumn.ReadOnly = True
        '
        'WorkDoneDataGridViewTextBoxColumn
        '
        Me.WorkDoneDataGridViewTextBoxColumn.DataPropertyName = "WorkDone"
        DataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft
        DataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.WorkDoneDataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle14
        Me.WorkDoneDataGridViewTextBoxColumn.HeaderText = "Work Done"
        Me.WorkDoneDataGridViewTextBoxColumn.Name = "WorkDoneDataGridViewTextBoxColumn"
        Me.WorkDoneDataGridViewTextBoxColumn.Width = 625
        '
        'RemarksDataGridViewTextBoxColumn1
        '
        Me.RemarksDataGridViewTextBoxColumn1.DataPropertyName = "Remarks"
        DataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft
        DataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.RemarksDataGridViewTextBoxColumn1.DefaultCellStyle = DataGridViewCellStyle15
        Me.RemarksDataGridViewTextBoxColumn1.HeaderText = "Remarks"
        Me.RemarksDataGridViewTextBoxColumn1.Name = "RemarksDataGridViewTextBoxColumn1"
        Me.RemarksDataGridViewTextBoxColumn1.Width = 200
        '
        'WeeklyDiaryBindingSource
        '
        Me.WeeklyDiaryBindingSource.DataMember = "WeeklyDiary"
        Me.WeeklyDiaryBindingSource.DataSource = Me.WeeklyDiaryDataSet1
        '
        'WeeklyDiaryDataSet1
        '
        Me.WeeklyDiaryDataSet1.DataSetName = "WeeklyDiaryDataSet"
        Me.WeeklyDiaryDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'tabWD
        '
        Me.tabWD.AttachedControl = Me.SuperTabControlPanel1
        Me.tabWD.GlobalItem = False
        Me.tabWD.Name = "tabWD"
        Me.tabWD.Text = "Weekly Diary"
        '
        'SuperTabControlPanel3
        '
        Me.SuperTabControlPanel3.Controls.Add(Me.PanelEx3)
        Me.SuperTabControlPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SuperTabControlPanel3.Location = New System.Drawing.Point(0, 28)
        Me.SuperTabControlPanel3.Name = "SuperTabControlPanel3"
        Me.SuperTabControlPanel3.Size = New System.Drawing.Size(988, 483)
        Me.SuperTabControlPanel3.TabIndex = 0
        Me.SuperTabControlPanel3.TabItem = Me.tabOD
        '
        'PanelEx3
        '
        Me.PanelEx3.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.PanelEx3.Controls.Add(Me.LabelX9)
        Me.PanelEx3.Controls.Add(Me.LabelX8)
        Me.PanelEx3.Controls.Add(Me.LabelX37)
        Me.PanelEx3.Controls.Add(Me.btnSaveOfficeDetails)
        Me.PanelEx3.Controls.Add(Me.LabelX7)
        Me.PanelEx3.Controls.Add(Me.LabelX6)
        Me.PanelEx3.Controls.Add(Me.LabelX5)
        Me.PanelEx3.Controls.Add(Me.LabelX4)
        Me.PanelEx3.Controls.Add(Me.LabelX3)
        Me.PanelEx3.Controls.Add(Me.txtODRemarks)
        Me.PanelEx3.Controls.Add(Me.dtTo)
        Me.PanelEx3.Controls.Add(Me.dtFrom)
        Me.PanelEx3.Controls.Add(Me.txtDesignation)
        Me.PanelEx3.Controls.Add(Me.txtUnit)
        Me.PanelEx3.Controls.Add(Me.dgvOfficeDetails)
        Me.PanelEx3.DisabledBackColor = System.Drawing.Color.Empty
        Me.PanelEx3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelEx3.Location = New System.Drawing.Point(0, 0)
        Me.PanelEx3.Name = "PanelEx3"
        Me.PanelEx3.Size = New System.Drawing.Size(988, 483)
        Me.PanelEx3.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx3.Style.GradientAngle = 90
        Me.PanelEx3.TabIndex = 4
        '
        'LabelX9
        '
        Me.LabelX9.AutoSize = True
        '
        '
        '
        Me.LabelX9.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX9.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX9.Location = New System.Drawing.Point(285, 100)
        Me.LabelX9.Name = "LabelX9"
        Me.LabelX9.Size = New System.Drawing.Size(7, 22)
        Me.LabelX9.TabIndex = 62
        Me.LabelX9.Text = "<font color=""#ED1C24"">*</font><b></b>"
        '
        'LabelX8
        '
        Me.LabelX8.AutoSize = True
        '
        '
        '
        Me.LabelX8.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX8.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX8.Location = New System.Drawing.Point(285, 42)
        Me.LabelX8.Name = "LabelX8"
        Me.LabelX8.Size = New System.Drawing.Size(7, 22)
        Me.LabelX8.TabIndex = 61
        Me.LabelX8.Text = "<font color=""#ED1C24"">*</font><b></b>"
        '
        'LabelX37
        '
        Me.LabelX37.AutoSize = True
        '
        '
        '
        Me.LabelX37.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX37.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX37.Location = New System.Drawing.Point(414, 13)
        Me.LabelX37.Name = "LabelX37"
        Me.LabelX37.Size = New System.Drawing.Size(7, 22)
        Me.LabelX37.TabIndex = 60
        Me.LabelX37.Text = "<font color=""#ED1C24"">*</font><b></b>"
        '
        'btnSaveOfficeDetails
        '
        Me.btnSaveOfficeDetails.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnSaveOfficeDetails.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnSaveOfficeDetails.Image = CType(resources.GetObject("btnSaveOfficeDetails.Image"), System.Drawing.Image)
        Me.btnSaveOfficeDetails.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top
        Me.btnSaveOfficeDetails.Location = New System.Drawing.Point(814, 29)
        Me.btnSaveOfficeDetails.Name = "btnSaveOfficeDetails"
        Me.btnSaveOfficeDetails.Size = New System.Drawing.Size(123, 73)
        Me.btnSaveOfficeDetails.TabIndex = 6
        Me.btnSaveOfficeDetails.Text = "Save"
        '
        'LabelX7
        '
        Me.LabelX7.AutoSize = True
        '
        '
        '
        Me.LabelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX7.Location = New System.Drawing.Point(438, 13)
        Me.LabelX7.Name = "LabelX7"
        Me.LabelX7.Size = New System.Drawing.Size(51, 18)
        Me.LabelX7.TabIndex = 16
        Me.LabelX7.Text = "Remarks"
        '
        'LabelX6
        '
        Me.LabelX6.AutoSize = True
        '
        '
        '
        Me.LabelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX6.Location = New System.Drawing.Point(32, 100)
        Me.LabelX6.Name = "LabelX6"
        Me.LabelX6.Size = New System.Drawing.Size(70, 18)
        Me.LabelX6.TabIndex = 15
        Me.LabelX6.Text = "Designation"
        '
        'LabelX5
        '
        Me.LabelX5.AutoSize = True
        '
        '
        '
        Me.LabelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX5.Location = New System.Drawing.Point(32, 71)
        Me.LabelX5.Name = "LabelX5"
        Me.LabelX5.Size = New System.Drawing.Size(47, 18)
        Me.LabelX5.TabIndex = 14
        Me.LabelX5.Text = "To Date"
        '
        'LabelX4
        '
        Me.LabelX4.AutoSize = True
        '
        '
        '
        Me.LabelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX4.Location = New System.Drawing.Point(32, 42)
        Me.LabelX4.Name = "LabelX4"
        Me.LabelX4.Size = New System.Drawing.Size(62, 18)
        Me.LabelX4.TabIndex = 13
        Me.LabelX4.Text = "From Date"
        '
        'LabelX3
        '
        Me.LabelX3.AutoSize = True
        '
        '
        '
        Me.LabelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX3.Location = New System.Drawing.Point(32, 13)
        Me.LabelX3.Name = "LabelX3"
        Me.LabelX3.Size = New System.Drawing.Size(27, 18)
        Me.LabelX3.TabIndex = 12
        Me.LabelX3.Text = "Unit"
        '
        'txtODRemarks
        '
        Me.txtODRemarks.AcceptsReturn = True
        Me.txtODRemarks.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtODRemarks.Border.Class = "TextBoxBorder"
        Me.txtODRemarks.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtODRemarks.ButtonCustom.Image = CType(resources.GetObject("txtODRemarks.ButtonCustom.Image"), System.Drawing.Image)
        Me.txtODRemarks.DisabledBackColor = System.Drawing.Color.White
        Me.txtODRemarks.FocusHighlightEnabled = True
        Me.txtODRemarks.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtODRemarks.ForeColor = System.Drawing.Color.Black
        Me.txtODRemarks.Location = New System.Drawing.Point(495, 10)
        Me.txtODRemarks.MaxLength = 255
        Me.txtODRemarks.Multiline = True
        Me.txtODRemarks.Name = "txtODRemarks"
        Me.txtODRemarks.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtODRemarks.Size = New System.Drawing.Size(280, 112)
        Me.txtODRemarks.TabIndex = 5
        Me.txtODRemarks.WatermarkBehavior = DevComponents.DotNetBar.eWatermarkBehavior.HideNonEmpty
        Me.txtODRemarks.WatermarkText = "Remarks"
        '
        'dtTo
        '
        Me.dtTo.AutoAdvance = True
        Me.dtTo.AutoSelectDate = True
        Me.dtTo.BackColor = System.Drawing.SystemColors.Window
        '
        '
        '
        Me.dtTo.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.dtTo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.dtTo.ButtonClear.Image = CType(resources.GetObject("dtTo.ButtonClear.Image"), System.Drawing.Image)
        Me.dtTo.ButtonClear.Visible = True
        Me.dtTo.ButtonDropDown.Visible = True
        Me.dtTo.CustomFormat = "dd/MM/yyyy"
        Me.dtTo.FocusHighlightEnabled = True
        Me.dtTo.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtTo.Format = DevComponents.Editors.eDateTimePickerFormat.Custom
        Me.dtTo.IsPopupCalendarOpen = False
        Me.dtTo.Location = New System.Drawing.Point(107, 68)
        Me.dtTo.MaxDate = New Date(2100, 12, 31, 0, 0, 0, 0)
        Me.dtTo.MinDate = New Date(1900, 1, 1, 0, 0, 0, 0)
        '
        '
        '
        '
        '
        '
        Me.dtTo.MonthCalendar.BackgroundStyle.BackColor = System.Drawing.SystemColors.Window
        Me.dtTo.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.dtTo.MonthCalendar.ClearButtonVisible = True
        '
        '
        '
        Me.dtTo.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2
        Me.dtTo.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90
        Me.dtTo.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.dtTo.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.dtTo.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder
        Me.dtTo.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1
        Me.dtTo.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.dtTo.MonthCalendar.DaySize = New System.Drawing.Size(30, 15)
        Me.dtTo.MonthCalendar.DisplayMonth = New Date(2008, 7, 1, 0, 0, 0, 0)
        Me.dtTo.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday
        '
        '
        '
        Me.dtTo.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.dtTo.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90
        Me.dtTo.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.dtTo.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.dtTo.MonthCalendar.TodayButtonVisible = True
        Me.dtTo.Name = "dtTo"
        Me.dtTo.Size = New System.Drawing.Size(172, 25)
        Me.dtTo.TabIndex = 3
        Me.dtTo.WatermarkText = "To Date"
        '
        'dtFrom
        '
        Me.dtFrom.AutoAdvance = True
        Me.dtFrom.AutoSelectDate = True
        Me.dtFrom.BackColor = System.Drawing.SystemColors.Window
        '
        '
        '
        Me.dtFrom.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.dtFrom.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.dtFrom.ButtonClear.Image = CType(resources.GetObject("dtFrom.ButtonClear.Image"), System.Drawing.Image)
        Me.dtFrom.ButtonClear.Visible = True
        Me.dtFrom.ButtonDropDown.Visible = True
        Me.dtFrom.CustomFormat = "dd/MM/yyyy"
        Me.dtFrom.FocusHighlightEnabled = True
        Me.dtFrom.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtFrom.Format = DevComponents.Editors.eDateTimePickerFormat.Custom
        Me.dtFrom.IsPopupCalendarOpen = False
        Me.dtFrom.Location = New System.Drawing.Point(107, 39)
        Me.dtFrom.MaxDate = New Date(2100, 12, 31, 0, 0, 0, 0)
        Me.dtFrom.MinDate = New Date(1900, 1, 1, 0, 0, 0, 0)
        '
        '
        '
        '
        '
        '
        Me.dtFrom.MonthCalendar.BackgroundStyle.BackColor = System.Drawing.SystemColors.Window
        Me.dtFrom.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.dtFrom.MonthCalendar.ClearButtonVisible = True
        '
        '
        '
        Me.dtFrom.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2
        Me.dtFrom.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90
        Me.dtFrom.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.dtFrom.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.dtFrom.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder
        Me.dtFrom.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1
        Me.dtFrom.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.dtFrom.MonthCalendar.DaySize = New System.Drawing.Size(30, 15)
        Me.dtFrom.MonthCalendar.DisplayMonth = New Date(2008, 7, 1, 0, 0, 0, 0)
        Me.dtFrom.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday
        '
        '
        '
        Me.dtFrom.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.dtFrom.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90
        Me.dtFrom.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.dtFrom.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.dtFrom.MonthCalendar.TodayButtonVisible = True
        Me.dtFrom.Name = "dtFrom"
        Me.dtFrom.Size = New System.Drawing.Size(172, 25)
        Me.dtFrom.TabIndex = 2
        Me.dtFrom.WatermarkText = "From Date"
        '
        'txtDesignation
        '
        Me.txtDesignation.AutoCompleteCustomSource.AddRange(New String() {"Tester Inspector", "Fingerprint Expert", "Fingerprint Searcher"})
        Me.txtDesignation.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.txtDesignation.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtDesignation.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtDesignation.Border.Class = "TextBoxBorder"
        Me.txtDesignation.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtDesignation.DisabledBackColor = System.Drawing.Color.White
        Me.txtDesignation.FocusHighlightEnabled = True
        Me.txtDesignation.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDesignation.ForeColor = System.Drawing.Color.Black
        Me.txtDesignation.Location = New System.Drawing.Point(107, 97)
        Me.txtDesignation.Name = "txtDesignation"
        Me.txtDesignation.PreventEnterBeep = True
        Me.txtDesignation.Size = New System.Drawing.Size(172, 25)
        Me.txtDesignation.TabIndex = 4
        Me.txtDesignation.WatermarkText = "Designation"
        '
        'txtUnit
        '
        Me.txtUnit.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtUnit.Border.Class = "TextBoxBorder"
        Me.txtUnit.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtUnit.DisabledBackColor = System.Drawing.Color.White
        Me.txtUnit.FocusHighlightEnabled = True
        Me.txtUnit.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUnit.ForeColor = System.Drawing.Color.Black
        Me.txtUnit.Location = New System.Drawing.Point(107, 10)
        Me.txtUnit.Name = "txtUnit"
        Me.txtUnit.PreventEnterBeep = True
        Me.txtUnit.Size = New System.Drawing.Size(301, 25)
        Me.txtUnit.TabIndex = 1
        Me.txtUnit.WatermarkText = "Unit"
        '
        'dgvOfficeDetails
        '
        Me.dgvOfficeDetails.AllowUserToAddRows = False
        Me.dgvOfficeDetails.AllowUserToDeleteRows = False
        Me.dgvOfficeDetails.AllowUserToOrderColumns = True
        Me.dgvOfficeDetails.AutoGenerateColumns = False
        Me.dgvOfficeDetails.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle18.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle18.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle18.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle18.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle18.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvOfficeDetails.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle18
        Me.dgvOfficeDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvOfficeDetails.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.UnitDataGridViewTextBoxColumn, Me.FromDateDataGridViewTextBoxColumn, Me.ToDateDataGridViewTextBoxColumn, Me.DesignationDataGridViewTextBoxColumn, Me.RemarksDataGridViewTextBoxColumn, Me.IDDataGridViewTextBoxColumn})
        Me.dgvOfficeDetails.DataSource = Me.OfficeDetailsBindingSource
        DataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle21.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle21.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle21.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle21.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle21.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle21.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvOfficeDetails.DefaultCellStyle = DataGridViewCellStyle21
        Me.dgvOfficeDetails.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.dgvOfficeDetails.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvOfficeDetails.EnableHeadersVisualStyles = False
        Me.dgvOfficeDetails.GridColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.dgvOfficeDetails.Location = New System.Drawing.Point(0, 128)
        Me.dgvOfficeDetails.MultiSelect = False
        Me.dgvOfficeDetails.Name = "dgvOfficeDetails"
        DataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle22.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle22.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle22.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle22.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle22.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle22.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvOfficeDetails.RowHeadersDefaultCellStyle = DataGridViewCellStyle22
        Me.dgvOfficeDetails.RowTemplate.Height = 30
        Me.dgvOfficeDetails.SelectAllSignVisible = False
        Me.dgvOfficeDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvOfficeDetails.Size = New System.Drawing.Size(988, 355)
        Me.dgvOfficeDetails.TabIndex = 0
        Me.dgvOfficeDetails.TabStop = False
        '
        'UnitDataGridViewTextBoxColumn
        '
        Me.UnitDataGridViewTextBoxColumn.DataPropertyName = "Unit"
        Me.UnitDataGridViewTextBoxColumn.HeaderText = "Unit"
        Me.UnitDataGridViewTextBoxColumn.Name = "UnitDataGridViewTextBoxColumn"
        Me.UnitDataGridViewTextBoxColumn.Width = 200
        '
        'FromDateDataGridViewTextBoxColumn
        '
        Me.FromDateDataGridViewTextBoxColumn.DataPropertyName = "FromDate"
        DataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle19.Format = "dd/MM/yyyy"
        Me.FromDateDataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle19
        Me.FromDateDataGridViewTextBoxColumn.HeaderText = "From Date"
        Me.FromDateDataGridViewTextBoxColumn.Name = "FromDateDataGridViewTextBoxColumn"
        '
        'ToDateDataGridViewTextBoxColumn
        '
        Me.ToDateDataGridViewTextBoxColumn.DataPropertyName = "ToDate"
        DataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle20.Format = "dd/MM/yyyy"
        Me.ToDateDataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle20
        Me.ToDateDataGridViewTextBoxColumn.HeaderText = "To Date"
        Me.ToDateDataGridViewTextBoxColumn.Name = "ToDateDataGridViewTextBoxColumn"
        '
        'DesignationDataGridViewTextBoxColumn
        '
        Me.DesignationDataGridViewTextBoxColumn.DataPropertyName = "Designation"
        Me.DesignationDataGridViewTextBoxColumn.HeaderText = "Designation"
        Me.DesignationDataGridViewTextBoxColumn.Name = "DesignationDataGridViewTextBoxColumn"
        Me.DesignationDataGridViewTextBoxColumn.Width = 150
        '
        'RemarksDataGridViewTextBoxColumn
        '
        Me.RemarksDataGridViewTextBoxColumn.DataPropertyName = "Remarks"
        Me.RemarksDataGridViewTextBoxColumn.HeaderText = "Remarks"
        Me.RemarksDataGridViewTextBoxColumn.Name = "RemarksDataGridViewTextBoxColumn"
        Me.RemarksDataGridViewTextBoxColumn.Width = 250
        '
        'IDDataGridViewTextBoxColumn
        '
        Me.IDDataGridViewTextBoxColumn.DataPropertyName = "ID"
        Me.IDDataGridViewTextBoxColumn.HeaderText = "ID"
        Me.IDDataGridViewTextBoxColumn.Name = "IDDataGridViewTextBoxColumn"
        Me.IDDataGridViewTextBoxColumn.Visible = False
        '
        'OfficeDetailsBindingSource
        '
        Me.OfficeDetailsBindingSource.DataMember = "OfficeDetails"
        Me.OfficeDetailsBindingSource.DataSource = Me.WeeklyDiaryDataSet1
        '
        'tabOD
        '
        Me.tabOD.AttachedControl = Me.SuperTabControlPanel3
        Me.tabOD.GlobalItem = False
        Me.tabOD.Name = "tabOD"
        Me.tabOD.Text = "Office Details"
        '
        'SuperTabControlPanel2
        '
        Me.SuperTabControlPanel2.Controls.Add(Me.PanelEx2)
        Me.SuperTabControlPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SuperTabControlPanel2.Location = New System.Drawing.Point(0, 28)
        Me.SuperTabControlPanel2.Name = "SuperTabControlPanel2"
        Me.SuperTabControlPanel2.Size = New System.Drawing.Size(988, 483)
        Me.SuperTabControlPanel2.TabIndex = 0
        Me.SuperTabControlPanel2.TabItem = Me.tabPass
        '
        'PanelEx2
        '
        Me.PanelEx2.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.PanelEx2.Controls.Add(Me.txtOldPassword)
        Me.PanelEx2.Controls.Add(Me.lblOldPassword)
        Me.PanelEx2.Controls.Add(Me.lblPEN)
        Me.PanelEx2.Controls.Add(Me.btnCancelPassword)
        Me.PanelEx2.Controls.Add(Me.btnSavePassword)
        Me.PanelEx2.Controls.Add(Me.btnCancelName)
        Me.PanelEx2.Controls.Add(Me.btnSaveName)
        Me.PanelEx2.Controls.Add(Me.lblChangeName)
        Me.PanelEx2.Controls.Add(Me.txtName)
        Me.PanelEx2.Controls.Add(Me.txtPassword2)
        Me.PanelEx2.Controls.Add(Me.txtPassword1)
        Me.PanelEx2.Controls.Add(Me.lblPassword2)
        Me.PanelEx2.Controls.Add(Me.lblPassword1)
        Me.PanelEx2.Controls.Add(Me.lblChangePassword)
        Me.PanelEx2.Controls.Add(Me.LabelX2)
        Me.PanelEx2.Controls.Add(Me.LabelX1)
        Me.PanelEx2.DisabledBackColor = System.Drawing.Color.Empty
        Me.PanelEx2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelEx2.Location = New System.Drawing.Point(0, 0)
        Me.PanelEx2.Name = "PanelEx2"
        Me.PanelEx2.Size = New System.Drawing.Size(988, 483)
        Me.PanelEx2.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx2.Style.GradientAngle = 90
        Me.PanelEx2.TabIndex = 0
        '
        'txtOldPassword
        '
        Me.txtOldPassword.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtOldPassword.Border.Class = "TextBoxBorder"
        Me.txtOldPassword.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtOldPassword.DisabledBackColor = System.Drawing.Color.White
        Me.txtOldPassword.FocusHighlightEnabled = True
        Me.txtOldPassword.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOldPassword.ForeColor = System.Drawing.Color.Black
        Me.txtOldPassword.Location = New System.Drawing.Point(128, 133)
        Me.txtOldPassword.Name = "txtOldPassword"
        Me.txtOldPassword.PreventEnterBeep = True
        Me.txtOldPassword.Size = New System.Drawing.Size(172, 25)
        Me.txtOldPassword.TabIndex = 4
        Me.txtOldPassword.WatermarkText = "Current Password"
        '
        'lblOldPassword
        '
        Me.lblOldPassword.AutoSize = True
        '
        '
        '
        Me.lblOldPassword.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblOldPassword.Location = New System.Drawing.Point(20, 136)
        Me.lblOldPassword.Name = "lblOldPassword"
        Me.lblOldPassword.Size = New System.Drawing.Size(102, 18)
        Me.lblOldPassword.TabIndex = 14
        Me.lblOldPassword.Text = "Current Password"
        '
        'lblPEN
        '
        Me.lblPEN.AutoSize = True
        '
        '
        '
        Me.lblPEN.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblPEN.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPEN.ForeColor = System.Drawing.Color.Red
        Me.lblPEN.Location = New System.Drawing.Point(125, 19)
        Me.lblPEN.Name = "lblPEN"
        Me.lblPEN.Size = New System.Drawing.Size(33, 24)
        Me.lblPEN.TabIndex = 12
        Me.lblPEN.Text = "PEN"
        '
        'btnCancelPassword
        '
        Me.btnCancelPassword.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnCancelPassword.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnCancelPassword.Location = New System.Drawing.Point(318, 193)
        Me.btnCancelPassword.Name = "btnCancelPassword"
        Me.btnCancelPassword.Size = New System.Drawing.Size(97, 23)
        Me.btnCancelPassword.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnCancelPassword.TabIndex = 8
        Me.btnCancelPassword.Text = "Cancel"
        '
        'btnSavePassword
        '
        Me.btnSavePassword.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnSavePassword.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnSavePassword.Location = New System.Drawing.Point(318, 162)
        Me.btnSavePassword.Name = "btnSavePassword"
        Me.btnSavePassword.Size = New System.Drawing.Size(97, 23)
        Me.btnSavePassword.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnSavePassword.TabIndex = 7
        Me.btnSavePassword.Text = "Save"
        '
        'btnCancelName
        '
        Me.btnCancelName.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnCancelName.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnCancelName.Location = New System.Drawing.Point(425, 64)
        Me.btnCancelName.Name = "btnCancelName"
        Me.btnCancelName.Size = New System.Drawing.Size(97, 23)
        Me.btnCancelName.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnCancelName.TabIndex = 3
        Me.btnCancelName.Text = "Cancel"
        '
        'btnSaveName
        '
        Me.btnSaveName.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnSaveName.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnSaveName.Location = New System.Drawing.Point(318, 64)
        Me.btnSaveName.Name = "btnSaveName"
        Me.btnSaveName.Size = New System.Drawing.Size(97, 23)
        Me.btnSaveName.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnSaveName.TabIndex = 2
        Me.btnSaveName.Text = "Save"
        '
        'lblChangeName
        '
        Me.lblChangeName.AutoSize = True
        '
        '
        '
        Me.lblChangeName.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblChangeName.Location = New System.Drawing.Point(212, 41)
        Me.lblChangeName.Name = "lblChangeName"
        Me.lblChangeName.Size = New System.Drawing.Size(85, 18)
        Me.lblChangeName.TabIndex = 11
        Me.lblChangeName.Text = "<a>Change Name </a>"
        '
        'txtName
        '
        Me.txtName.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtName.Border.Class = "TextBoxBorder"
        Me.txtName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtName.DisabledBackColor = System.Drawing.Color.White
        Me.txtName.FocusHighlightEnabled = True
        Me.txtName.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtName.ForeColor = System.Drawing.Color.Black
        Me.txtName.Location = New System.Drawing.Point(125, 64)
        Me.txtName.Name = "txtName"
        Me.txtName.PreventEnterBeep = True
        Me.txtName.Size = New System.Drawing.Size(172, 25)
        Me.txtName.TabIndex = 1
        Me.txtName.WatermarkText = "Name"
        '
        'txtPassword2
        '
        Me.txtPassword2.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtPassword2.Border.Class = "TextBoxBorder"
        Me.txtPassword2.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtPassword2.DisabledBackColor = System.Drawing.Color.White
        Me.txtPassword2.FocusHighlightEnabled = True
        Me.txtPassword2.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPassword2.ForeColor = System.Drawing.Color.Black
        Me.txtPassword2.Location = New System.Drawing.Point(128, 193)
        Me.txtPassword2.Name = "txtPassword2"
        Me.txtPassword2.PreventEnterBeep = True
        Me.txtPassword2.Size = New System.Drawing.Size(172, 25)
        Me.txtPassword2.TabIndex = 6
        Me.txtPassword2.WatermarkText = "Confirm Password"
        '
        'txtPassword1
        '
        Me.txtPassword1.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtPassword1.Border.Class = "TextBoxBorder"
        Me.txtPassword1.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtPassword1.DisabledBackColor = System.Drawing.Color.White
        Me.txtPassword1.FocusHighlightEnabled = True
        Me.txtPassword1.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPassword1.ForeColor = System.Drawing.Color.Black
        Me.txtPassword1.Location = New System.Drawing.Point(128, 163)
        Me.txtPassword1.Name = "txtPassword1"
        Me.txtPassword1.PreventEnterBeep = True
        Me.txtPassword1.Size = New System.Drawing.Size(172, 25)
        Me.txtPassword1.TabIndex = 5
        Me.txtPassword1.WatermarkText = "New Password"
        '
        'lblPassword2
        '
        Me.lblPassword2.AutoSize = True
        '
        '
        '
        Me.lblPassword2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblPassword2.Location = New System.Drawing.Point(20, 196)
        Me.lblPassword2.Name = "lblPassword2"
        Me.lblPassword2.Size = New System.Drawing.Size(48, 18)
        Me.lblPassword2.TabIndex = 6
        Me.lblPassword2.Text = "Confirm"
        '
        'lblPassword1
        '
        Me.lblPassword1.AutoSize = True
        '
        '
        '
        Me.lblPassword1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblPassword1.Location = New System.Drawing.Point(20, 166)
        Me.lblPassword1.Name = "lblPassword1"
        Me.lblPassword1.Size = New System.Drawing.Size(85, 18)
        Me.lblPassword1.TabIndex = 5
        Me.lblPassword1.Text = "New Password"
        '
        'lblChangePassword
        '
        Me.lblChangePassword.AutoSize = True
        '
        '
        '
        Me.lblChangePassword.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblChangePassword.Location = New System.Drawing.Point(199, 109)
        Me.lblChangePassword.Name = "lblChangePassword"
        Me.lblChangePassword.Size = New System.Drawing.Size(101, 18)
        Me.lblChangePassword.TabIndex = 2
        Me.lblChangePassword.Text = "<a>Change Password</a>"
        '
        'LabelX2
        '
        Me.LabelX2.AutoSize = True
        '
        '
        '
        Me.LabelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX2.Location = New System.Drawing.Point(65, 66)
        Me.LabelX2.Name = "LabelX2"
        Me.LabelX2.Size = New System.Drawing.Size(36, 18)
        Me.LabelX2.TabIndex = 1
        Me.LabelX2.Text = "Name"
        '
        'LabelX1
        '
        Me.LabelX1.AutoSize = True
        '
        '
        '
        Me.LabelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX1.Location = New System.Drawing.Point(65, 22)
        Me.LabelX1.Name = "LabelX1"
        Me.LabelX1.Size = New System.Drawing.Size(26, 18)
        Me.LabelX1.TabIndex = 0
        Me.LabelX1.Text = "PEN"
        '
        'tabPass
        '
        Me.tabPass.AttachedControl = Me.SuperTabControlPanel2
        Me.tabPass.GlobalItem = False
        Me.tabPass.Name = "tabPass"
        Me.tabPass.Text = "Change Password"
        '
        'RibbonBar1
        '
        Me.RibbonBar1.AutoOverflowEnabled = True
        '
        '
        '
        Me.RibbonBar1.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.RibbonBar1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.RibbonBar1.ContainerControlProcessDialogKey = True
        Me.RibbonBar1.Dock = System.Windows.Forms.DockStyle.Top
        Me.RibbonBar1.DragDropSupport = True
        Me.RibbonBar1.Items.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnNewEntry, Me.btnEdit, Me.btnDelete, Me.btnReload, Me.btnOpenFolder, Me.btnPrintAll, Me.btnCL, Me.btnOnlineBackup, Me.btnRestore, Me.CircularProgress1})
        Me.RibbonBar1.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F"
        Me.RibbonBar1.Location = New System.Drawing.Point(0, 0)
        Me.RibbonBar1.Name = "RibbonBar1"
        Me.RibbonBar1.Size = New System.Drawing.Size(988, 65)
        Me.RibbonBar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.RibbonBar1.TabIndex = 0
        '
        '
        '
        Me.RibbonBar1.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.RibbonBar1.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        'btnNewEntry
        '
        Me.btnNewEntry.BeginGroup = True
        Me.btnNewEntry.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnNewEntry.Image = CType(resources.GetObject("btnNewEntry.Image"), System.Drawing.Image)
        Me.btnNewEntry.ImageSmall = CType(resources.GetObject("btnNewEntry.ImageSmall"), System.Drawing.Image)
        Me.btnNewEntry.Name = "btnNewEntry"
        Me.btnNewEntry.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlN)
        Me.btnNewEntry.SubItemsExpandWidth = 14
        Me.btnNewEntry.Text = "New"
        '
        'btnEdit
        '
        Me.btnEdit.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnEdit.Image = CType(resources.GetObject("btnEdit.Image"), System.Drawing.Image)
        Me.btnEdit.ImageSmall = CType(resources.GetObject("btnEdit.ImageSmall"), System.Drawing.Image)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.SubItemsExpandWidth = 14
        Me.btnEdit.Text = "Edit"
        '
        'btnDelete
        '
        Me.btnDelete.BeginGroup = True
        Me.btnDelete.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), System.Drawing.Image)
        Me.btnDelete.ImageSmall = CType(resources.GetObject("btnDelete.ImageSmall"), System.Drawing.Image)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.SubItemsExpandWidth = 14
        Me.btnDelete.Text = "Delete"
        '
        'btnReload
        '
        Me.btnReload.BeginGroup = True
        Me.btnReload.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnReload.Image = CType(resources.GetObject("btnReload.Image"), System.Drawing.Image)
        Me.btnReload.ImageSmall = CType(resources.GetObject("btnReload.ImageSmall"), System.Drawing.Image)
        Me.btnReload.Name = "btnReload"
        Me.btnReload.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.F5)
        Me.btnReload.SubItemsExpandWidth = 14
        Me.btnReload.Text = "Reload"
        '
        'btnOpenFolder
        '
        Me.btnOpenFolder.BeginGroup = True
        Me.btnOpenFolder.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnOpenFolder.Image = CType(resources.GetObject("btnOpenFolder.Image"), System.Drawing.Image)
        Me.btnOpenFolder.Name = "btnOpenFolder"
        Me.btnOpenFolder.SubItemsExpandWidth = 14
        Me.btnOpenFolder.Text = "Folder"
        '
        'btnPrintAll
        '
        Me.btnPrintAll.BeginGroup = True
        Me.btnPrintAll.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnPrintAll.Image = CType(resources.GetObject("btnPrintAll.Image"), System.Drawing.Image)
        Me.btnPrintAll.Name = "btnPrintAll"
        Me.btnPrintAll.SubItemsExpandWidth = 14
        Me.btnPrintAll.Text = "Diary"
        '
        'btnCL
        '
        Me.btnCL.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnCL.Image = CType(resources.GetObject("btnCL.Image"), System.Drawing.Image)
        Me.btnCL.Name = "btnCL"
        Me.btnCL.SubItemsExpandWidth = 14
        Me.btnCL.Text = "C/L"
        '
        'btnOnlineBackup
        '
        Me.btnOnlineBackup.BeginGroup = True
        Me.btnOnlineBackup.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnOnlineBackup.Image = CType(resources.GetObject("btnOnlineBackup.Image"), System.Drawing.Image)
        Me.btnOnlineBackup.Name = "btnOnlineBackup"
        Me.btnOnlineBackup.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlB)
        Me.btnOnlineBackup.SubItemsExpandWidth = 14
        Me.btnOnlineBackup.Text = "Backup"
        '
        'btnRestore
        '
        Me.btnRestore.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnRestore.Image = CType(resources.GetObject("btnRestore.Image"), System.Drawing.Image)
        Me.btnRestore.Name = "btnRestore"
        Me.btnRestore.SubItemsExpandWidth = 14
        Me.btnRestore.Text = "Restore"
        '
        'CircularProgress1
        '
        Me.CircularProgress1.Diameter = 60
        Me.CircularProgress1.Name = "CircularProgress1"
        Me.CircularProgress1.ProgressTextVisible = True
        '
        'bgwUpload
        '
        Me.bgwUpload.WorkerReportsProgress = True
        Me.bgwUpload.WorkerSupportsCancellation = True
        '
        'SocRegisterTableAdapter1
        '
        Me.SocRegisterTableAdapter1.ClearBeforeFill = True
        '
        'FingerPrintDataSet1
        '
        Me.FingerPrintDataSet1.DataSetName = "FingerPrintDataSet"
        Me.FingerPrintDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'AuthenticationTableAdapter1
        '
        Me.AuthenticationTableAdapter1.ClearBeforeFill = True
        '
        'WeeklyDiaryTableAdapter1
        '
        Me.WeeklyDiaryTableAdapter1.ClearBeforeFill = True
        '
        'PersonalDetailsTableAdapter1
        '
        Me.PersonalDetailsTableAdapter1.ClearBeforeFill = True
        '
        'OfficeDetailsTableAdapter1
        '
        Me.OfficeDetailsTableAdapter1.ClearBeforeFill = True
        '
        'bgwDownload
        '
        Me.bgwDownload.WorkerReportsProgress = True
        Me.bgwDownload.WorkerSupportsCancellation = True
        '
        'bgwAutoUpload
        '
        Me.bgwAutoUpload.WorkerReportsProgress = True
        Me.bgwAutoUpload.WorkerSupportsCancellation = True
        '
        'frmWeeklyDiaryDE
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(988, 576)
        Me.Controls.Add(Me.PanelEx1)
        Me.DoubleBuffered = True
        Me.EnableGlass = False
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmWeeklyDiaryDE"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Weekly Diary"
        Me.TitleText = "<b>Weekly Diary</b>"
        Me.PanelEx1.ResumeLayout(False)
        CType(Me.SuperTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SuperTabControl1.ResumeLayout(False)
        Me.SuperTabControlPanel1.ResumeLayout(False)
        Me.PanelEx4.ResumeLayout(False)
        Me.PanelEx4.PerformLayout()
        CType(Me.dgvWeeklyDiary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.WeeklyDiaryBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.WeeklyDiaryDataSet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SuperTabControlPanel3.ResumeLayout(False)
        Me.PanelEx3.ResumeLayout(False)
        Me.PanelEx3.PerformLayout()
        CType(Me.dtTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtFrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvOfficeDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OfficeDetailsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SuperTabControlPanel2.ResumeLayout(False)
        Me.PanelEx2.ResumeLayout(False)
        Me.PanelEx2.PerformLayout()
        CType(Me.FingerPrintDataSet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PanelEx1 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents RibbonBar1 As DevComponents.DotNetBar.RibbonBar
    Friend WithEvents SuperTabControl1 As DevComponents.DotNetBar.SuperTabControl
    Friend WithEvents SuperTabControlPanel1 As DevComponents.DotNetBar.SuperTabControlPanel
    Friend WithEvents tabWD As DevComponents.DotNetBar.SuperTabItem
    Friend WithEvents SuperTabControlPanel3 As DevComponents.DotNetBar.SuperTabControlPanel
    Friend WithEvents tabOD As DevComponents.DotNetBar.SuperTabItem
    Friend WithEvents SuperTabControlPanel2 As DevComponents.DotNetBar.SuperTabControlPanel
    Friend WithEvents tabPass As DevComponents.DotNetBar.SuperTabItem
    Friend WithEvents PanelEx3 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents PanelEx2 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents PanelEx4 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents lblChangePassword As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX2 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtPassword2 As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents txtPassword1 As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents lblPassword2 As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblPassword1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtName As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents lblChangeName As DevComponents.DotNetBar.LabelX
    Friend WithEvents btnCancelPassword As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnSavePassword As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnCancelName As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnSaveName As DevComponents.DotNetBar.ButtonX
    Friend WithEvents lblPEN As DevComponents.DotNetBar.LabelX
    Friend WithEvents AuthenticationTableAdapter1 As FingerprintInformationSystem.WeeklyDiaryDataSetTableAdapters.AuthenticationTableAdapter
    Friend WithEvents txtOldPassword As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents lblOldPassword As DevComponents.DotNetBar.LabelX
    Friend WithEvents WeeklyDiaryTableAdapter1 As FingerprintInformationSystem.WeeklyDiaryDataSetTableAdapters.WeeklyDiaryTableAdapter
    Friend WithEvents PersonalDetailsTableAdapter1 As FingerprintInformationSystem.WeeklyDiaryDataSetTableAdapters.PersonalDetailsTableAdapter
    Friend WithEvents WeeklyDiaryDataSet1 As FingerprintInformationSystem.WeeklyDiaryDataSet
    Friend WithEvents dgvOfficeDetails As DevComponents.DotNetBar.Controls.DataGridViewX
    Friend WithEvents OfficeDetailsBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents OfficeDetailsTableAdapter1 As FingerprintInformationSystem.WeeklyDiaryDataSetTableAdapters.OfficeDetailsTableAdapter
    Friend WithEvents txtDesignation As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents txtUnit As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents dtTo As DevComponents.Editors.DateTimeAdv.DateTimeInput
    Friend WithEvents dtFrom As DevComponents.Editors.DateTimeAdv.DateTimeInput
    Friend WithEvents txtODRemarks As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX7 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX6 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX5 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX4 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX3 As DevComponents.DotNetBar.LabelX
    Friend WithEvents btnSaveOfficeDetails As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnNewEntry As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnEdit As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnDelete As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents LabelX8 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX37 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX9 As DevComponents.DotNetBar.LabelX
    Friend WithEvents btnReload As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnOnlineBackup As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnOpenFolder As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents CircularProgress1 As DevComponents.DotNetBar.CircularProgressItem
    Friend WithEvents bgwUpload As System.ComponentModel.BackgroundWorker
    Friend WithEvents dgvWeeklyDiary As DevComponents.DotNetBar.Controls.DataGridViewX
    Friend WithEvents WeeklyDiaryBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents MonthCalendarAdv1 As DevComponents.Editors.DateTimeAdv.MonthCalendarAdv
    Friend WithEvents btnSaveWD As DevComponents.DotNetBar.ButtonX
    Friend WithEvents lblSelectedDate As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX10 As DevComponents.DotNetBar.LabelX
    Friend WithEvents btnGenerateWD As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnPrintWD As DevComponents.DotNetBar.ButtonX
    Friend WithEvents SocRegisterTableAdapter1 As FingerprintInformationSystem.FingerPrintDataSetTableAdapters.SOCRegisterTableAdapter
    Friend WithEvents FingerPrintDataSet1 As FingerprintInformationSystem.FingerPrintDataSet
    Friend WithEvents btnCL As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnRestore As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents bgwDownload As System.ComponentModel.BackgroundWorker
    Friend WithEvents IDDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DiaryDateDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents WorkDoneDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RemarksDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents UnitDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FromDateDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ToDateDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DesignationDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RemarksDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnPrintAll As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents bgwAutoUpload As System.ComponentModel.BackgroundWorker
End Class
