<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSOCReportSentStatus
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmSOCReportSentStatus))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.btnSave = New DevComponents.DotNetBar.ButtonX()
        Me.btnDontSave = New DevComponents.DotNetBar.ButtonX()
        Me.LabelX1 = New DevComponents.DotNetBar.LabelX()
        Me.txtReportSentTo = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.dtReportSentOn = New DevComponents.Editors.DateTimeAdv.DateTimeInput()
        Me.LabelX3 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX2 = New DevComponents.DotNetBar.LabelX()
        Me.cmbNatureOfReport = New DevComponents.DotNetBar.Controls.ComboBoxEx()
        Me.LabelX4 = New DevComponents.DotNetBar.LabelX()
        Me.PanelEx1 = New DevComponents.DotNetBar.PanelEx()
        Me.LabelX5 = New DevComponents.DotNetBar.LabelX()
        Me.RSOCDatagrid = New DevComponents.DotNetBar.Controls.DataGridViewX()
        Me.SOCNumberDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ReportSentToDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DateOfReportSentDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NatureOfReportsDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SOCReportRegisterBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.FingerPrintDataSet1 = New FingerprintInformationSystem.FingerPrintDataSet()
        Me.btnCancel = New DevComponents.DotNetBar.ButtonX()
        Me.SocReportRegisterTableAdapter = New FingerprintInformationSystem.FingerPrintDataSetTableAdapters.SOCReportRegisterTableAdapter()
        Me.PoliceStationListTableAdapter1 = New FingerprintInformationSystem.FingerPrintDataSetTableAdapters.PoliceStationListTableAdapter()
        CType(Me.dtReportSentOn, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelEx1.SuspendLayout()
        CType(Me.RSOCDatagrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SOCReportRegisterBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FingerPrintDataSet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnSave
        '
        Me.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnSave.Location = New System.Drawing.Point(501, 45)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlS)
        Me.btnSave.Size = New System.Drawing.Size(96, 38)
        Me.btnSave.TabIndex = 3
        Me.btnSave.Text = "Save"
        '
        'btnDontSave
        '
        Me.btnDontSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnDontSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnDontSave.Location = New System.Drawing.Point(501, 94)
        Me.btnDontSave.Name = "btnDontSave"
        Me.btnDontSave.Size = New System.Drawing.Size(96, 37)
        Me.btnDontSave.TabIndex = 4
        Me.btnDontSave.Text = "Don't Save"
        '
        'LabelX1
        '
        Me.LabelX1.AutoSize = True
        '
        '
        '
        Me.LabelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX1.Location = New System.Drawing.Point(11, 45)
        Me.LabelX1.Name = "LabelX1"
        Me.LabelX1.Size = New System.Drawing.Size(65, 18)
        Me.LabelX1.TabIndex = 33
        Me.LabelX1.Text = "Address To"
        '
        'txtReportSentTo
        '
        Me.txtReportSentTo.AcceptsReturn = True
        Me.txtReportSentTo.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtReportSentTo.Border.Class = "TextBoxBorder"
        Me.txtReportSentTo.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtReportSentTo.ButtonCustom.Image = CType(resources.GetObject("txtReportSentTo.ButtonCustom.Image"), System.Drawing.Image)
        Me.txtReportSentTo.DisabledBackColor = System.Drawing.Color.White
        Me.txtReportSentTo.FocusHighlightEnabled = True
        Me.txtReportSentTo.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReportSentTo.ForeColor = System.Drawing.Color.Black
        Me.txtReportSentTo.Location = New System.Drawing.Point(116, 40)
        Me.txtReportSentTo.MaxLength = 255
        Me.txtReportSentTo.Multiline = True
        Me.txtReportSentTo.Name = "txtReportSentTo"
        Me.txtReportSentTo.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtReportSentTo.Size = New System.Drawing.Size(269, 71)
        Me.txtReportSentTo.TabIndex = 0
        Me.txtReportSentTo.WatermarkText = "Report Address"
        '
        'dtReportSentOn
        '
        Me.dtReportSentOn.AutoAdvance = True
        Me.dtReportSentOn.AutoSelectDate = True
        Me.dtReportSentOn.BackColor = System.Drawing.SystemColors.Window
        '
        '
        '
        Me.dtReportSentOn.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.dtReportSentOn.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.dtReportSentOn.ButtonClear.Image = CType(resources.GetObject("dtReportSentOn.ButtonClear.Image"), System.Drawing.Image)
        Me.dtReportSentOn.ButtonClear.Visible = True
        Me.dtReportSentOn.ButtonDropDown.Visible = True
        Me.dtReportSentOn.CustomFormat = "dd/MM/yyyy"
        Me.dtReportSentOn.FocusHighlightEnabled = True
        Me.dtReportSentOn.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtReportSentOn.Format = DevComponents.Editors.eDateTimePickerFormat.Custom
        Me.dtReportSentOn.IsPopupCalendarOpen = False
        Me.dtReportSentOn.Location = New System.Drawing.Point(116, 117)
        Me.dtReportSentOn.MaxDate = New Date(2100, 12, 31, 0, 0, 0, 0)
        Me.dtReportSentOn.MinDate = New Date(1900, 1, 1, 0, 0, 0, 0)
        '
        '
        '
        '
        '
        '
        Me.dtReportSentOn.MonthCalendar.BackgroundStyle.BackColor = System.Drawing.SystemColors.Window
        Me.dtReportSentOn.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.dtReportSentOn.MonthCalendar.ClearButtonVisible = True
        '
        '
        '
        Me.dtReportSentOn.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2
        Me.dtReportSentOn.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90
        Me.dtReportSentOn.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.dtReportSentOn.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.dtReportSentOn.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder
        Me.dtReportSentOn.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1
        Me.dtReportSentOn.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.dtReportSentOn.MonthCalendar.DaySize = New System.Drawing.Size(30, 15)
        Me.dtReportSentOn.MonthCalendar.DisplayMonth = New Date(2008, 7, 1, 0, 0, 0, 0)
        '
        '
        '
        Me.dtReportSentOn.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.dtReportSentOn.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90
        Me.dtReportSentOn.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.dtReportSentOn.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.dtReportSentOn.MonthCalendar.TodayButtonVisible = True
        Me.dtReportSentOn.Name = "dtReportSentOn"
        Me.dtReportSentOn.Size = New System.Drawing.Size(269, 29)
        Me.dtReportSentOn.TabIndex = 1
        Me.dtReportSentOn.WatermarkText = "Date of Report"
        '
        'LabelX3
        '
        Me.LabelX3.AutoSize = True
        '
        '
        '
        Me.LabelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX3.Location = New System.Drawing.Point(11, 122)
        Me.LabelX3.Name = "LabelX3"
        Me.LabelX3.Size = New System.Drawing.Size(86, 18)
        Me.LabelX3.TabIndex = 35
        Me.LabelX3.Text = "Date of Report"
        '
        'LabelX2
        '
        Me.LabelX2.AutoSize = True
        '
        '
        '
        Me.LabelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX2.Location = New System.Drawing.Point(11, 158)
        Me.LabelX2.Name = "LabelX2"
        Me.LabelX2.Size = New System.Drawing.Size(99, 18)
        Me.LabelX2.TabIndex = 37
        Me.LabelX2.Text = "Nature of Report"
        '
        'cmbNatureOfReport
        '
        Me.cmbNatureOfReport.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbNatureOfReport.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbNatureOfReport.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbNatureOfReport.FocusHighlightEnabled = True
        Me.cmbNatureOfReport.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbNatureOfReport.ForeColor = System.Drawing.Color.Black
        Me.cmbNatureOfReport.FormattingEnabled = True
        Me.cmbNatureOfReport.ItemHeight = 24
        Me.cmbNatureOfReport.Location = New System.Drawing.Point(116, 152)
        Me.cmbNatureOfReport.MaxDropDownItems = 35
        Me.cmbNatureOfReport.MaxLength = 255
        Me.cmbNatureOfReport.Name = "cmbNatureOfReport"
        Me.cmbNatureOfReport.Size = New System.Drawing.Size(269, 30)
        Me.cmbNatureOfReport.TabIndex = 2
        Me.cmbNatureOfReport.WatermarkText = "Nature of Report"
        '
        'LabelX4
        '
        Me.LabelX4.AutoSize = True
        '
        '
        '
        Me.LabelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX4.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX4.Location = New System.Drawing.Point(11, 12)
        Me.LabelX4.Name = "LabelX4"
        Me.LabelX4.Size = New System.Drawing.Size(474, 22)
        Me.LabelX4.TabIndex = 39
        Me.LabelX4.Text = "Please Fill the following details and press 'Save' to save the details."
        '
        'PanelEx1
        '
        Me.PanelEx1.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.PanelEx1.Controls.Add(Me.LabelX5)
        Me.PanelEx1.Controls.Add(Me.RSOCDatagrid)
        Me.PanelEx1.Controls.Add(Me.btnCancel)
        Me.PanelEx1.Controls.Add(Me.LabelX4)
        Me.PanelEx1.Controls.Add(Me.btnSave)
        Me.PanelEx1.Controls.Add(Me.cmbNatureOfReport)
        Me.PanelEx1.Controls.Add(Me.btnDontSave)
        Me.PanelEx1.Controls.Add(Me.LabelX2)
        Me.PanelEx1.Controls.Add(Me.txtReportSentTo)
        Me.PanelEx1.Controls.Add(Me.dtReportSentOn)
        Me.PanelEx1.Controls.Add(Me.LabelX1)
        Me.PanelEx1.Controls.Add(Me.LabelX3)
        Me.PanelEx1.DisabledBackColor = System.Drawing.Color.Empty
        Me.PanelEx1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelEx1.Location = New System.Drawing.Point(0, 0)
        Me.PanelEx1.Name = "PanelEx1"
        Me.PanelEx1.Size = New System.Drawing.Size(678, 406)
        Me.PanelEx1.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx1.Style.GradientAngle = 90
        Me.PanelEx1.TabIndex = 40
        '
        'LabelX5
        '
        Me.LabelX5.AutoSize = True
        '
        '
        '
        Me.LabelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX5.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX5.Location = New System.Drawing.Point(11, 204)
        Me.LabelX5.Name = "LabelX5"
        Me.LabelX5.Size = New System.Drawing.Size(155, 22)
        Me.LabelX5.TabIndex = 44
        Me.LabelX5.Text = "Reports already sent :"
        '
        'RSOCDatagrid
        '
        Me.RSOCDatagrid.AllowUserToAddRows = False
        Me.RSOCDatagrid.AllowUserToOrderColumns = True
        Me.RSOCDatagrid.AutoGenerateColumns = False
        Me.RSOCDatagrid.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.RSOCDatagrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.RSOCDatagrid.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.RSOCDatagrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.RSOCDatagrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.SOCNumberDataGridViewTextBoxColumn, Me.ReportSentToDataGridViewTextBoxColumn, Me.DateOfReportSentDataGridViewTextBoxColumn, Me.NatureOfReportsDataGridViewTextBoxColumn})
        Me.RSOCDatagrid.DataSource = Me.SOCReportRegisterBindingSource
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.RSOCDatagrid.DefaultCellStyle = DataGridViewCellStyle3
        Me.RSOCDatagrid.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.RSOCDatagrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.RSOCDatagrid.EnableHeadersVisualStyles = False
        Me.RSOCDatagrid.GridColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.RSOCDatagrid.Location = New System.Drawing.Point(0, 229)
        Me.RSOCDatagrid.MultiSelect = False
        Me.RSOCDatagrid.Name = "RSOCDatagrid"
        Me.RSOCDatagrid.ReadOnly = True
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.RSOCDatagrid.RowHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.RSOCDatagrid.RowHeadersWidth = 40
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.RSOCDatagrid.RowsDefaultCellStyle = DataGridViewCellStyle5
        Me.RSOCDatagrid.RowTemplate.Height = 40
        Me.RSOCDatagrid.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.RSOCDatagrid.SelectAllSignVisible = False
        Me.RSOCDatagrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.RSOCDatagrid.Size = New System.Drawing.Size(678, 177)
        Me.RSOCDatagrid.TabIndex = 43
        Me.RSOCDatagrid.TabStop = False
        '
        'SOCNumberDataGridViewTextBoxColumn
        '
        Me.SOCNumberDataGridViewTextBoxColumn.DataPropertyName = "SOCNumber"
        Me.SOCNumberDataGridViewTextBoxColumn.HeaderText = "SOC No."
        Me.SOCNumberDataGridViewTextBoxColumn.Name = "SOCNumberDataGridViewTextBoxColumn"
        Me.SOCNumberDataGridViewTextBoxColumn.ReadOnly = True
        Me.SOCNumberDataGridViewTextBoxColumn.Width = 80
        '
        'ReportSentToDataGridViewTextBoxColumn
        '
        Me.ReportSentToDataGridViewTextBoxColumn.DataPropertyName = "ReportSentTo"
        Me.ReportSentToDataGridViewTextBoxColumn.HeaderText = "Report Sent To"
        Me.ReportSentToDataGridViewTextBoxColumn.Name = "ReportSentToDataGridViewTextBoxColumn"
        Me.ReportSentToDataGridViewTextBoxColumn.ReadOnly = True
        Me.ReportSentToDataGridViewTextBoxColumn.Width = 250
        '
        'DateOfReportSentDataGridViewTextBoxColumn
        '
        Me.DateOfReportSentDataGridViewTextBoxColumn.DataPropertyName = "DateOfReportSent"
        DataGridViewCellStyle2.Format = "dd/MM/yyyy"
        Me.DateOfReportSentDataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle2
        Me.DateOfReportSentDataGridViewTextBoxColumn.HeaderText = "Date of Report"
        Me.DateOfReportSentDataGridViewTextBoxColumn.Name = "DateOfReportSentDataGridViewTextBoxColumn"
        Me.DateOfReportSentDataGridViewTextBoxColumn.ReadOnly = True
        Me.DateOfReportSentDataGridViewTextBoxColumn.Width = 120
        '
        'NatureOfReportsDataGridViewTextBoxColumn
        '
        Me.NatureOfReportsDataGridViewTextBoxColumn.DataPropertyName = "NatureOfReports"
        Me.NatureOfReportsDataGridViewTextBoxColumn.HeaderText = "Nature of Reports"
        Me.NatureOfReportsDataGridViewTextBoxColumn.Name = "NatureOfReportsDataGridViewTextBoxColumn"
        Me.NatureOfReportsDataGridViewTextBoxColumn.ReadOnly = True
        Me.NatureOfReportsDataGridViewTextBoxColumn.Width = 170
        '
        'SOCReportRegisterBindingSource
        '
        Me.SOCReportRegisterBindingSource.DataMember = "SOCReportRegister"
        Me.SOCReportRegisterBindingSource.DataSource = Me.FingerPrintDataSet1
        '
        'FingerPrintDataSet1
        '
        Me.FingerPrintDataSet1.DataSetName = "FingerPrintDataSet"
        Me.FingerPrintDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'btnCancel
        '
        Me.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(501, 141)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(96, 38)
        Me.btnCancel.TabIndex = 5
        Me.btnCancel.Text = "Cancel"
        '
        'SocReportRegisterTableAdapter
        '
        Me.SocReportRegisterTableAdapter.ClearBeforeFill = True
        '
        'PoliceStationListTableAdapter1
        '
        Me.PoliceStationListTableAdapter1.ClearBeforeFill = True
        '
        'FrmSOCReportSentStatus
        '
        Me.AcceptButton = Me.btnSave
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(678, 406)
        Me.Controls.Add(Me.PanelEx1)
        Me.DoubleBuffered = True
        Me.EnableGlass = False
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmSOCReportSentStatus"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Set Report Sent Status"
        Me.TitleText = "<b>Set Report Sent Status</b>"
        CType(Me.dtReportSentOn, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelEx1.ResumeLayout(False)
        Me.PanelEx1.PerformLayout()
        CType(Me.RSOCDatagrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SOCReportRegisterBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FingerPrintDataSet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnSave As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnDontSave As DevComponents.DotNetBar.ButtonX
    Friend WithEvents LabelX1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtReportSentTo As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents dtReportSentOn As DevComponents.Editors.DateTimeAdv.DateTimeInput
    Friend WithEvents LabelX3 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX2 As DevComponents.DotNetBar.LabelX
    Friend WithEvents cmbNatureOfReport As DevComponents.DotNetBar.Controls.ComboBoxEx
    Friend WithEvents LabelX4 As DevComponents.DotNetBar.LabelX
    Friend WithEvents PanelEx1 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents btnCancel As DevComponents.DotNetBar.ButtonX
    Friend WithEvents SocReportRegisterTableAdapter As FingerprintInformationSystem.FingerPrintDataSetTableAdapters.SOCReportRegisterTableAdapter
    Friend WithEvents FingerPrintDataSet1 As FingerprintInformationSystem.FingerPrintDataSet
    Friend WithEvents SOCReportRegisterBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents RSOCDatagrid As DevComponents.DotNetBar.Controls.DataGridViewX
    Friend WithEvents SOCNumberDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ReportSentToDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DateOfReportSentDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NatureOfReportsDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LabelX5 As DevComponents.DotNetBar.LabelX
    Friend WithEvents PoliceStationListTableAdapter1 As FingerprintInformationSystem.FingerPrintDataSetTableAdapters.PoliceStationListTableAdapter
End Class
