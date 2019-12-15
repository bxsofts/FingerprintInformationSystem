<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFPAStatement
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFPAStatement))
        Dim DataGridViewCellStyle25 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle28 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle29 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle30 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle35 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle36 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle31 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle32 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle33 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle34 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle26 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle27 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.LabelX2 = New DevComponents.DotNetBar.LabelX()
        Me.btnPrint = New DevComponents.DotNetBar.ButtonX()
        Me.LabelX1 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX4 = New DevComponents.DotNetBar.LabelX()
        Me.btnGenerateByDate = New DevComponents.DotNetBar.ButtonX()
        Me.LabelX3 = New DevComponents.DotNetBar.LabelX()
        Me.txtYear = New DevComponents.Editors.IntegerInput()
        Me.dtFrom = New DevComponents.Editors.DateTimeAdv.DateTimeInput()
        Me.cmbMonth = New DevComponents.DotNetBar.Controls.ComboBoxEx()
        Me.dtTo = New DevComponents.Editors.DateTimeAdv.DateTimeInput()
        Me.PanelEx3 = New DevComponents.DotNetBar.PanelEx()
        Me.btnGenerateMonthlyData = New DevComponents.DotNetBar.ButtonX()
        Me.CircularProgress1 = New DevComponents.DotNetBar.Controls.CircularProgress()
        Me.LabelX5 = New DevComponents.DotNetBar.LabelX()
        Me.chkCoB = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.chkLetter = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.ChalanTableBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.FingerPrintDataSet = New FingerprintInformationSystem.FingerPrintDataSet()
        Me.bgwLetter = New System.ComponentModel.BackgroundWorker()
        Me.bgwCoB = New System.ComponentModel.BackgroundWorker()
        Me.ChalanTableTableAdapter1 = New FingerprintInformationSystem.FingerPrintDataSetTableAdapters.ChalanTableTableAdapter()
        Me.PanelEx1 = New DevComponents.DotNetBar.PanelEx()
        Me.PanelEx2 = New DevComponents.DotNetBar.PanelEx()
        Me.dgvChalan = New DevComponents.DotNetBar.Controls.DataGridViewX()
        Me.dgvSum = New DevComponents.DotNetBar.Controls.DataGridViewX()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lblAmount1 = New DevComponents.DotNetBar.LabelX()
        Me.lblAmount4 = New DevComponents.DotNetBar.LabelX()
        Me.lblAmount3 = New DevComponents.DotNetBar.LabelX()
        Me.lblAmount2 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX10 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX11 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX12 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX13 = New DevComponents.DotNetBar.LabelX()
        Me.HeadOfAccountDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TreasuryDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ChalanNumberDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ChalanDateDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AmountRemittedDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.txtYear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtTo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelEx3.SuspendLayout()
        CType(Me.ChalanTableBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FingerPrintDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelEx1.SuspendLayout()
        Me.PanelEx2.SuspendLayout()
        CType(Me.dgvChalan, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvSum, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelX2
        '
        Me.LabelX2.AutoSize = True
        '
        '
        '
        Me.LabelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX2.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX2.Location = New System.Drawing.Point(245, 76)
        Me.LabelX2.Name = "LabelX2"
        Me.LabelX2.Size = New System.Drawing.Size(17, 18)
        Me.LabelX2.TabIndex = 8
        Me.LabelX2.Text = "To"
        '
        'btnPrint
        '
        Me.btnPrint.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnPrint.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnPrint.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(574, 34)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(118, 67)
        Me.btnPrint.TabIndex = 3
        Me.btnPrint.Text = "Print"
        '
        'LabelX1
        '
        Me.LabelX1.AutoSize = True
        '
        '
        '
        Me.LabelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX1.Location = New System.Drawing.Point(15, 76)
        Me.LabelX1.Name = "LabelX1"
        Me.LabelX1.Size = New System.Drawing.Size(72, 18)
        Me.LabelX1.TabIndex = 7
        Me.LabelX1.Text = "Period From"
        '
        'LabelX4
        '
        Me.LabelX4.AutoSize = True
        '
        '
        '
        Me.LabelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX4.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX4.Location = New System.Drawing.Point(245, 38)
        Me.LabelX4.Name = "LabelX4"
        Me.LabelX4.Size = New System.Drawing.Size(28, 18)
        Me.LabelX4.TabIndex = 25
        Me.LabelX4.Text = "Year"
        '
        'btnGenerateByDate
        '
        Me.btnGenerateByDate.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnGenerateByDate.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnGenerateByDate.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGenerateByDate.Location = New System.Drawing.Point(424, 72)
        Me.btnGenerateByDate.Name = "btnGenerateByDate"
        Me.btnGenerateByDate.Size = New System.Drawing.Size(118, 29)
        Me.btnGenerateByDate.TabIndex = 6
        Me.btnGenerateByDate.Text = "Generate"
        '
        'LabelX3
        '
        Me.LabelX3.AutoSize = True
        '
        '
        '
        Me.LabelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX3.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX3.Location = New System.Drawing.Point(15, 38)
        Me.LabelX3.Name = "LabelX3"
        Me.LabelX3.Size = New System.Drawing.Size(40, 18)
        Me.LabelX3.TabIndex = 24
        Me.LabelX3.Text = "Month"
        '
        'txtYear
        '
        '
        '
        '
        Me.txtYear.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.txtYear.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtYear.ButtonCustom.Image = CType(resources.GetObject("txtYear.ButtonCustom.Image"), System.Drawing.Image)
        Me.txtYear.FocusHighlightEnabled = True
        Me.txtYear.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtYear.InputHorizontalAlignment = DevComponents.Editors.eHorizontalAlignment.Left
        Me.txtYear.Location = New System.Drawing.Point(286, 34)
        Me.txtYear.MaxValue = 2099
        Me.txtYear.MinValue = 0
        Me.txtYear.Name = "txtYear"
        Me.txtYear.ShowUpDown = True
        Me.txtYear.Size = New System.Drawing.Size(118, 25)
        Me.txtYear.TabIndex = 2
        Me.txtYear.Value = 1900
        Me.txtYear.WatermarkText = "Year"
        '
        'dtFrom
        '
        Me.dtFrom.AllowEmptyState = False
        Me.dtFrom.AutoAdvance = True
        Me.dtFrom.AutoSelectDate = True
        Me.dtFrom.BackColor = System.Drawing.SystemColors.Window
        '
        '
        '
        Me.dtFrom.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.dtFrom.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.dtFrom.ButtonClear.Image = CType(resources.GetObject("dtFrom.ButtonClear.Image"), System.Drawing.Image)
        Me.dtFrom.ButtonDropDown.Visible = True
        Me.dtFrom.CustomFormat = "dd/MM/yyyy"
        Me.dtFrom.FocusHighlightEnabled = True
        Me.dtFrom.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtFrom.Format = DevComponents.Editors.eDateTimePickerFormat.Custom
        Me.dtFrom.IsPopupCalendarOpen = False
        Me.dtFrom.Location = New System.Drawing.Point(106, 72)
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
        '
        '
        '
        Me.dtFrom.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.dtFrom.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90
        Me.dtFrom.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.dtFrom.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.dtFrom.MonthCalendar.TodayButtonVisible = True
        Me.dtFrom.Name = "dtFrom"
        Me.dtFrom.Size = New System.Drawing.Size(118, 25)
        Me.dtFrom.TabIndex = 4
        Me.dtFrom.Value = New Date(1900, 1, 1, 0, 0, 0, 0)
        Me.dtFrom.WatermarkText = "From"
        '
        'cmbMonth
        '
        Me.cmbMonth.DisplayMember = "Text"
        Me.cmbMonth.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMonth.FocusHighlightEnabled = True
        Me.cmbMonth.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbMonth.ForeColor = System.Drawing.Color.Black
        Me.cmbMonth.FormattingEnabled = True
        Me.cmbMonth.ItemHeight = 20
        Me.cmbMonth.Location = New System.Drawing.Point(106, 34)
        Me.cmbMonth.MaxDropDownItems = 15
        Me.cmbMonth.MaxLength = 255
        Me.cmbMonth.Name = "cmbMonth"
        Me.cmbMonth.Size = New System.Drawing.Size(118, 26)
        Me.cmbMonth.TabIndex = 1
        Me.cmbMonth.WatermarkText = "Month"
        '
        'dtTo
        '
        Me.dtTo.AllowEmptyState = False
        Me.dtTo.AutoAdvance = True
        Me.dtTo.AutoSelectDate = True
        Me.dtTo.BackColor = System.Drawing.SystemColors.Window
        '
        '
        '
        Me.dtTo.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.dtTo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.dtTo.ButtonClear.Image = CType(resources.GetObject("dtTo.ButtonClear.Image"), System.Drawing.Image)
        Me.dtTo.ButtonDropDown.Visible = True
        Me.dtTo.CustomFormat = "dd/MM/yyyy"
        Me.dtTo.FocusHighlightEnabled = True
        Me.dtTo.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtTo.Format = DevComponents.Editors.eDateTimePickerFormat.Custom
        Me.dtTo.IsPopupCalendarOpen = False
        Me.dtTo.Location = New System.Drawing.Point(286, 72)
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
        '
        '
        '
        Me.dtTo.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.dtTo.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90
        Me.dtTo.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.dtTo.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.dtTo.MonthCalendar.TodayButtonVisible = True
        Me.dtTo.Name = "dtTo"
        Me.dtTo.Size = New System.Drawing.Size(118, 25)
        Me.dtTo.TabIndex = 5
        Me.dtTo.Value = New Date(1900, 1, 1, 0, 0, 0, 0)
        Me.dtTo.WatermarkText = "To"
        '
        'PanelEx3
        '
        Me.PanelEx3.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.PanelEx3.Controls.Add(Me.btnGenerateMonthlyData)
        Me.PanelEx3.Controls.Add(Me.CircularProgress1)
        Me.PanelEx3.Controls.Add(Me.LabelX5)
        Me.PanelEx3.Controls.Add(Me.chkCoB)
        Me.PanelEx3.Controls.Add(Me.chkLetter)
        Me.PanelEx3.Controls.Add(Me.LabelX1)
        Me.PanelEx3.Controls.Add(Me.LabelX2)
        Me.PanelEx3.Controls.Add(Me.txtYear)
        Me.PanelEx3.Controls.Add(Me.btnPrint)
        Me.PanelEx3.Controls.Add(Me.LabelX3)
        Me.PanelEx3.Controls.Add(Me.dtFrom)
        Me.PanelEx3.Controls.Add(Me.dtTo)
        Me.PanelEx3.Controls.Add(Me.btnGenerateByDate)
        Me.PanelEx3.Controls.Add(Me.LabelX4)
        Me.PanelEx3.Controls.Add(Me.cmbMonth)
        Me.PanelEx3.DisabledBackColor = System.Drawing.Color.Empty
        Me.PanelEx3.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelEx3.Location = New System.Drawing.Point(0, 0)
        Me.PanelEx3.Name = "PanelEx3"
        Me.PanelEx3.Size = New System.Drawing.Size(998, 109)
        Me.PanelEx3.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx3.Style.GradientAngle = 90
        Me.PanelEx3.TabIndex = 26
        '
        'btnGenerateMonthlyData
        '
        Me.btnGenerateMonthlyData.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnGenerateMonthlyData.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnGenerateMonthlyData.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGenerateMonthlyData.Location = New System.Drawing.Point(424, 34)
        Me.btnGenerateMonthlyData.Name = "btnGenerateMonthlyData"
        Me.btnGenerateMonthlyData.Size = New System.Drawing.Size(118, 29)
        Me.btnGenerateMonthlyData.TabIndex = 52
        Me.btnGenerateMonthlyData.Text = "Generate"
        '
        'CircularProgress1
        '
        '
        '
        '
        Me.CircularProgress1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.CircularProgress1.FocusCuesEnabled = False
        Me.CircularProgress1.Location = New System.Drawing.Point(732, 27)
        Me.CircularProgress1.Name = "CircularProgress1"
        Me.CircularProgress1.ProgressColor = System.Drawing.Color.Red
        Me.CircularProgress1.ProgressTextVisible = True
        Me.CircularProgress1.Size = New System.Drawing.Size(128, 78)
        Me.CircularProgress1.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP
        Me.CircularProgress1.TabIndex = 50
        Me.CircularProgress1.TabStop = False
        '
        'LabelX5
        '
        Me.LabelX5.AutoSize = True
        '
        '
        '
        Me.LabelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX5.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX5.Location = New System.Drawing.Point(15, 7)
        Me.LabelX5.Name = "LabelX5"
        Me.LabelX5.Size = New System.Drawing.Size(43, 18)
        Me.LabelX5.TabIndex = 28
        Me.LabelX5.Text = "Format"
        '
        'chkCoB
        '
        '
        '
        '
        Me.chkCoB.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.chkCoB.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton
        Me.chkCoB.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCoB.Location = New System.Drawing.Point(286, 6)
        Me.chkCoB.Name = "chkCoB"
        Me.chkCoB.Size = New System.Drawing.Size(100, 23)
        Me.chkCoB.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.chkCoB.TabIndex = 27
        Me.chkCoB.Text = "CoB"
        '
        'chkLetter
        '
        '
        '
        '
        Me.chkLetter.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.chkLetter.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton
        Me.chkLetter.Checked = True
        Me.chkLetter.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkLetter.CheckValue = "Y"
        Me.chkLetter.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkLetter.Location = New System.Drawing.Point(106, 6)
        Me.chkLetter.Name = "chkLetter"
        Me.chkLetter.Size = New System.Drawing.Size(100, 23)
        Me.chkLetter.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.chkLetter.TabIndex = 26
        Me.chkLetter.Text = "Letter"
        '
        'ChalanTableBindingSource
        '
        Me.ChalanTableBindingSource.DataMember = "ChalanTable"
        Me.ChalanTableBindingSource.DataSource = Me.FingerPrintDataSet
        '
        'FingerPrintDataSet
        '
        Me.FingerPrintDataSet.DataSetName = "FingerPrintDataSet"
        Me.FingerPrintDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'bgwLetter
        '
        Me.bgwLetter.WorkerReportsProgress = True
        Me.bgwLetter.WorkerSupportsCancellation = True
        '
        'bgwCoB
        '
        Me.bgwCoB.WorkerReportsProgress = True
        Me.bgwCoB.WorkerSupportsCancellation = True
        '
        'ChalanTableTableAdapter1
        '
        Me.ChalanTableTableAdapter1.ClearBeforeFill = True
        '
        'PanelEx1
        '
        Me.PanelEx1.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.PanelEx1.Controls.Add(Me.LabelX13)
        Me.PanelEx1.Controls.Add(Me.LabelX12)
        Me.PanelEx1.Controls.Add(Me.LabelX11)
        Me.PanelEx1.Controls.Add(Me.LabelX10)
        Me.PanelEx1.Controls.Add(Me.lblAmount2)
        Me.PanelEx1.Controls.Add(Me.lblAmount3)
        Me.PanelEx1.Controls.Add(Me.lblAmount4)
        Me.PanelEx1.Controls.Add(Me.lblAmount1)
        Me.PanelEx1.DisabledBackColor = System.Drawing.Color.Empty
        Me.PanelEx1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelEx1.Location = New System.Drawing.Point(0, 441)
        Me.PanelEx1.Name = "PanelEx1"
        Me.PanelEx1.Size = New System.Drawing.Size(998, 104)
        Me.PanelEx1.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx1.Style.GradientAngle = 90
        Me.PanelEx1.TabIndex = 78
        '
        'PanelEx2
        '
        Me.PanelEx2.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.PanelEx2.Controls.Add(Me.dgvChalan)
        Me.PanelEx2.Controls.Add(Me.dgvSum)
        Me.PanelEx2.DisabledBackColor = System.Drawing.Color.Empty
        Me.PanelEx2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelEx2.Location = New System.Drawing.Point(0, 109)
        Me.PanelEx2.Name = "PanelEx2"
        Me.PanelEx2.Size = New System.Drawing.Size(998, 332)
        Me.PanelEx2.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx2.Style.GradientAngle = 90
        Me.PanelEx2.TabIndex = 82
        '
        'dgvChalan
        '
        Me.dgvChalan.AllowUserToAddRows = False
        Me.dgvChalan.AllowUserToDeleteRows = False
        Me.dgvChalan.AutoGenerateColumns = False
        Me.dgvChalan.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle25.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle25.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle25.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle25.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle25.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle25.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvChalan.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle25
        Me.dgvChalan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvChalan.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.HeadOfAccountDataGridViewTextBoxColumn, Me.TreasuryDataGridViewTextBoxColumn, Me.ChalanNumberDataGridViewTextBoxColumn, Me.ChalanDateDataGridViewTextBoxColumn, Me.AmountRemittedDataGridViewTextBoxColumn})
        Me.dgvChalan.DataSource = Me.ChalanTableBindingSource
        DataGridViewCellStyle28.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle28.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle28.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle28.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle28.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle28.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle28.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvChalan.DefaultCellStyle = DataGridViewCellStyle28
        Me.dgvChalan.Dock = System.Windows.Forms.DockStyle.Left
        Me.dgvChalan.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvChalan.EnableHeadersVisualStyles = False
        Me.dgvChalan.GridColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.dgvChalan.Location = New System.Drawing.Point(0, 0)
        Me.dgvChalan.MultiSelect = False
        Me.dgvChalan.Name = "dgvChalan"
        DataGridViewCellStyle29.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle29.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle29.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle29.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle29.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle29.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle29.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvChalan.RowHeadersDefaultCellStyle = DataGridViewCellStyle29
        Me.dgvChalan.RowHeadersWidth = 40
        Me.dgvChalan.SelectAllSignVisible = False
        Me.dgvChalan.Size = New System.Drawing.Size(659, 332)
        Me.dgvChalan.TabIndex = 51
        '
        'dgvSum
        '
        Me.dgvSum.AllowUserToAddRows = False
        Me.dgvSum.AllowUserToDeleteRows = False
        Me.dgvSum.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle30.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle30.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle30.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle30.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle30.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle30.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle30.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvSum.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle30
        Me.dgvSum.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSum.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2, Me.Column3, Me.Column4})
        DataGridViewCellStyle35.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle35.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle35.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle35.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle35.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle35.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle35.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvSum.DefaultCellStyle = DataGridViewCellStyle35
        Me.dgvSum.Dock = System.Windows.Forms.DockStyle.Right
        Me.dgvSum.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvSum.EnableHeadersVisualStyles = False
        Me.dgvSum.GridColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.dgvSum.Location = New System.Drawing.Point(668, 0)
        Me.dgvSum.MultiSelect = False
        Me.dgvSum.Name = "dgvSum"
        DataGridViewCellStyle36.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle36.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle36.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle36.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle36.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle36.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle36.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvSum.RowHeadersDefaultCellStyle = DataGridViewCellStyle36
        Me.dgvSum.RowHeadersVisible = False
        Me.dgvSum.SelectAllSignVisible = False
        Me.dgvSum.Size = New System.Drawing.Size(330, 332)
        Me.dgvSum.TabIndex = 53
        '
        'Column1
        '
        DataGridViewCellStyle31.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Column1.DefaultCellStyle = DataGridViewCellStyle31
        Me.Column1.HeaderText = "Month"
        Me.Column1.Name = "Column1"
        Me.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Column1.Width = 80
        '
        'Column2
        '
        DataGridViewCellStyle32.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Column2.DefaultCellStyle = DataGridViewCellStyle32
        Me.Column2.HeaderText = "Amount"
        Me.Column2.Name = "Column2"
        Me.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Column2.Width = 80
        '
        'Column3
        '
        DataGridViewCellStyle33.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Column3.DefaultCellStyle = DataGridViewCellStyle33
        Me.Column3.HeaderText = "Month"
        Me.Column3.Name = "Column3"
        Me.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Column3.Width = 80
        '
        'Column4
        '
        DataGridViewCellStyle34.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Column4.DefaultCellStyle = DataGridViewCellStyle34
        Me.Column4.HeaderText = "Amount"
        Me.Column4.Name = "Column4"
        Me.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Column4.Width = 80
        '
        'lblAmount1
        '
        '
        '
        '
        Me.lblAmount1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblAmount1.Location = New System.Drawing.Point(813, 10)
        Me.lblAmount1.Name = "lblAmount1"
        Me.lblAmount1.Size = New System.Drawing.Size(55, 18)
        Me.lblAmount1.TabIndex = 0
        Me.lblAmount1.Text = "Amount1"
        Me.lblAmount1.TextAlignment = System.Drawing.StringAlignment.Far
        '
        'lblAmount4
        '
        '
        '
        '
        Me.lblAmount4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblAmount4.Location = New System.Drawing.Point(813, 76)
        Me.lblAmount4.Name = "lblAmount4"
        Me.lblAmount4.Size = New System.Drawing.Size(55, 18)
        Me.lblAmount4.TabIndex = 1
        Me.lblAmount4.Text = "Amount4"
        Me.lblAmount4.TextAlignment = System.Drawing.StringAlignment.Far
        '
        'lblAmount3
        '
        '
        '
        '
        Me.lblAmount3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblAmount3.Location = New System.Drawing.Point(813, 54)
        Me.lblAmount3.Name = "lblAmount3"
        Me.lblAmount3.Size = New System.Drawing.Size(55, 18)
        Me.lblAmount3.TabIndex = 2
        Me.lblAmount3.Text = "Amount3"
        Me.lblAmount3.TextAlignment = System.Drawing.StringAlignment.Far
        '
        'lblAmount2
        '
        '
        '
        '
        Me.lblAmount2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblAmount2.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmount2.Location = New System.Drawing.Point(813, 32)
        Me.lblAmount2.Name = "lblAmount2"
        Me.lblAmount2.Size = New System.Drawing.Size(55, 18)
        Me.lblAmount2.TabIndex = 3
        Me.lblAmount2.Text = "Amount2"
        Me.lblAmount2.TextAlignment = System.Drawing.StringAlignment.Far
        '
        'LabelX10
        '
        '
        '
        '
        Me.LabelX10.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX10.Location = New System.Drawing.Point(574, 10)
        Me.LabelX10.Name = "LabelX10"
        Me.LabelX10.Size = New System.Drawing.Size(233, 18)
        Me.LabelX10.TabIndex = 4
        Me.LabelX10.Text = "Amount collected during the month:"
        Me.LabelX10.TextAlignment = System.Drawing.StringAlignment.Far
        '
        'LabelX11
        '
        '
        '
        '
        Me.LabelX11.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX11.Location = New System.Drawing.Point(404, 32)
        Me.LabelX11.Name = "LabelX11"
        Me.LabelX11.Size = New System.Drawing.Size(403, 18)
        Me.LabelX11.TabIndex = 5
        Me.LabelX11.Text = "Amount collected upto the previous month in current financial year:"
        Me.LabelX11.TextAlignment = System.Drawing.StringAlignment.Far
        '
        'LabelX12
        '
        '
        '
        '
        Me.LabelX12.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX12.Location = New System.Drawing.Point(668, 54)
        Me.LabelX12.Name = "LabelX12"
        Me.LabelX12.Size = New System.Drawing.Size(139, 18)
        Me.LabelX12.TabIndex = 6
        Me.LabelX12.Text = "Progressive Total:"
        Me.LabelX12.TextAlignment = System.Drawing.StringAlignment.Far
        '
        'LabelX13
        '
        '
        '
        '
        Me.LabelX13.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX13.Location = New System.Drawing.Point(404, 76)
        Me.LabelX13.Name = "LabelX13"
        Me.LabelX13.Size = New System.Drawing.Size(403, 18)
        Me.LabelX13.TabIndex = 7
        Me.LabelX13.Text = "Collection from April upto the month during the last financial year:"
        Me.LabelX13.TextAlignment = System.Drawing.StringAlignment.Far
        '
        'HeadOfAccountDataGridViewTextBoxColumn
        '
        Me.HeadOfAccountDataGridViewTextBoxColumn.DataPropertyName = "HeadOfAccount"
        Me.HeadOfAccountDataGridViewTextBoxColumn.HeaderText = "Head of Account"
        Me.HeadOfAccountDataGridViewTextBoxColumn.Name = "HeadOfAccountDataGridViewTextBoxColumn"
        '
        'TreasuryDataGridViewTextBoxColumn
        '
        Me.TreasuryDataGridViewTextBoxColumn.DataPropertyName = "Treasury"
        Me.TreasuryDataGridViewTextBoxColumn.HeaderText = "Treasury"
        Me.TreasuryDataGridViewTextBoxColumn.Name = "TreasuryDataGridViewTextBoxColumn"
        Me.TreasuryDataGridViewTextBoxColumn.Width = 170
        '
        'ChalanNumberDataGridViewTextBoxColumn
        '
        Me.ChalanNumberDataGridViewTextBoxColumn.DataPropertyName = "ChalanNumber"
        Me.ChalanNumberDataGridViewTextBoxColumn.HeaderText = "Chalan Number"
        Me.ChalanNumberDataGridViewTextBoxColumn.Name = "ChalanNumberDataGridViewTextBoxColumn"
        Me.ChalanNumberDataGridViewTextBoxColumn.Width = 150
        '
        'ChalanDateDataGridViewTextBoxColumn
        '
        Me.ChalanDateDataGridViewTextBoxColumn.DataPropertyName = "ChalanDate"
        DataGridViewCellStyle26.Format = "dd/MM/yyyy"
        Me.ChalanDateDataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle26
        Me.ChalanDateDataGridViewTextBoxColumn.HeaderText = "Chalan Date"
        Me.ChalanDateDataGridViewTextBoxColumn.Name = "ChalanDateDataGridViewTextBoxColumn"
        Me.ChalanDateDataGridViewTextBoxColumn.Width = 80
        '
        'AmountRemittedDataGridViewTextBoxColumn
        '
        Me.AmountRemittedDataGridViewTextBoxColumn.DataPropertyName = "AmountRemitted"
        DataGridViewCellStyle27.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.AmountRemittedDataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle27
        Me.AmountRemittedDataGridViewTextBoxColumn.HeaderText = "Amount"
        Me.AmountRemittedDataGridViewTextBoxColumn.Name = "AmountRemittedDataGridViewTextBoxColumn"
        Me.AmountRemittedDataGridViewTextBoxColumn.Width = 70
        '
        'frmFPAStatement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(998, 545)
        Me.Controls.Add(Me.PanelEx2)
        Me.Controls.Add(Me.PanelEx1)
        Me.Controls.Add(Me.PanelEx3)
        Me.DoubleBuffered = True
        Me.EnableGlass = False
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmFPAStatement"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Revenue Income Statement"
        Me.TitleText = "<b>FP Attestation Statement</b>"
        CType(Me.txtYear, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtFrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtTo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelEx3.ResumeLayout(False)
        Me.PanelEx3.PerformLayout()
        CType(Me.ChalanTableBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FingerPrintDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelEx1.ResumeLayout(False)
        Me.PanelEx2.ResumeLayout(False)
        CType(Me.dgvChalan, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvSum, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnPrint As DevComponents.DotNetBar.ButtonX
    Friend WithEvents LabelX4 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX3 As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtYear As DevComponents.Editors.IntegerInput
    Friend WithEvents cmbMonth As DevComponents.DotNetBar.Controls.ComboBoxEx
    Friend WithEvents LabelX2 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents btnGenerateByDate As DevComponents.DotNetBar.ButtonX
    Friend WithEvents dtFrom As DevComponents.Editors.DateTimeAdv.DateTimeInput
    Friend WithEvents dtTo As DevComponents.Editors.DateTimeAdv.DateTimeInput
    Friend WithEvents PanelEx3 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents LabelX5 As DevComponents.DotNetBar.LabelX
    Friend WithEvents chkCoB As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents chkLetter As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents CircularProgress1 As DevComponents.DotNetBar.Controls.CircularProgress
    Friend WithEvents bgwLetter As System.ComponentModel.BackgroundWorker
    Friend WithEvents bgwCoB As System.ComponentModel.BackgroundWorker
    Friend WithEvents ChalanTableTableAdapter1 As FingerprintInformationSystem.FingerPrintDataSetTableAdapters.ChalanTableTableAdapter
    Friend WithEvents btnGenerateMonthlyData As DevComponents.DotNetBar.ButtonX
    Friend WithEvents FingerPrintDataSet As FingerprintInformationSystem.FingerPrintDataSet
    Friend WithEvents ChalanTableBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DateValueDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Expr2DataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PanelEx1 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents PanelEx2 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents dgvChalan As DevComponents.DotNetBar.Controls.DataGridViewX
    Friend WithEvents dgvSum As DevComponents.DotNetBar.Controls.DataGridViewX
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LabelX13 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX12 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX11 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX10 As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblAmount2 As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblAmount3 As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblAmount4 As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblAmount1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents HeadOfAccountDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TreasuryDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ChalanNumberDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ChalanDateDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AmountRemittedDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
