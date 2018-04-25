<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSOCStatistics
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSOCStatistics))
        Dim ReportDataSource1 As Microsoft.Reporting.WinForms.ReportDataSource = New Microsoft.Reporting.WinForms.ReportDataSource()
        Dim ReportDataSource2 As Microsoft.Reporting.WinForms.ReportDataSource = New Microsoft.Reporting.WinForms.ReportDataSource()
        Dim ReportDataSource3 As Microsoft.Reporting.WinForms.ReportDataSource = New Microsoft.Reporting.WinForms.ReportDataSource()
        Dim ReportDataSource4 As Microsoft.Reporting.WinForms.ReportDataSource = New Microsoft.Reporting.WinForms.ReportDataSource()
        Me.PSWiseSOCCountBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.FingerPrintDataSet = New FingerprintInformationSystem.FingerPrintDataSet()
        Me.OfficerWiseSOCCountBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.MOWiseSOCCountBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.CrNoWiseSOCCountBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.PanelEx2 = New DevComponents.DotNetBar.PanelEx()
        Me.PanelEx1 = New DevComponents.DotNetBar.PanelEx()
        Me.btnPrint = New DevComponents.DotNetBar.ButtonX()
        Me.LabelX2 = New DevComponents.DotNetBar.LabelX()
        Me.btnGenerateByMonth = New DevComponents.DotNetBar.ButtonX()
        Me.LabelX1 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX4 = New DevComponents.DotNetBar.LabelX()
        Me.btnGenerateByDate = New DevComponents.DotNetBar.ButtonX()
        Me.LabelX3 = New DevComponents.DotNetBar.LabelX()
        Me.txtYear = New DevComponents.Editors.IntegerInput()
        Me.dtFrom = New DevComponents.Editors.DateTimeAdv.DateTimeInput()
        Me.cmbMonth = New DevComponents.DotNetBar.Controls.ComboBoxEx()
        Me.dtTo = New DevComponents.Editors.DateTimeAdv.DateTimeInput()
        Me.ReportViewer1 = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.SOCRegisterTableAdapter = New FingerprintInformationSystem.FingerPrintDataSetTableAdapters.SOCRegisterTableAdapter()
        Me.PSWiseSOCCountTableAdapter = New FingerprintInformationSystem.FingerPrintDataSetTableAdapters.PSWiseSOCCountTableAdapter()
        Me.OfficerWiseSOCCountTableAdapter = New FingerprintInformationSystem.FingerPrintDataSetTableAdapters.OfficerWiseSOCCountTableAdapter()
        Me.MOWiseSOCCountTableAdapter = New FingerprintInformationSystem.FingerPrintDataSetTableAdapters.MOWiseSOCCountTableAdapter()
        Me.CrNoWiseSOCCountTableAdapter = New FingerprintInformationSystem.FingerPrintDataSetTableAdapters.CrNoWiseSOCCountTableAdapter()
        Me.SOCRegisterBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        CType(Me.PSWiseSOCCountBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FingerPrintDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OfficerWiseSOCCountBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MOWiseSOCCountBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CrNoWiseSOCCountBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.PanelEx2.SuspendLayout()
        Me.PanelEx1.SuspendLayout()
        CType(Me.txtYear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SOCRegisterBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PSWiseSOCCountBindingSource
        '
        Me.PSWiseSOCCountBindingSource.DataMember = "PSWiseSOCCount"
        Me.PSWiseSOCCountBindingSource.DataSource = Me.FingerPrintDataSet
        '
        'FingerPrintDataSet
        '
        Me.FingerPrintDataSet.DataSetName = "FingerPrintDataSet"
        Me.FingerPrintDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'OfficerWiseSOCCountBindingSource
        '
        Me.OfficerWiseSOCCountBindingSource.DataMember = "OfficerWiseSOCCount"
        Me.OfficerWiseSOCCountBindingSource.DataSource = Me.FingerPrintDataSet
        '
        'MOWiseSOCCountBindingSource
        '
        Me.MOWiseSOCCountBindingSource.DataMember = "MOWiseSOCCount"
        Me.MOWiseSOCCountBindingSource.DataSource = Me.FingerPrintDataSet
        '
        'CrNoWiseSOCCountBindingSource
        '
        Me.CrNoWiseSOCCountBindingSource.DataMember = "CrNoWiseSOCCount"
        Me.CrNoWiseSOCCountBindingSource.DataSource = Me.FingerPrintDataSet
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.PanelEx2, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.ReportViewer1, 0, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 97.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1055, 348)
        Me.TableLayoutPanel1.TabIndex = 2
        '
        'PanelEx2
        '
        Me.PanelEx2.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.PanelEx2.Controls.Add(Me.PanelEx1)
        Me.PanelEx2.DisabledBackColor = System.Drawing.Color.Empty
        Me.PanelEx2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelEx2.Location = New System.Drawing.Point(3, 3)
        Me.PanelEx2.Name = "PanelEx2"
        Me.PanelEx2.Size = New System.Drawing.Size(1049, 91)
        Me.PanelEx2.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx2.Style.GradientAngle = 90
        Me.PanelEx2.TabIndex = 9
        '
        'PanelEx1
        '
        Me.PanelEx1.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.PanelEx1.Controls.Add(Me.btnPrint)
        Me.PanelEx1.Controls.Add(Me.LabelX2)
        Me.PanelEx1.Controls.Add(Me.btnGenerateByMonth)
        Me.PanelEx1.Controls.Add(Me.LabelX1)
        Me.PanelEx1.Controls.Add(Me.LabelX4)
        Me.PanelEx1.Controls.Add(Me.btnGenerateByDate)
        Me.PanelEx1.Controls.Add(Me.LabelX3)
        Me.PanelEx1.Controls.Add(Me.txtYear)
        Me.PanelEx1.Controls.Add(Me.dtFrom)
        Me.PanelEx1.Controls.Add(Me.cmbMonth)
        Me.PanelEx1.Controls.Add(Me.dtTo)
        Me.PanelEx1.DisabledBackColor = System.Drawing.Color.Empty
        Me.PanelEx1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelEx1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PanelEx1.Location = New System.Drawing.Point(0, 0)
        Me.PanelEx1.Name = "PanelEx1"
        Me.PanelEx1.Size = New System.Drawing.Size(1049, 91)
        Me.PanelEx1.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx1.Style.GradientAngle = 90
        Me.PanelEx1.TabIndex = 0
        '
        'btnPrint
        '
        Me.btnPrint.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnPrint.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnPrint.Image = CType(resources.GetObject("btnPrint.Image"), System.Drawing.Image)
        Me.btnPrint.Location = New System.Drawing.Point(532, 19)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlP)
        Me.btnPrint.Size = New System.Drawing.Size(98, 56)
        Me.btnPrint.TabIndex = 7
        Me.btnPrint.Text = "Print"
        '
        'LabelX2
        '
        Me.LabelX2.AutoSize = True
        '
        '
        '
        Me.LabelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX2.Location = New System.Drawing.Point(246, 16)
        Me.LabelX2.Name = "LabelX2"
        Me.LabelX2.Size = New System.Drawing.Size(17, 18)
        Me.LabelX2.TabIndex = 8
        Me.LabelX2.Text = "To"
        '
        'btnGenerateByMonth
        '
        Me.btnGenerateByMonth.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnGenerateByMonth.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnGenerateByMonth.Location = New System.Drawing.Point(412, 49)
        Me.btnGenerateByMonth.Name = "btnGenerateByMonth"
        Me.btnGenerateByMonth.Size = New System.Drawing.Size(98, 29)
        Me.btnGenerateByMonth.TabIndex = 6
        Me.btnGenerateByMonth.Text = "Generate"
        '
        'LabelX1
        '
        Me.LabelX1.AutoSize = True
        '
        '
        '
        Me.LabelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX1.Location = New System.Drawing.Point(9, 16)
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
        Me.LabelX4.Location = New System.Drawing.Point(246, 60)
        Me.LabelX4.Name = "LabelX4"
        Me.LabelX4.Size = New System.Drawing.Size(28, 18)
        Me.LabelX4.TabIndex = 25
        Me.LabelX4.Text = "Year"
        '
        'btnGenerateByDate
        '
        Me.btnGenerateByDate.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnGenerateByDate.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnGenerateByDate.Location = New System.Drawing.Point(412, 11)
        Me.btnGenerateByDate.Name = "btnGenerateByDate"
        Me.btnGenerateByDate.Size = New System.Drawing.Size(98, 29)
        Me.btnGenerateByDate.TabIndex = 3
        Me.btnGenerateByDate.Text = "Generate"
        '
        'LabelX3
        '
        Me.LabelX3.AutoSize = True
        '
        '
        '
        Me.LabelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX3.Location = New System.Drawing.Point(9, 60)
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
        Me.txtYear.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtYear.InputHorizontalAlignment = DevComponents.Editors.eHorizontalAlignment.Left
        Me.txtYear.Location = New System.Drawing.Point(279, 49)
        Me.txtYear.MaxValue = 2099
        Me.txtYear.MinValue = 0
        Me.txtYear.Name = "txtYear"
        Me.txtYear.ShowUpDown = True
        Me.txtYear.Size = New System.Drawing.Size(111, 29)
        Me.txtYear.TabIndex = 5
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
        Me.dtFrom.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtFrom.Format = DevComponents.Editors.eDateTimePickerFormat.Custom
        Me.dtFrom.IsPopupCalendarOpen = False
        Me.dtFrom.Location = New System.Drawing.Point(87, 11)
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
        Me.dtFrom.Size = New System.Drawing.Size(134, 29)
        Me.dtFrom.TabIndex = 1
        Me.dtFrom.Value = New Date(1900, 1, 1, 0, 0, 0, 0)
        Me.dtFrom.WatermarkText = "From"
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
        Me.cmbMonth.Location = New System.Drawing.Point(87, 49)
        Me.cmbMonth.MaxDropDownItems = 15
        Me.cmbMonth.MaxLength = 255
        Me.cmbMonth.Name = "cmbMonth"
        Me.cmbMonth.Size = New System.Drawing.Size(134, 29)
        Me.cmbMonth.TabIndex = 4
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
        Me.dtTo.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtTo.Format = DevComponents.Editors.eDateTimePickerFormat.Custom
        Me.dtTo.IsPopupCalendarOpen = False
        Me.dtTo.Location = New System.Drawing.Point(279, 11)
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
        Me.dtTo.Size = New System.Drawing.Size(111, 29)
        Me.dtTo.TabIndex = 2
        Me.dtTo.Value = New Date(1900, 1, 1, 0, 0, 0, 0)
        Me.dtTo.WatermarkText = "To"
        '
        'ReportViewer1
        '
        Me.ReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill
        ReportDataSource1.Name = "FingerPrintDataSet_PSWiseSOCCount"
        ReportDataSource1.Value = Me.PSWiseSOCCountBindingSource
        ReportDataSource2.Name = "FingerPrintDataSet_OfficerWiseSOCCount"
        ReportDataSource2.Value = Me.OfficerWiseSOCCountBindingSource
        ReportDataSource3.Name = "FingerPrintDataSet_MOWiseSOCCount"
        ReportDataSource3.Value = Me.MOWiseSOCCountBindingSource
        ReportDataSource4.Name = "FingerPrintDataSet_CrNoWiseSOCCount"
        ReportDataSource4.Value = Me.CrNoWiseSOCCountBindingSource
        Me.ReportViewer1.LocalReport.DataSources.Add(ReportDataSource1)
        Me.ReportViewer1.LocalReport.DataSources.Add(ReportDataSource2)
        Me.ReportViewer1.LocalReport.DataSources.Add(ReportDataSource3)
        Me.ReportViewer1.LocalReport.DataSources.Add(ReportDataSource4)
        Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "FingerprintInformationSystem.SOC Statistics.rdlc"
        Me.ReportViewer1.Location = New System.Drawing.Point(3, 100)
        Me.ReportViewer1.Name = "ReportViewer1"
        Me.ReportViewer1.Size = New System.Drawing.Size(1049, 245)
        Me.ReportViewer1.TabIndex = 0
        '
        'SOCRegisterTableAdapter
        '
        Me.SOCRegisterTableAdapter.ClearBeforeFill = True
        '
        'PSWiseSOCCountTableAdapter
        '
        Me.PSWiseSOCCountTableAdapter.ClearBeforeFill = True
        '
        'OfficerWiseSOCCountTableAdapter
        '
        Me.OfficerWiseSOCCountTableAdapter.ClearBeforeFill = True
        '
        'MOWiseSOCCountTableAdapter
        '
        Me.MOWiseSOCCountTableAdapter.ClearBeforeFill = True
        '
        'CrNoWiseSOCCountTableAdapter
        '
        Me.CrNoWiseSOCCountTableAdapter.ClearBeforeFill = True
        '
        'SOCRegisterBindingSource
        '
        Me.SOCRegisterBindingSource.DataMember = "SOCRegister"
        '
        'Timer1
        '
        '
        'frmSOCStatistics
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1055, 348)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.DoubleBuffered = True
        Me.EnableGlass = False
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmSOCStatistics"
        Me.Text = "SOC Statistics"
        Me.TitleText = "<b>SOC Statistics</b>"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.PSWiseSOCCountBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FingerPrintDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OfficerWiseSOCCountBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MOWiseSOCCountBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CrNoWiseSOCCountBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.PanelEx2.ResumeLayout(False)
        Me.PanelEx1.ResumeLayout(False)
        Me.PanelEx1.PerformLayout()
        CType(Me.txtYear, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtFrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SOCRegisterBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents PanelEx1 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents LabelX2 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents btnGenerateByDate As DevComponents.DotNetBar.ButtonX
    Friend WithEvents dtFrom As DevComponents.Editors.DateTimeAdv.DateTimeInput
    Friend WithEvents dtTo As DevComponents.Editors.DateTimeAdv.DateTimeInput
    Friend WithEvents SOCRegisterTableAdapter As FingerprintInformationSystem.FingerPrintDataSetTableAdapters.SOCRegisterTableAdapter
    Friend WithEvents SOCRegisterBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents FingerPrintDataSet As FingerprintInformationSystem.FingerPrintDataSet
    Friend WithEvents PSWiseSOCCountBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents PSWiseSOCCountTableAdapter As FingerprintInformationSystem.FingerPrintDataSetTableAdapters.PSWiseSOCCountTableAdapter
    Friend WithEvents OfficerWiseSOCCountBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents OfficerWiseSOCCountTableAdapter As FingerprintInformationSystem.FingerPrintDataSetTableAdapters.OfficerWiseSOCCountTableAdapter
    Friend WithEvents MOWiseSOCCountBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents MOWiseSOCCountTableAdapter As FingerprintInformationSystem.FingerPrintDataSetTableAdapters.MOWiseSOCCountTableAdapter
    Friend WithEvents PanelEx2 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents btnGenerateByMonth As DevComponents.DotNetBar.ButtonX
    Friend WithEvents LabelX4 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX3 As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtYear As DevComponents.Editors.IntegerInput
    Friend WithEvents cmbMonth As DevComponents.DotNetBar.Controls.ComboBoxEx
    Friend WithEvents CrNoWiseSOCCountBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents CrNoWiseSOCCountTableAdapter As FingerprintInformationSystem.FingerPrintDataSetTableAdapters.CrNoWiseSOCCountTableAdapter
    Friend WithEvents btnPrint As DevComponents.DotNetBar.ButtonX
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Private WithEvents ReportViewer1 As Microsoft.Reporting.WinForms.ReportViewer
End Class
