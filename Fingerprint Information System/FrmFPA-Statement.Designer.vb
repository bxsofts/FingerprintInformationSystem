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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.LabelX2 = New DevComponents.DotNetBar.LabelX()
        Me.btnGenerateReport = New DevComponents.DotNetBar.ButtonX()
        Me.LabelX1 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX4 = New DevComponents.DotNetBar.LabelX()
        Me.btnGenerateByDate = New DevComponents.DotNetBar.ButtonX()
        Me.LabelX3 = New DevComponents.DotNetBar.LabelX()
        Me.txtYear = New DevComponents.Editors.IntegerInput()
        Me.dtFrom = New DevComponents.Editors.DateTimeAdv.DateTimeInput()
        Me.cmbMonth = New DevComponents.DotNetBar.Controls.ComboBoxEx()
        Me.dtTo = New DevComponents.Editors.DateTimeAdv.DateTimeInput()
        Me.PanelEx3 = New DevComponents.DotNetBar.PanelEx()
        Me.PanelEx5 = New DevComponents.DotNetBar.PanelEx()
        Me.btnGenerateMonthlyData = New DevComponents.DotNetBar.ButtonX()
        Me.PanelEx4 = New DevComponents.DotNetBar.PanelEx()
        Me.LabelX5 = New DevComponents.DotNetBar.LabelX()
        Me.chkiAPS = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.btnOpenFolder = New DevComponents.DotNetBar.ButtonX()
        Me.chkExcel = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.chkLetter = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.PanelEx1 = New DevComponents.DotNetBar.PanelEx()
        Me.LabelX13 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX12 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX11 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX10 = New DevComponents.DotNetBar.LabelX()
        Me.lblAmount2 = New DevComponents.DotNetBar.LabelX()
        Me.lblAmount3 = New DevComponents.DotNetBar.LabelX()
        Me.lblAmount4 = New DevComponents.DotNetBar.LabelX()
        Me.lblAmount1 = New DevComponents.DotNetBar.LabelX()
        Me.PanelEx2 = New DevComponents.DotNetBar.PanelEx()
        Me.btnEdit = New DevComponents.DotNetBar.ButtonX()
        Me.btnSave = New DevComponents.DotNetBar.ButtonX()
        Me.dgvChalan = New DevComponents.DotNetBar.Controls.DataGridViewX()
        Me.HeadOfAccountDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TreasuryDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ChalanNumberDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ChalanDateDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AmountRemittedDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ChalanTableBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.FingerPrintDataSet = New FingerprintInformationSystem.FingerPrintDataSet()
        Me.dgvSum = New DevComponents.DotNetBar.Controls.DataGridViewX()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ChalanTableTableAdapter1 = New FingerprintInformationSystem.FingerPrintDataSetTableAdapters.ChalanTableTableAdapter()
        Me.RevenueCollectionTableAdapter1 = New FingerprintInformationSystem.FingerPrintDataSetTableAdapters.RevenueCollectionTableAdapter()
        CType(Me.txtYear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtTo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelEx3.SuspendLayout()
        Me.PanelEx5.SuspendLayout()
        Me.PanelEx4.SuspendLayout()
        Me.PanelEx1.SuspendLayout()
        Me.PanelEx2.SuspendLayout()
        CType(Me.dgvChalan, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChalanTableBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FingerPrintDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.LabelX2.Location = New System.Drawing.Point(243, 50)
        Me.LabelX2.Name = "LabelX2"
        Me.LabelX2.Size = New System.Drawing.Size(17, 18)
        Me.LabelX2.TabIndex = 8
        Me.LabelX2.Text = "To"
        '
        'btnGenerateReport
        '
        Me.btnGenerateReport.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnGenerateReport.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnGenerateReport.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGenerateReport.Location = New System.Drawing.Point(114, 32)
        Me.btnGenerateReport.Name = "btnGenerateReport"
        Me.btnGenerateReport.Size = New System.Drawing.Size(97, 42)
        Me.btnGenerateReport.TabIndex = 7
        Me.btnGenerateReport.Text = "Generate Statement"
        '
        'LabelX1
        '
        Me.LabelX1.AutoSize = True
        '
        '
        '
        Me.LabelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX1.Location = New System.Drawing.Point(13, 50)
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
        Me.LabelX4.Location = New System.Drawing.Point(243, 13)
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
        Me.btnGenerateByDate.Location = New System.Drawing.Point(422, 45)
        Me.btnGenerateByDate.Name = "btnGenerateByDate"
        Me.btnGenerateByDate.Size = New System.Drawing.Size(94, 29)
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
        Me.LabelX3.Location = New System.Drawing.Point(13, 13)
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
        Me.txtYear.Location = New System.Drawing.Point(284, 10)
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
        Me.dtFrom.Location = New System.Drawing.Point(104, 47)
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
        Me.cmbMonth.Location = New System.Drawing.Point(104, 9)
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
        Me.dtTo.Location = New System.Drawing.Point(284, 47)
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
        Me.PanelEx3.Controls.Add(Me.PanelEx5)
        Me.PanelEx3.Controls.Add(Me.PanelEx4)
        Me.PanelEx3.DisabledBackColor = System.Drawing.Color.Empty
        Me.PanelEx3.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelEx3.Location = New System.Drawing.Point(0, 0)
        Me.PanelEx3.Name = "PanelEx3"
        Me.PanelEx3.Size = New System.Drawing.Size(998, 87)
        Me.PanelEx3.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx3.Style.GradientAngle = 90
        Me.PanelEx3.TabIndex = 26
        '
        'PanelEx5
        '
        Me.PanelEx5.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx5.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.PanelEx5.Controls.Add(Me.LabelX3)
        Me.PanelEx5.Controls.Add(Me.btnGenerateMonthlyData)
        Me.PanelEx5.Controls.Add(Me.cmbMonth)
        Me.PanelEx5.Controls.Add(Me.LabelX1)
        Me.PanelEx5.Controls.Add(Me.LabelX4)
        Me.PanelEx5.Controls.Add(Me.LabelX2)
        Me.PanelEx5.Controls.Add(Me.btnGenerateByDate)
        Me.PanelEx5.Controls.Add(Me.txtYear)
        Me.PanelEx5.Controls.Add(Me.dtTo)
        Me.PanelEx5.Controls.Add(Me.dtFrom)
        Me.PanelEx5.DisabledBackColor = System.Drawing.Color.Empty
        Me.PanelEx5.Location = New System.Drawing.Point(3, 4)
        Me.PanelEx5.Name = "PanelEx5"
        Me.PanelEx5.Size = New System.Drawing.Size(639, 79)
        Me.PanelEx5.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx5.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx5.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.PanelEx5.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx5.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx5.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx5.Style.GradientAngle = 90
        Me.PanelEx5.TabIndex = 58
        '
        'btnGenerateMonthlyData
        '
        Me.btnGenerateMonthlyData.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnGenerateMonthlyData.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnGenerateMonthlyData.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGenerateMonthlyData.Location = New System.Drawing.Point(422, 8)
        Me.btnGenerateMonthlyData.Name = "btnGenerateMonthlyData"
        Me.btnGenerateMonthlyData.Size = New System.Drawing.Size(94, 29)
        Me.btnGenerateMonthlyData.TabIndex = 3
        Me.btnGenerateMonthlyData.Text = "Generate"
        '
        'PanelEx4
        '
        Me.PanelEx4.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.PanelEx4.Controls.Add(Me.LabelX5)
        Me.PanelEx4.Controls.Add(Me.chkiAPS)
        Me.PanelEx4.Controls.Add(Me.btnOpenFolder)
        Me.PanelEx4.Controls.Add(Me.btnGenerateReport)
        Me.PanelEx4.Controls.Add(Me.chkExcel)
        Me.PanelEx4.Controls.Add(Me.chkLetter)
        Me.PanelEx4.DisabledBackColor = System.Drawing.Color.Empty
        Me.PanelEx4.Location = New System.Drawing.Point(648, 4)
        Me.PanelEx4.Name = "PanelEx4"
        Me.PanelEx4.Size = New System.Drawing.Size(347, 79)
        Me.PanelEx4.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx4.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx4.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.PanelEx4.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx4.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx4.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx4.Style.GradientAngle = 90
        Me.PanelEx4.TabIndex = 59
        '
        'LabelX5
        '
        Me.LabelX5.AutoSize = True
        '
        '
        '
        Me.LabelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX5.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX5.Location = New System.Drawing.Point(3, 7)
        Me.LabelX5.Name = "LabelX5"
        Me.LabelX5.Size = New System.Drawing.Size(105, 18)
        Me.LabelX5.TabIndex = 54
        Me.LabelX5.Text = "Statement Format"
        '
        'chkiAPS
        '
        Me.chkiAPS.AutoSize = True
        '
        '
        '
        Me.chkiAPS.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.chkiAPS.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton
        Me.chkiAPS.Checked = True
        Me.chkiAPS.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkiAPS.CheckValue = "Y"
        Me.chkiAPS.FocusCuesEnabled = False
        Me.chkiAPS.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkiAPS.Location = New System.Drawing.Point(114, 7)
        Me.chkiAPS.Name = "chkiAPS"
        Me.chkiAPS.Size = New System.Drawing.Size(48, 18)
        Me.chkiAPS.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.chkiAPS.TabIndex = 27
        Me.chkiAPS.TabStop = False
        Me.chkiAPS.Text = "iAPS"
        '
        'btnOpenFolder
        '
        Me.btnOpenFolder.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnOpenFolder.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnOpenFolder.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOpenFolder.Location = New System.Drawing.Point(237, 32)
        Me.btnOpenFolder.Name = "btnOpenFolder"
        Me.btnOpenFolder.Size = New System.Drawing.Size(97, 42)
        Me.btnOpenFolder.TabIndex = 8
        Me.btnOpenFolder.Text = "Open Excel Folder"
        '
        'chkExcel
        '
        Me.chkExcel.AutoSize = True
        '
        '
        '
        Me.chkExcel.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.chkExcel.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton
        Me.chkExcel.FocusCuesEnabled = False
        Me.chkExcel.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkExcel.Location = New System.Drawing.Point(286, 7)
        Me.chkExcel.Name = "chkExcel"
        Me.chkExcel.Size = New System.Drawing.Size(51, 18)
        Me.chkExcel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.chkExcel.TabIndex = 53
        Me.chkExcel.TabStop = False
        Me.chkExcel.Text = "Excel"
        '
        'chkLetter
        '
        Me.chkLetter.AutoSize = True
        '
        '
        '
        Me.chkLetter.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.chkLetter.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton
        Me.chkLetter.FocusCuesEnabled = False
        Me.chkLetter.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkLetter.Location = New System.Drawing.Point(196, 7)
        Me.chkLetter.Name = "chkLetter"
        Me.chkLetter.Size = New System.Drawing.Size(56, 18)
        Me.chkLetter.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.chkLetter.TabIndex = 26
        Me.chkLetter.TabStop = False
        Me.chkLetter.Text = "Letter"
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
        'PanelEx2
        '
        Me.PanelEx2.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.PanelEx2.Controls.Add(Me.btnEdit)
        Me.PanelEx2.Controls.Add(Me.btnSave)
        Me.PanelEx2.Controls.Add(Me.dgvChalan)
        Me.PanelEx2.Controls.Add(Me.dgvSum)
        Me.PanelEx2.DisabledBackColor = System.Drawing.Color.Empty
        Me.PanelEx2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelEx2.Location = New System.Drawing.Point(0, 87)
        Me.PanelEx2.Name = "PanelEx2"
        Me.PanelEx2.Size = New System.Drawing.Size(998, 354)
        Me.PanelEx2.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx2.Style.GradientAngle = 90
        Me.PanelEx2.TabIndex = 82
        '
        'btnEdit
        '
        Me.btnEdit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnEdit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnEdit.Location = New System.Drawing.Point(806, 316)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(85, 32)
        Me.btnEdit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnEdit.TabIndex = 55
        Me.btnEdit.TabStop = False
        Me.btnEdit.Text = "Edit"
        '
        'btnSave
        '
        Me.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnSave.Location = New System.Drawing.Point(904, 316)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(85, 32)
        Me.btnSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnSave.TabIndex = 54
        Me.btnSave.TabStop = False
        Me.btnSave.Text = "Save"
        '
        'dgvChalan
        '
        Me.dgvChalan.AllowUserToAddRows = False
        Me.dgvChalan.AllowUserToDeleteRows = False
        Me.dgvChalan.AutoGenerateColumns = False
        Me.dgvChalan.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvChalan.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvChalan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvChalan.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.HeadOfAccountDataGridViewTextBoxColumn, Me.TreasuryDataGridViewTextBoxColumn, Me.ChalanNumberDataGridViewTextBoxColumn, Me.ChalanDateDataGridViewTextBoxColumn, Me.AmountRemittedDataGridViewTextBoxColumn})
        Me.dgvChalan.DataSource = Me.ChalanTableBindingSource
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvChalan.DefaultCellStyle = DataGridViewCellStyle4
        Me.dgvChalan.Dock = System.Windows.Forms.DockStyle.Left
        Me.dgvChalan.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvChalan.EnableHeadersVisualStyles = False
        Me.dgvChalan.GridColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.dgvChalan.Location = New System.Drawing.Point(0, 0)
        Me.dgvChalan.MultiSelect = False
        Me.dgvChalan.Name = "dgvChalan"
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvChalan.RowHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.dgvChalan.RowHeadersWidth = 40
        Me.dgvChalan.SelectAllSignVisible = False
        Me.dgvChalan.Size = New System.Drawing.Size(642, 354)
        Me.dgvChalan.TabIndex = 51
        '
        'HeadOfAccountDataGridViewTextBoxColumn
        '
        Me.HeadOfAccountDataGridViewTextBoxColumn.DataPropertyName = "HeadOfAccount"
        Me.HeadOfAccountDataGridViewTextBoxColumn.HeaderText = "Head of A/C"
        Me.HeadOfAccountDataGridViewTextBoxColumn.Name = "HeadOfAccountDataGridViewTextBoxColumn"
        Me.HeadOfAccountDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'TreasuryDataGridViewTextBoxColumn
        '
        Me.TreasuryDataGridViewTextBoxColumn.DataPropertyName = "Treasury"
        Me.TreasuryDataGridViewTextBoxColumn.HeaderText = "Treasury"
        Me.TreasuryDataGridViewTextBoxColumn.Name = "TreasuryDataGridViewTextBoxColumn"
        Me.TreasuryDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.TreasuryDataGridViewTextBoxColumn.Width = 170
        '
        'ChalanNumberDataGridViewTextBoxColumn
        '
        Me.ChalanNumberDataGridViewTextBoxColumn.DataPropertyName = "ChalanNumber"
        Me.ChalanNumberDataGridViewTextBoxColumn.HeaderText = "Chalan Number"
        Me.ChalanNumberDataGridViewTextBoxColumn.Name = "ChalanNumberDataGridViewTextBoxColumn"
        Me.ChalanNumberDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.ChalanNumberDataGridViewTextBoxColumn.Width = 150
        '
        'ChalanDateDataGridViewTextBoxColumn
        '
        Me.ChalanDateDataGridViewTextBoxColumn.DataPropertyName = "ChalanDate"
        DataGridViewCellStyle2.Format = "dd/MM/yyyy"
        Me.ChalanDateDataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle2
        Me.ChalanDateDataGridViewTextBoxColumn.HeaderText = "Chalan Date"
        Me.ChalanDateDataGridViewTextBoxColumn.Name = "ChalanDateDataGridViewTextBoxColumn"
        Me.ChalanDateDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.ChalanDateDataGridViewTextBoxColumn.Width = 80
        '
        'AmountRemittedDataGridViewTextBoxColumn
        '
        Me.AmountRemittedDataGridViewTextBoxColumn.DataPropertyName = "AmountRemitted"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.AmountRemittedDataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle3
        Me.AmountRemittedDataGridViewTextBoxColumn.HeaderText = "Amount"
        Me.AmountRemittedDataGridViewTextBoxColumn.Name = "AmountRemittedDataGridViewTextBoxColumn"
        Me.AmountRemittedDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.AmountRemittedDataGridViewTextBoxColumn.Width = 70
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
        'dgvSum
        '
        Me.dgvSum.AllowUserToAddRows = False
        Me.dgvSum.AllowUserToDeleteRows = False
        Me.dgvSum.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvSum.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.dgvSum.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSum.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2, Me.Column3, Me.Column4})
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle11.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle11.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvSum.DefaultCellStyle = DataGridViewCellStyle11
        Me.dgvSum.Dock = System.Windows.Forms.DockStyle.Right
        Me.dgvSum.EnableHeadersVisualStyles = False
        Me.dgvSum.GridColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.dgvSum.Location = New System.Drawing.Point(648, 0)
        Me.dgvSum.MultiSelect = False
        Me.dgvSum.Name = "dgvSum"
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle12.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle12.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvSum.RowHeadersDefaultCellStyle = DataGridViewCellStyle12
        Me.dgvSum.RowHeadersVisible = False
        Me.dgvSum.SelectAllSignVisible = False
        Me.dgvSum.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvSum.Size = New System.Drawing.Size(350, 354)
        Me.dgvSum.TabIndex = 53
        '
        'Column1
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Column1.DefaultCellStyle = DataGridViewCellStyle7
        Me.Column1.HeaderText = "Month"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Column2
        '
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Column2.DefaultCellStyle = DataGridViewCellStyle8
        Me.Column2.HeaderText = "Amount"
        Me.Column2.Name = "Column2"
        Me.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Column2.Width = 70
        '
        'Column3
        '
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Column3.DefaultCellStyle = DataGridViewCellStyle9
        Me.Column3.HeaderText = "Month"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        Me.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Column4
        '
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Column4.DefaultCellStyle = DataGridViewCellStyle10
        Me.Column4.HeaderText = "Amount"
        Me.Column4.Name = "Column4"
        Me.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Column4.Width = 70
        '
        'ChalanTableTableAdapter1
        '
        Me.ChalanTableTableAdapter1.ClearBeforeFill = True
        '
        'RevenueCollectionTableAdapter1
        '
        Me.RevenueCollectionTableAdapter1.ClearBeforeFill = True
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
        Me.Text = "Revenue Collection Statement"
        Me.TitleText = "<b>Revenue Collection Statement</b>"
        CType(Me.txtYear, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtFrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtTo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelEx3.ResumeLayout(False)
        Me.PanelEx5.ResumeLayout(False)
        Me.PanelEx5.PerformLayout()
        Me.PanelEx4.ResumeLayout(False)
        Me.PanelEx4.PerformLayout()
        Me.PanelEx1.ResumeLayout(False)
        Me.PanelEx2.ResumeLayout(False)
        CType(Me.dgvChalan, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChalanTableBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FingerPrintDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvSum, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnGenerateReport As DevComponents.DotNetBar.ButtonX
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
    Friend WithEvents chkiAPS As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents chkLetter As DevComponents.DotNetBar.Controls.CheckBoxX
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
    Friend WithEvents LabelX13 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX12 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX11 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX10 As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblAmount2 As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblAmount3 As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblAmount4 As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblAmount1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents chkExcel As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents btnOpenFolder As DevComponents.DotNetBar.ButtonX
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents HeadOfAccountDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TreasuryDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ChalanNumberDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ChalanDateDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AmountRemittedDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RevenueCollectionTableAdapter1 As FingerprintInformationSystem.FingerPrintDataSetTableAdapters.RevenueCollectionTableAdapter
    Friend WithEvents btnSave As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnEdit As DevComponents.DotNetBar.ButtonX
    Friend WithEvents PanelEx5 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents PanelEx4 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents LabelX5 As DevComponents.DotNetBar.LabelX
End Class
