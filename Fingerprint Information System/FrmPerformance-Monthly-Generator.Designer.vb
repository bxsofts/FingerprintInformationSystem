<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMonthlyPerformance
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMonthlyPerformance))
        Dim DataGridViewCellStyle19 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle26 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle27 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle20 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle21 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle22 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle23 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle24 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle25 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.PanelEx1 = New DevComponents.DotNetBar.PanelEx()
        Me.lblHeader = New DevComponents.DotNetBar.LabelX()
        Me.txtBlankCellValue = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.btnInsertBlankValues = New DevComponents.DotNetBar.ButtonX()
        Me.btnClearAllFields = New DevComponents.DotNetBar.ButtonX()
        Me.LabelX4 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX3 = New DevComponents.DotNetBar.LabelX()
        Me.txtYear = New DevComponents.Editors.IntegerInput()
        Me.cmbMonth = New DevComponents.DotNetBar.Controls.ComboBoxEx()
        Me.btnGenerateMonth1Values = New DevComponents.DotNetBar.ButtonX()
        Me.btnGeneratePreviousMonthValues = New DevComponents.DotNetBar.ButtonX()
        Me.PanelEx2 = New DevComponents.DotNetBar.PanelEx()
        Me.GroupPanel8 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.LabelX2 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX1 = New DevComponents.DotNetBar.LabelX()
        Me.dtFrom = New DevComponents.Editors.DateTimeAdv.DateTimeInput()
        Me.dtTo = New DevComponents.Editors.DateTimeAdv.DateTimeInput()
        Me.btnGenerateSelectedPeriodValues = New DevComponents.DotNetBar.ButtonX()
        Me.GroupPanel4 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.CheckBoxX2 = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.chkBlankValue = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.GroupPanel3 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.chkGeneratePreviousMonthValuesFromFile = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.chkGeneratePreviousMonthValuesFromDB = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.GroupPanel2 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.chkGenerateMonth1ValuesFromFile = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.chkGenerateMonth1ValuesFromDB = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.GroupPanel1 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.btnGeneratePerformanceStatement = New DevComponents.DotNetBar.ButtonX()
        Me.PerformanceBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.FingerPrintDataSet = New FingerprintInformationSystem.FingerPrintDataSet()
        Me.FPARegisterTableAdapter = New FingerprintInformationSystem.FingerPrintDataSetTableAdapters.FPAttestationRegisterTableAdapter()
        Me.SOCRegisterTableAdapter = New FingerprintInformationSystem.FingerPrintDataSetTableAdapters.SOCRegisterTableAdapter()
        Me.DaRegisterTableAdapter = New FingerprintInformationSystem.FingerPrintDataSetTableAdapters.DARegisterTableAdapter()
        Me.PerformanceTableAdapter = New FingerprintInformationSystem.FingerPrintDataSetTableAdapters.PerformanceTableAdapter()
        Me.CdRegisterTableAdapter = New FingerprintInformationSystem.FingerPrintDataSetTableAdapters.CDRegisterTableAdapter()
        Me.PanelEx3 = New DevComponents.DotNetBar.PanelEx()
        Me.CircularProgress1 = New DevComponents.DotNetBar.Controls.CircularProgress()
        Me.btnOpenFolder = New DevComponents.DotNetBar.ButtonX()
        Me.lblPreviousMonth = New DevComponents.DotNetBar.LabelX()
        Me.btnOpenInWord = New DevComponents.DotNetBar.ButtonX()
        Me.lblMonth1 = New DevComponents.DotNetBar.LabelX()
        Me.DataGridViewX1 = New DevComponents.DotNetBar.Controls.DataGridViewX()
        Me.SlNoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DetailsOfWorkDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PreviousDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Month1DataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Month2DataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Month3DataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PresentDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RemarksDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.bgwStatement = New System.ComponentModel.BackgroundWorker()
        Me.PanelEx1.SuspendLayout()
        CType(Me.txtYear, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelEx2.SuspendLayout()
        Me.GroupPanel8.SuspendLayout()
        CType(Me.dtFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtTo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupPanel4.SuspendLayout()
        Me.GroupPanel3.SuspendLayout()
        Me.GroupPanel2.SuspendLayout()
        Me.GroupPanel1.SuspendLayout()
        CType(Me.PerformanceBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FingerPrintDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelEx3.SuspendLayout()
        CType(Me.DataGridViewX1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelEx1
        '
        Me.PanelEx1.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.PanelEx1.Controls.Add(Me.lblHeader)
        Me.PanelEx1.DisabledBackColor = System.Drawing.Color.Empty
        Me.PanelEx1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelEx1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PanelEx1.Location = New System.Drawing.Point(377, 0)
        Me.PanelEx1.Name = "PanelEx1"
        Me.PanelEx1.Size = New System.Drawing.Size(977, 45)
        Me.PanelEx1.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx1.Style.GradientAngle = 90
        Me.PanelEx1.TabIndex = 1
        '
        'lblHeader
        '
        '
        '
        '
        Me.lblHeader.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblHeader.Font = New System.Drawing.Font("Segoe UI", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.Location = New System.Drawing.Point(0, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(977, 45)
        Me.lblHeader.TabIndex = 34
        Me.lblHeader.Text = "STATEMENT OF PERFORMANCE"
        Me.lblHeader.TextAlignment = System.Drawing.StringAlignment.Center
        '
        'txtBlankCellValue
        '
        Me.txtBlankCellValue.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtBlankCellValue.Border.Class = "TextBoxBorder"
        Me.txtBlankCellValue.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtBlankCellValue.DisabledBackColor = System.Drawing.Color.White
        Me.txtBlankCellValue.FocusHighlightEnabled = True
        Me.txtBlankCellValue.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBlankCellValue.ForeColor = System.Drawing.Color.Black
        Me.txtBlankCellValue.Location = New System.Drawing.Point(129, 7)
        Me.txtBlankCellValue.Name = "txtBlankCellValue"
        Me.txtBlankCellValue.Size = New System.Drawing.Size(82, 27)
        Me.txtBlankCellValue.TabIndex = 3
        '
        'btnInsertBlankValues
        '
        Me.btnInsertBlankValues.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnInsertBlankValues.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnInsertBlankValues.Location = New System.Drawing.Point(249, 6)
        Me.btnInsertBlankValues.Name = "btnInsertBlankValues"
        Me.btnInsertBlankValues.Size = New System.Drawing.Size(99, 58)
        Me.btnInsertBlankValues.TabIndex = 4
        Me.btnInsertBlankValues.Text = "INSERT"
        '
        'btnClearAllFields
        '
        Me.btnClearAllFields.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnClearAllFields.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnClearAllFields.Image = CType(resources.GetObject("btnClearAllFields.Image"), System.Drawing.Image)
        Me.btnClearAllFields.Location = New System.Drawing.Point(12, 15)
        Me.btnClearAllFields.Name = "btnClearAllFields"
        Me.btnClearAllFields.Size = New System.Drawing.Size(126, 56)
        Me.btnClearAllFields.TabIndex = 10
        Me.btnClearAllFields.Text = "CLEAR"
        '
        'LabelX4
        '
        Me.LabelX4.AutoSize = True
        '
        '
        '
        Me.LabelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX4.Location = New System.Drawing.Point(3, 54)
        Me.LabelX4.Name = "LabelX4"
        Me.LabelX4.Size = New System.Drawing.Size(28, 18)
        Me.LabelX4.TabIndex = 30
        Me.LabelX4.Text = "Year"
        '
        'LabelX3
        '
        Me.LabelX3.AutoSize = True
        '
        '
        '
        Me.LabelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX3.Location = New System.Drawing.Point(3, 19)
        Me.LabelX3.Name = "LabelX3"
        Me.LabelX3.Size = New System.Drawing.Size(40, 18)
        Me.LabelX3.TabIndex = 29
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
        Me.txtYear.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtYear.InputHorizontalAlignment = DevComponents.Editors.eHorizontalAlignment.Left
        Me.txtYear.Location = New System.Drawing.Point(50, 48)
        Me.txtYear.MaxValue = 2099
        Me.txtYear.MinValue = 1900
        Me.txtYear.Name = "txtYear"
        Me.txtYear.ShowUpDown = True
        Me.txtYear.Size = New System.Drawing.Size(119, 29)
        Me.txtYear.TabIndex = 1
        Me.txtYear.Value = 1900
        Me.txtYear.WatermarkText = "Year"
        '
        'cmbMonth
        '
        Me.cmbMonth.DisplayMember = "Text"
        Me.cmbMonth.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMonth.FocusHighlightEnabled = True
        Me.cmbMonth.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbMonth.ForeColor = System.Drawing.Color.Black
        Me.cmbMonth.FormattingEnabled = True
        Me.cmbMonth.ItemHeight = 23
        Me.cmbMonth.Location = New System.Drawing.Point(50, 13)
        Me.cmbMonth.MaxDropDownItems = 15
        Me.cmbMonth.MaxLength = 255
        Me.cmbMonth.Name = "cmbMonth"
        Me.cmbMonth.Size = New System.Drawing.Size(119, 29)
        Me.cmbMonth.TabIndex = 0
        Me.cmbMonth.WatermarkText = "Month"
        '
        'btnGenerateMonth1Values
        '
        Me.btnGenerateMonth1Values.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnGenerateMonth1Values.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnGenerateMonth1Values.Location = New System.Drawing.Point(249, 6)
        Me.btnGenerateMonth1Values.Name = "btnGenerateMonth1Values"
        Me.btnGenerateMonth1Values.Size = New System.Drawing.Size(99, 58)
        Me.btnGenerateMonth1Values.TabIndex = 5
        Me.btnGenerateMonth1Values.Text = "GENERATE"
        '
        'btnGeneratePreviousMonthValues
        '
        Me.btnGeneratePreviousMonthValues.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnGeneratePreviousMonthValues.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnGeneratePreviousMonthValues.Location = New System.Drawing.Point(249, 6)
        Me.btnGeneratePreviousMonthValues.Name = "btnGeneratePreviousMonthValues"
        Me.btnGeneratePreviousMonthValues.Size = New System.Drawing.Size(99, 58)
        Me.btnGeneratePreviousMonthValues.TabIndex = 6
        Me.btnGeneratePreviousMonthValues.Text = "GENERATE"
        '
        'PanelEx2
        '
        Me.PanelEx2.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.PanelEx2.Controls.Add(Me.GroupPanel8)
        Me.PanelEx2.Controls.Add(Me.GroupPanel4)
        Me.PanelEx2.Controls.Add(Me.GroupPanel3)
        Me.PanelEx2.Controls.Add(Me.GroupPanel2)
        Me.PanelEx2.Controls.Add(Me.GroupPanel1)
        Me.PanelEx2.DisabledBackColor = System.Drawing.Color.Empty
        Me.PanelEx2.Dock = System.Windows.Forms.DockStyle.Left
        Me.PanelEx2.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PanelEx2.Location = New System.Drawing.Point(0, 0)
        Me.PanelEx2.Name = "PanelEx2"
        Me.PanelEx2.Size = New System.Drawing.Size(377, 733)
        Me.PanelEx2.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx2.Style.GradientAngle = 90
        Me.PanelEx2.TabIndex = 2
        '
        'GroupPanel8
        '
        Me.GroupPanel8.BackColor = System.Drawing.Color.Transparent
        Me.GroupPanel8.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanel8.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanel8.Controls.Add(Me.LabelX2)
        Me.GroupPanel8.Controls.Add(Me.LabelX1)
        Me.GroupPanel8.Controls.Add(Me.dtFrom)
        Me.GroupPanel8.Controls.Add(Me.dtTo)
        Me.GroupPanel8.Controls.Add(Me.btnGenerateSelectedPeriodValues)
        Me.GroupPanel8.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel8.Location = New System.Drawing.Point(7, 566)
        Me.GroupPanel8.Name = "GroupPanel8"
        Me.GroupPanel8.Size = New System.Drawing.Size(360, 93)
        '
        '
        '
        Me.GroupPanel8.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.GroupPanel8.Style.BackColorGradientAngle = 90
        Me.GroupPanel8.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.GroupPanel8.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel8.Style.BorderBottomWidth = 1
        Me.GroupPanel8.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.GroupPanel8.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel8.Style.BorderLeftWidth = 1
        Me.GroupPanel8.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel8.Style.BorderRightWidth = 1
        Me.GroupPanel8.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel8.Style.BorderTopWidth = 1
        Me.GroupPanel8.Style.CornerDiameter = 4
        Me.GroupPanel8.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.GroupPanel8.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.GroupPanel8.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.GroupPanel8.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.GroupPanel8.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.GroupPanel8.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.GroupPanel8.TabIndex = 36
        Me.GroupPanel8.Text = "Generate Selected Period Statement"
        '
        'LabelX2
        '
        Me.LabelX2.AutoSize = True
        '
        '
        '
        Me.LabelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX2.Location = New System.Drawing.Point(9, 40)
        Me.LabelX2.Name = "LabelX2"
        Me.LabelX2.Size = New System.Drawing.Size(17, 18)
        Me.LabelX2.TabIndex = 12
        Me.LabelX2.Text = "To"
        '
        'LabelX1
        '
        Me.LabelX1.AutoSize = True
        '
        '
        '
        Me.LabelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX1.Location = New System.Drawing.Point(9, 3)
        Me.LabelX1.Name = "LabelX1"
        Me.LabelX1.Size = New System.Drawing.Size(32, 18)
        Me.LabelX1.TabIndex = 11
        Me.LabelX1.Text = "From"
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
        Me.dtFrom.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtFrom.Format = DevComponents.Editors.eDateTimePickerFormat.Custom
        Me.dtFrom.IsPopupCalendarOpen = False
        Me.dtFrom.Location = New System.Drawing.Point(54, 3)
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
        Me.dtFrom.Size = New System.Drawing.Size(115, 27)
        Me.dtFrom.TabIndex = 7
        Me.dtFrom.Value = New Date(1900, 1, 1, 0, 0, 0, 0)
        Me.dtFrom.WatermarkText = "From"
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
        Me.dtTo.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtTo.Format = DevComponents.Editors.eDateTimePickerFormat.Custom
        Me.dtTo.IsPopupCalendarOpen = False
        Me.dtTo.Location = New System.Drawing.Point(54, 36)
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
        Me.dtTo.Size = New System.Drawing.Size(115, 27)
        Me.dtTo.TabIndex = 8
        Me.dtTo.Value = New Date(1900, 1, 1, 0, 0, 0, 0)
        Me.dtTo.WatermarkText = "To"
        '
        'btnGenerateSelectedPeriodValues
        '
        Me.btnGenerateSelectedPeriodValues.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnGenerateSelectedPeriodValues.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnGenerateSelectedPeriodValues.Location = New System.Drawing.Point(249, 6)
        Me.btnGenerateSelectedPeriodValues.Name = "btnGenerateSelectedPeriodValues"
        Me.btnGenerateSelectedPeriodValues.Size = New System.Drawing.Size(99, 58)
        Me.btnGenerateSelectedPeriodValues.TabIndex = 9
        Me.btnGenerateSelectedPeriodValues.Text = "GENERATE"
        '
        'GroupPanel4
        '
        Me.GroupPanel4.BackColor = System.Drawing.Color.Transparent
        Me.GroupPanel4.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanel4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanel4.Controls.Add(Me.CheckBoxX2)
        Me.GroupPanel4.Controls.Add(Me.chkBlankValue)
        Me.GroupPanel4.Controls.Add(Me.txtBlankCellValue)
        Me.GroupPanel4.Controls.Add(Me.btnInsertBlankValues)
        Me.GroupPanel4.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel4.Location = New System.Drawing.Point(7, 164)
        Me.GroupPanel4.Name = "GroupPanel4"
        Me.GroupPanel4.Size = New System.Drawing.Size(360, 91)
        '
        '
        '
        Me.GroupPanel4.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.GroupPanel4.Style.BackColorGradientAngle = 90
        Me.GroupPanel4.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.GroupPanel4.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel4.Style.BorderBottomWidth = 1
        Me.GroupPanel4.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.GroupPanel4.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel4.Style.BorderLeftWidth = 1
        Me.GroupPanel4.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel4.Style.BorderRightWidth = 1
        Me.GroupPanel4.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel4.Style.BorderTopWidth = 1
        Me.GroupPanel4.Style.CornerDiameter = 4
        Me.GroupPanel4.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.GroupPanel4.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.GroupPanel4.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.GroupPanel4.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.GroupPanel4.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.GroupPanel4.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.GroupPanel4.TabIndex = 33
        Me.GroupPanel4.Text = "Blank Cell Value"
        '
        'CheckBoxX2
        '
        Me.CheckBoxX2.AutoSize = True
        '
        '
        '
        Me.CheckBoxX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.CheckBoxX2.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton
        Me.CheckBoxX2.Location = New System.Drawing.Point(3, 39)
        Me.CheckBoxX2.Name = "CheckBoxX2"
        Me.CheckBoxX2.Size = New System.Drawing.Size(101, 18)
        Me.CheckBoxX2.TabIndex = 10
        Me.CheckBoxX2.TabStop = False
        Me.CheckBoxX2.Text = "Leave it blank"
        '
        'chkBlankValue
        '
        Me.chkBlankValue.AutoSize = True
        '
        '
        '
        Me.chkBlankValue.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.chkBlankValue.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton
        Me.chkBlankValue.Checked = True
        Me.chkBlankValue.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkBlankValue.CheckValue = "Y"
        Me.chkBlankValue.Location = New System.Drawing.Point(3, 12)
        Me.chkBlankValue.Name = "chkBlankValue"
        Me.chkBlankValue.Size = New System.Drawing.Size(122, 18)
        Me.chkBlankValue.TabIndex = 9
        Me.chkBlankValue.TabStop = False
        Me.chkBlankValue.Text = "Value specified in"
        '
        'GroupPanel3
        '
        Me.GroupPanel3.BackColor = System.Drawing.Color.Transparent
        Me.GroupPanel3.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanel3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanel3.Controls.Add(Me.btnGeneratePreviousMonthValues)
        Me.GroupPanel3.Controls.Add(Me.chkGeneratePreviousMonthValuesFromFile)
        Me.GroupPanel3.Controls.Add(Me.chkGeneratePreviousMonthValuesFromDB)
        Me.GroupPanel3.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel3.Location = New System.Drawing.Point(7, 432)
        Me.GroupPanel3.Name = "GroupPanel3"
        Me.GroupPanel3.Size = New System.Drawing.Size(360, 91)
        '
        '
        '
        Me.GroupPanel3.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.GroupPanel3.Style.BackColorGradientAngle = 90
        Me.GroupPanel3.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.GroupPanel3.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel3.Style.BorderBottomWidth = 1
        Me.GroupPanel3.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.GroupPanel3.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel3.Style.BorderLeftWidth = 1
        Me.GroupPanel3.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel3.Style.BorderRightWidth = 1
        Me.GroupPanel3.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel3.Style.BorderTopWidth = 1
        Me.GroupPanel3.Style.CornerDiameter = 4
        Me.GroupPanel3.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.GroupPanel3.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.GroupPanel3.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.GroupPanel3.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.GroupPanel3.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.GroupPanel3.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.GroupPanel3.TabIndex = 35
        Me.GroupPanel3.Text = "Generate Previous Month Values from"
        '
        'chkGeneratePreviousMonthValuesFromFile
        '
        Me.chkGeneratePreviousMonthValuesFromFile.AutoSize = True
        '
        '
        '
        Me.chkGeneratePreviousMonthValuesFromFile.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.chkGeneratePreviousMonthValuesFromFile.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton
        Me.chkGeneratePreviousMonthValuesFromFile.Location = New System.Drawing.Point(3, 40)
        Me.chkGeneratePreviousMonthValuesFromFile.Name = "chkGeneratePreviousMonthValuesFromFile"
        Me.chkGeneratePreviousMonthValuesFromFile.Size = New System.Drawing.Size(225, 18)
        Me.chkGeneratePreviousMonthValuesFromFile.TabIndex = 3
        Me.chkGeneratePreviousMonthValuesFromFile.TabStop = False
        Me.chkGeneratePreviousMonthValuesFromFile.Text = "Saved statement of Previous Month"
        '
        'chkGeneratePreviousMonthValuesFromDB
        '
        Me.chkGeneratePreviousMonthValuesFromDB.AutoSize = True
        '
        '
        '
        Me.chkGeneratePreviousMonthValuesFromDB.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.chkGeneratePreviousMonthValuesFromDB.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton
        Me.chkGeneratePreviousMonthValuesFromDB.Checked = True
        Me.chkGeneratePreviousMonthValuesFromDB.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkGeneratePreviousMonthValuesFromDB.CheckValue = "Y"
        Me.chkGeneratePreviousMonthValuesFromDB.Location = New System.Drawing.Point(3, 13)
        Me.chkGeneratePreviousMonthValuesFromDB.Name = "chkGeneratePreviousMonthValuesFromDB"
        Me.chkGeneratePreviousMonthValuesFromDB.Size = New System.Drawing.Size(75, 18)
        Me.chkGeneratePreviousMonthValuesFromDB.TabIndex = 2
        Me.chkGeneratePreviousMonthValuesFromDB.TabStop = False
        Me.chkGeneratePreviousMonthValuesFromDB.Text = "Database"
        '
        'GroupPanel2
        '
        Me.GroupPanel2.BackColor = System.Drawing.Color.Transparent
        Me.GroupPanel2.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanel2.Controls.Add(Me.btnGenerateMonth1Values)
        Me.GroupPanel2.Controls.Add(Me.chkGenerateMonth1ValuesFromFile)
        Me.GroupPanel2.Controls.Add(Me.chkGenerateMonth1ValuesFromDB)
        Me.GroupPanel2.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel2.Location = New System.Drawing.Point(7, 298)
        Me.GroupPanel2.Name = "GroupPanel2"
        Me.GroupPanel2.Size = New System.Drawing.Size(360, 91)
        '
        '
        '
        Me.GroupPanel2.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.GroupPanel2.Style.BackColorGradientAngle = 90
        Me.GroupPanel2.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.GroupPanel2.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel2.Style.BorderBottomWidth = 1
        Me.GroupPanel2.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.GroupPanel2.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel2.Style.BorderLeftWidth = 1
        Me.GroupPanel2.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel2.Style.BorderRightWidth = 1
        Me.GroupPanel2.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel2.Style.BorderTopWidth = 1
        Me.GroupPanel2.Style.CornerDiameter = 4
        Me.GroupPanel2.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.GroupPanel2.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.GroupPanel2.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.GroupPanel2.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.GroupPanel2.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.GroupPanel2.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.GroupPanel2.TabIndex = 34
        Me.GroupPanel2.Text = "Generate Month 1 Values from"
        '
        'chkGenerateMonth1ValuesFromFile
        '
        Me.chkGenerateMonth1ValuesFromFile.AutoSize = True
        '
        '
        '
        Me.chkGenerateMonth1ValuesFromFile.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.chkGenerateMonth1ValuesFromFile.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton
        Me.chkGenerateMonth1ValuesFromFile.Location = New System.Drawing.Point(3, 39)
        Me.chkGenerateMonth1ValuesFromFile.Name = "chkGenerateMonth1ValuesFromFile"
        Me.chkGenerateMonth1ValuesFromFile.Size = New System.Drawing.Size(183, 18)
        Me.chkGenerateMonth1ValuesFromFile.TabIndex = 1
        Me.chkGenerateMonth1ValuesFromFile.TabStop = False
        Me.chkGenerateMonth1ValuesFromFile.Text = "Saved statement of Month 1"
        '
        'chkGenerateMonth1ValuesFromDB
        '
        Me.chkGenerateMonth1ValuesFromDB.AutoSize = True
        '
        '
        '
        Me.chkGenerateMonth1ValuesFromDB.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.chkGenerateMonth1ValuesFromDB.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton
        Me.chkGenerateMonth1ValuesFromDB.Checked = True
        Me.chkGenerateMonth1ValuesFromDB.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkGenerateMonth1ValuesFromDB.CheckValue = "Y"
        Me.chkGenerateMonth1ValuesFromDB.Location = New System.Drawing.Point(3, 12)
        Me.chkGenerateMonth1ValuesFromDB.Name = "chkGenerateMonth1ValuesFromDB"
        Me.chkGenerateMonth1ValuesFromDB.Size = New System.Drawing.Size(75, 18)
        Me.chkGenerateMonth1ValuesFromDB.TabIndex = 0
        Me.chkGenerateMonth1ValuesFromDB.TabStop = False
        Me.chkGenerateMonth1ValuesFromDB.Text = "Database"
        '
        'GroupPanel1
        '
        Me.GroupPanel1.BackColor = System.Drawing.Color.Transparent
        Me.GroupPanel1.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanel1.Controls.Add(Me.btnGeneratePerformanceStatement)
        Me.GroupPanel1.Controls.Add(Me.LabelX3)
        Me.GroupPanel1.Controls.Add(Me.LabelX4)
        Me.GroupPanel1.Controls.Add(Me.txtYear)
        Me.GroupPanel1.Controls.Add(Me.cmbMonth)
        Me.GroupPanel1.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel1.Location = New System.Drawing.Point(7, 12)
        Me.GroupPanel1.Name = "GroupPanel1"
        Me.GroupPanel1.Size = New System.Drawing.Size(360, 109)
        '
        '
        '
        Me.GroupPanel1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.GroupPanel1.Style.BackColorGradientAngle = 90
        Me.GroupPanel1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.GroupPanel1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel1.Style.BorderBottomWidth = 1
        Me.GroupPanel1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.GroupPanel1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel1.Style.BorderLeftWidth = 1
        Me.GroupPanel1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel1.Style.BorderRightWidth = 1
        Me.GroupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel1.Style.BorderTopWidth = 1
        Me.GroupPanel1.Style.CornerDiameter = 4
        Me.GroupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.GroupPanel1.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.GroupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.GroupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.GroupPanel1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.GroupPanel1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.GroupPanel1.TabIndex = 32
        Me.GroupPanel1.Text = "Generate Statement for Selected Month & Year"
        '
        'btnGeneratePerformanceStatement
        '
        Me.btnGeneratePerformanceStatement.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnGeneratePerformanceStatement.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnGeneratePerformanceStatement.Location = New System.Drawing.Point(249, 18)
        Me.btnGeneratePerformanceStatement.Name = "btnGeneratePerformanceStatement"
        Me.btnGeneratePerformanceStatement.Size = New System.Drawing.Size(99, 58)
        Me.btnGeneratePerformanceStatement.TabIndex = 2
        Me.btnGeneratePerformanceStatement.Text = "GENERATE"
        '
        'PerformanceBindingSource
        '
        Me.PerformanceBindingSource.DataMember = "Performance"
        Me.PerformanceBindingSource.DataSource = Me.FingerPrintDataSet
        '
        'FingerPrintDataSet
        '
        Me.FingerPrintDataSet.DataSetName = "FingerPrintDataSet"
        Me.FingerPrintDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'FPARegisterTableAdapter
        '
        Me.FPARegisterTableAdapter.ClearBeforeFill = True
        '
        'SOCRegisterTableAdapter
        '
        Me.SOCRegisterTableAdapter.ClearBeforeFill = True
        '
        'DaRegisterTableAdapter
        '
        Me.DaRegisterTableAdapter.ClearBeforeFill = True
        '
        'PerformanceTableAdapter
        '
        Me.PerformanceTableAdapter.ClearBeforeFill = True
        '
        'CdRegisterTableAdapter
        '
        Me.CdRegisterTableAdapter.ClearBeforeFill = True
        '
        'PanelEx3
        '
        Me.PanelEx3.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.PanelEx3.Controls.Add(Me.CircularProgress1)
        Me.PanelEx3.Controls.Add(Me.btnOpenFolder)
        Me.PanelEx3.Controls.Add(Me.lblPreviousMonth)
        Me.PanelEx3.Controls.Add(Me.btnOpenInWord)
        Me.PanelEx3.Controls.Add(Me.lblMonth1)
        Me.PanelEx3.Controls.Add(Me.btnClearAllFields)
        Me.PanelEx3.DisabledBackColor = System.Drawing.Color.Empty
        Me.PanelEx3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelEx3.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PanelEx3.Location = New System.Drawing.Point(377, 632)
        Me.PanelEx3.Name = "PanelEx3"
        Me.PanelEx3.Size = New System.Drawing.Size(977, 101)
        Me.PanelEx3.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx3.Style.GradientAngle = 90
        Me.PanelEx3.TabIndex = 13
        '
        'CircularProgress1
        '
        '
        '
        '
        Me.CircularProgress1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.CircularProgress1.FocusCuesEnabled = False
        Me.CircularProgress1.Location = New System.Drawing.Point(295, 7)
        Me.CircularProgress1.Name = "CircularProgress1"
        Me.CircularProgress1.ProgressColor = System.Drawing.Color.Red
        Me.CircularProgress1.ProgressTextVisible = True
        Me.CircularProgress1.Size = New System.Drawing.Size(136, 69)
        Me.CircularProgress1.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP
        Me.CircularProgress1.TabIndex = 55
        Me.CircularProgress1.TabStop = False
        '
        'btnOpenFolder
        '
        Me.btnOpenFolder.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnOpenFolder.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnOpenFolder.Image = CType(resources.GetObject("btnOpenFolder.Image"), System.Drawing.Image)
        Me.btnOpenFolder.Location = New System.Drawing.Point(299, 15)
        Me.btnOpenFolder.Name = "btnOpenFolder"
        Me.btnOpenFolder.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlW)
        Me.btnOpenFolder.Size = New System.Drawing.Size(126, 56)
        Me.btnOpenFolder.TabIndex = 13
        Me.btnOpenFolder.Text = "Open Folder"
        '
        'lblPreviousMonth
        '
        Me.lblPreviousMonth.AutoSize = True
        '
        '
        '
        Me.lblPreviousMonth.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblPreviousMonth.Location = New System.Drawing.Point(437, 15)
        Me.lblPreviousMonth.Name = "lblPreviousMonth"
        Me.lblPreviousMonth.Size = New System.Drawing.Size(100, 20)
        Me.lblPreviousMonth.TabIndex = 31
        Me.lblPreviousMonth.Text = "Previous Month"
        '
        'btnOpenInWord
        '
        Me.btnOpenInWord.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnOpenInWord.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnOpenInWord.Image = CType(resources.GetObject("btnOpenInWord.Image"), System.Drawing.Image)
        Me.btnOpenInWord.Location = New System.Drawing.Point(156, 15)
        Me.btnOpenInWord.Name = "btnOpenInWord"
        Me.btnOpenInWord.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlW)
        Me.btnOpenInWord.Size = New System.Drawing.Size(126, 56)
        Me.btnOpenInWord.TabIndex = 12
        Me.btnOpenInWord.Text = "PRINT"
        '
        'lblMonth1
        '
        Me.lblMonth1.AutoSize = True
        '
        '
        '
        Me.lblMonth1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblMonth1.Location = New System.Drawing.Point(437, 51)
        Me.lblMonth1.Name = "lblMonth1"
        Me.lblMonth1.Size = New System.Drawing.Size(55, 20)
        Me.lblMonth1.TabIndex = 30
        Me.lblMonth1.Text = "Month 1"
        '
        'DataGridViewX1
        '
        Me.DataGridViewX1.AllowUserToAddRows = False
        Me.DataGridViewX1.AllowUserToDeleteRows = False
        Me.DataGridViewX1.AutoGenerateColumns = False
        Me.DataGridViewX1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        DataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle19.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle19.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle19.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle19.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle19.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewX1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle19
        Me.DataGridViewX1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewX1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.SlNoDataGridViewTextBoxColumn, Me.DetailsOfWorkDataGridViewTextBoxColumn, Me.PreviousDataGridViewTextBoxColumn, Me.Month1DataGridViewTextBoxColumn, Me.Month2DataGridViewTextBoxColumn, Me.Month3DataGridViewTextBoxColumn, Me.PresentDataGridViewTextBoxColumn, Me.RemarksDataGridViewTextBoxColumn})
        Me.DataGridViewX1.DataSource = Me.PerformanceBindingSource
        DataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle26.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle26.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle26.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle26.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle26.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle26.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridViewX1.DefaultCellStyle = DataGridViewCellStyle26
        Me.DataGridViewX1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridViewX1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke
        Me.DataGridViewX1.GridColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.DataGridViewX1.Location = New System.Drawing.Point(377, 45)
        Me.DataGridViewX1.MultiSelect = False
        Me.DataGridViewX1.Name = "DataGridViewX1"
        Me.DataGridViewX1.RowHeadersVisible = False
        DataGridViewCellStyle27.Font = New System.Drawing.Font("Rupee Foradian", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DataGridViewX1.RowsDefaultCellStyle = DataGridViewCellStyle27
        Me.DataGridViewX1.RowTemplate.Height = 25
        Me.DataGridViewX1.Size = New System.Drawing.Size(977, 587)
        Me.DataGridViewX1.TabIndex = 15
        '
        'SlNoDataGridViewTextBoxColumn
        '
        Me.SlNoDataGridViewTextBoxColumn.DataPropertyName = "SlNo"
        DataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter
        Me.SlNoDataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle20
        Me.SlNoDataGridViewTextBoxColumn.HeaderText = "Sl.No."
        Me.SlNoDataGridViewTextBoxColumn.Name = "SlNoDataGridViewTextBoxColumn"
        Me.SlNoDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.SlNoDataGridViewTextBoxColumn.Width = 45
        '
        'DetailsOfWorkDataGridViewTextBoxColumn
        '
        Me.DetailsOfWorkDataGridViewTextBoxColumn.DataPropertyName = "DetailsOfWork"
        Me.DetailsOfWorkDataGridViewTextBoxColumn.HeaderText = "Details Of Work"
        Me.DetailsOfWorkDataGridViewTextBoxColumn.Name = "DetailsOfWorkDataGridViewTextBoxColumn"
        Me.DetailsOfWorkDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DetailsOfWorkDataGridViewTextBoxColumn.Width = 350
        '
        'PreviousDataGridViewTextBoxColumn
        '
        Me.PreviousDataGridViewTextBoxColumn.DataPropertyName = "Previous"
        DataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter
        Me.PreviousDataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle21
        Me.PreviousDataGridViewTextBoxColumn.HeaderText = "Previous Quarter/Month"
        Me.PreviousDataGridViewTextBoxColumn.Name = "PreviousDataGridViewTextBoxColumn"
        Me.PreviousDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.PreviousDataGridViewTextBoxColumn.Width = 80
        '
        'Month1DataGridViewTextBoxColumn
        '
        Me.Month1DataGridViewTextBoxColumn.DataPropertyName = "Month1"
        DataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter
        Me.Month1DataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle22
        Me.Month1DataGridViewTextBoxColumn.HeaderText = "Month1"
        Me.Month1DataGridViewTextBoxColumn.Name = "Month1DataGridViewTextBoxColumn"
        Me.Month1DataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Month1DataGridViewTextBoxColumn.Width = 80
        '
        'Month2DataGridViewTextBoxColumn
        '
        Me.Month2DataGridViewTextBoxColumn.DataPropertyName = "Month2"
        DataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter
        Me.Month2DataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle23
        Me.Month2DataGridViewTextBoxColumn.HeaderText = "Month2"
        Me.Month2DataGridViewTextBoxColumn.Name = "Month2DataGridViewTextBoxColumn"
        Me.Month2DataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Month2DataGridViewTextBoxColumn.Width = 80
        '
        'Month3DataGridViewTextBoxColumn
        '
        Me.Month3DataGridViewTextBoxColumn.DataPropertyName = "Month3"
        DataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter
        Me.Month3DataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle24
        Me.Month3DataGridViewTextBoxColumn.HeaderText = "Month3"
        Me.Month3DataGridViewTextBoxColumn.Name = "Month3DataGridViewTextBoxColumn"
        Me.Month3DataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Month3DataGridViewTextBoxColumn.Width = 80
        '
        'PresentDataGridViewTextBoxColumn
        '
        Me.PresentDataGridViewTextBoxColumn.DataPropertyName = "Present"
        DataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter
        Me.PresentDataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle25
        Me.PresentDataGridViewTextBoxColumn.HeaderText = "Present Quarter"
        Me.PresentDataGridViewTextBoxColumn.Name = "PresentDataGridViewTextBoxColumn"
        Me.PresentDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.PresentDataGridViewTextBoxColumn.Width = 80
        '
        'RemarksDataGridViewTextBoxColumn
        '
        Me.RemarksDataGridViewTextBoxColumn.DataPropertyName = "Remarks"
        Me.RemarksDataGridViewTextBoxColumn.HeaderText = "Remarks"
        Me.RemarksDataGridViewTextBoxColumn.Name = "RemarksDataGridViewTextBoxColumn"
        Me.RemarksDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.RemarksDataGridViewTextBoxColumn.Width = 190
        '
        'bgwStatement
        '
        Me.bgwStatement.WorkerReportsProgress = True
        Me.bgwStatement.WorkerSupportsCancellation = True
        '
        'frmMonthlyPerformance
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1354, 733)
        Me.Controls.Add(Me.DataGridViewX1)
        Me.Controls.Add(Me.PanelEx3)
        Me.Controls.Add(Me.PanelEx1)
        Me.Controls.Add(Me.PanelEx2)
        Me.DoubleBuffered = True
        Me.EnableGlass = False
        Me.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.Name = "frmMonthlyPerformance"
        Me.Text = "Monthly Performance Statement"
        Me.TitleText = "<b>Monthly Performance Statement</b>"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.PanelEx1.ResumeLayout(False)
        CType(Me.txtYear, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelEx2.ResumeLayout(False)
        Me.GroupPanel8.ResumeLayout(False)
        Me.GroupPanel8.PerformLayout()
        CType(Me.dtFrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtTo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupPanel4.ResumeLayout(False)
        Me.GroupPanel4.PerformLayout()
        Me.GroupPanel3.ResumeLayout(False)
        Me.GroupPanel3.PerformLayout()
        Me.GroupPanel2.ResumeLayout(False)
        Me.GroupPanel2.PerformLayout()
        Me.GroupPanel1.ResumeLayout(False)
        Me.GroupPanel1.PerformLayout()
        CType(Me.PerformanceBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FingerPrintDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelEx3.ResumeLayout(False)
        Me.PanelEx3.PerformLayout()
        CType(Me.DataGridViewX1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents FingerPrintDataSet As FingerprintInformationSystem.FingerPrintDataSet
    Friend WithEvents FPARegisterTableAdapter As FingerprintInformationSystem.FingerPrintDataSetTableAdapters.FPAttestationRegisterTableAdapter
    Friend WithEvents SOCRegisterTableAdapter As FingerprintInformationSystem.FingerPrintDataSetTableAdapters.SOCRegisterTableAdapter
    Friend WithEvents DaRegisterTableAdapter As FingerprintInformationSystem.FingerPrintDataSetTableAdapters.DARegisterTableAdapter
    Friend WithEvents PanelEx1 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents LabelX4 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX3 As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtYear As DevComponents.Editors.IntegerInput
    Friend WithEvents cmbMonth As DevComponents.DotNetBar.Controls.ComboBoxEx
    Friend WithEvents btnClearAllFields As DevComponents.DotNetBar.ButtonX
    Friend WithEvents PerformanceBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents PerformanceTableAdapter As FingerprintInformationSystem.FingerPrintDataSetTableAdapters.PerformanceTableAdapter
    Friend WithEvents lblHeader As DevComponents.DotNetBar.LabelX
    Friend WithEvents btnInsertBlankValues As DevComponents.DotNetBar.ButtonX
    Friend WithEvents CdRegisterTableAdapter As FingerprintInformationSystem.FingerPrintDataSetTableAdapters.CDRegisterTableAdapter
    Friend WithEvents txtBlankCellValue As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents PanelEx2 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents GroupPanel1 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents GroupPanel4 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents GroupPanel3 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents GroupPanel2 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents chkGenerateMonth1ValuesFromFile As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents chkGenerateMonth1ValuesFromDB As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents chkGeneratePreviousMonthValuesFromFile As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents chkGeneratePreviousMonthValuesFromDB As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents CheckBoxX2 As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents chkBlankValue As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents btnGeneratePreviousMonthValues As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnGenerateMonth1Values As DevComponents.DotNetBar.ButtonX
    Friend WithEvents PanelEx3 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents lblPreviousMonth As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblMonth1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents GroupPanel8 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents btnGenerateSelectedPeriodValues As DevComponents.DotNetBar.ButtonX
    Friend WithEvents LabelX2 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents dtFrom As DevComponents.Editors.DateTimeAdv.DateTimeInput
    Friend WithEvents dtTo As DevComponents.Editors.DateTimeAdv.DateTimeInput
    Friend WithEvents btnOpenInWord As DevComponents.DotNetBar.ButtonX
    Friend WithEvents DataGridViewX1 As DevComponents.DotNetBar.Controls.DataGridViewX
    Friend WithEvents CircularProgress1 As DevComponents.DotNetBar.Controls.CircularProgress
    Friend WithEvents bgwStatement As System.ComponentModel.BackgroundWorker
    Friend WithEvents btnOpenFolder As DevComponents.DotNetBar.ButtonX
    Friend WithEvents SlNoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DetailsOfWorkDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PreviousDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Month1DataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Month2DataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Month3DataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PresentDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RemarksDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnGeneratePerformanceStatement As DevComponents.DotNetBar.ButtonX
End Class
