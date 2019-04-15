<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmIdentificationRegisterDE
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmIdentificationRegisterDE))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.LabelX4 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX5 = New DevComponents.DotNetBar.LabelX()
        Me.txtCulpritName = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.txtRemarks = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.btnSave = New DevComponents.DotNetBar.ButtonX()
        Me.btnCancel = New DevComponents.DotNetBar.ButtonX()
        Me.txtIdentificationNumber = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.lblcrt3 = New DevComponents.DotNetBar.LabelX()
        Me.lblIdentificationDate = New DevComponents.DotNetBar.LabelX()
        Me.lblIdentifiedBy = New DevComponents.DotNetBar.LabelX()
        Me.dtIdentificationDate = New DevComponents.Editors.DateTimeAdv.DateTimeInput()
        Me.cmbIdentifyingOfficer = New DevComponents.DotNetBar.Controls.ComboBoxEx()
        Me.lblCPsIdentified = New DevComponents.DotNetBar.LabelX()
        Me.txtCPsIdentified = New DevComponents.Editors.IntegerInput()
        Me.LabelX6 = New DevComponents.DotNetBar.LabelX()
        Me.PanelEx1 = New DevComponents.DotNetBar.PanelEx()
        Me.GroupPanel1 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.dgv = New DevComponents.DotNetBar.Controls.DataGridViewX()
        Me.CulpritsRegisterBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.FingerPrintDataSet1 = New FingerprintInformationSystem.FingerPrintDataSet()
        Me.btnEditCulprit = New DevComponents.DotNetBar.ButtonX()
        Me.btnClearFields = New DevComponents.DotNetBar.ButtonX()
        Me.btnRemoveCulprit = New DevComponents.DotNetBar.ButtonX()
        Me.btnAddCulprit = New DevComponents.DotNetBar.ButtonX()
        Me.LabelX21 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX14 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX7 = New DevComponents.DotNetBar.LabelX()
        Me.txtAddress = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.lblCPCountWarning = New DevComponents.DotNetBar.LabelX()
        Me.LabelX18 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX19 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX8 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX9 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX10 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX11 = New DevComponents.DotNetBar.LabelX()
        Me.txtClassification = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX17 = New DevComponents.DotNetBar.LabelX()
        Me.txtDANumber = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.cmbIdentifiedFrom = New DevComponents.DotNetBar.Controls.ComboBoxEx()
        Me.txtFingersIdentified = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX2 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX13 = New DevComponents.DotNetBar.LabelX()
        Me.lblSOCNumberWarning = New DevComponents.DotNetBar.LabelX()
        Me.LabelX20 = New DevComponents.DotNetBar.LabelX()
        Me.txtSOCNumber = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX1 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX16 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX15 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX12 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX3 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX37 = New DevComponents.DotNetBar.LabelX()
        Me.IdentificationRegisterTableAdapter1 = New FingerprintInformationSystem.FingerPrintDataSetTableAdapters.IdentificationRegisterTableAdapter()
        Me.SocRegisterTableAdapter1 = New FingerprintInformationSystem.FingerPrintDataSetTableAdapters.SOCRegisterTableAdapter()
        Me.SocRegisterAutoTextTableAdapter1 = New FingerprintInformationSystem.FingerPrintDataSetTableAdapters.SOCRegisterAutoTextTableAdapter()
        Me.CulpritsRegisterTableAdapter1 = New FingerprintInformationSystem.FingerPrintDataSetTableAdapters.CulpritsRegisterTableAdapter()
        Me.SlNumberDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IdentificationNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CulpritNameDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AddressDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CPsIdentifiedDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FingersIdentifiedDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.HenryClassificationDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DANumberDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IdentifiedFromDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IdentificationDetailsDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.dtIdentificationDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCPsIdentified, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelEx1.SuspendLayout()
        Me.GroupPanel1.SuspendLayout()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CulpritsRegisterBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FingerPrintDataSet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelX4
        '
        Me.LabelX4.AutoSize = True
        '
        '
        '
        Me.LabelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX4.Location = New System.Drawing.Point(449, 325)
        Me.LabelX4.Name = "LabelX4"
        Me.LabelX4.Size = New System.Drawing.Size(51, 18)
        Me.LabelX4.TabIndex = 3
        Me.LabelX4.Text = "Remarks"
        '
        'LabelX5
        '
        Me.LabelX5.AutoSize = True
        '
        '
        '
        Me.LabelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX5.Location = New System.Drawing.Point(12, 214)
        Me.LabelX5.Name = "LabelX5"
        Me.LabelX5.Size = New System.Drawing.Size(93, 18)
        Me.LabelX5.TabIndex = 4
        Me.LabelX5.Text = "Name of Culprit"
        '
        'txtCulpritName
        '
        Me.txtCulpritName.AcceptsReturn = True
        Me.txtCulpritName.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtCulpritName.Border.Class = "TextBoxBorder"
        Me.txtCulpritName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtCulpritName.ButtonCustom.Image = CType(resources.GetObject("txtCulpritName.ButtonCustom.Image"), System.Drawing.Image)
        Me.txtCulpritName.DisabledBackColor = System.Drawing.Color.White
        Me.txtCulpritName.FocusHighlightEnabled = True
        Me.txtCulpritName.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCulpritName.ForeColor = System.Drawing.Color.Black
        Me.txtCulpritName.Location = New System.Drawing.Point(116, 211)
        Me.txtCulpritName.MaxLength = 255
        Me.txtCulpritName.Name = "txtCulpritName"
        Me.txtCulpritName.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtCulpritName.Size = New System.Drawing.Size(214, 25)
        Me.txtCulpritName.TabIndex = 5
        Me.txtCulpritName.WatermarkBehavior = DevComponents.DotNetBar.eWatermarkBehavior.HideNonEmpty
        Me.txtCulpritName.WatermarkText = "Name of the identified criminal"
        '
        'txtRemarks
        '
        Me.txtRemarks.AcceptsReturn = True
        Me.txtRemarks.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtRemarks.Border.Class = "TextBoxBorder"
        Me.txtRemarks.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtRemarks.ButtonCustom.Image = CType(resources.GetObject("txtRemarks.ButtonCustom.Image"), System.Drawing.Image)
        Me.txtRemarks.DisabledBackColor = System.Drawing.Color.White
        Me.txtRemarks.FocusHighlightEnabled = True
        Me.txtRemarks.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.ForeColor = System.Drawing.Color.Black
        Me.txtRemarks.Location = New System.Drawing.Point(545, 323)
        Me.txtRemarks.MaxLength = 0
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtRemarks.Size = New System.Drawing.Size(255, 55)
        Me.txtRemarks.TabIndex = 12
        Me.txtRemarks.WatermarkBehavior = DevComponents.DotNetBar.eWatermarkBehavior.HideNonEmpty
        Me.txtRemarks.WatermarkText = "Remarks"
        '
        'btnSave
        '
        Me.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnSave.Location = New System.Drawing.Point(625, 387)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(105, 34)
        Me.btnSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnSave.TabIndex = 16
        Me.btnSave.Text = "Save Record"
        '
        'btnCancel
        '
        Me.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnCancel.Location = New System.Drawing.Point(889, 387)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(105, 34)
        Me.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnCancel.TabIndex = 18
        Me.btnCancel.Text = "Cancel && Exit"
        '
        'txtIdentificationNumber
        '
        Me.txtIdentificationNumber.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtIdentificationNumber.Border.Class = "TextBoxBorder"
        Me.txtIdentificationNumber.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtIdentificationNumber.DisabledBackColor = System.Drawing.Color.White
        Me.txtIdentificationNumber.FocusHighlightEnabled = True
        Me.txtIdentificationNumber.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIdentificationNumber.ForeColor = System.Drawing.Color.Black
        Me.txtIdentificationNumber.Location = New System.Drawing.Point(119, 11)
        Me.txtIdentificationNumber.MaxLength = 10
        Me.txtIdentificationNumber.Name = "txtIdentificationNumber"
        Me.txtIdentificationNumber.PreventEnterBeep = True
        Me.txtIdentificationNumber.Size = New System.Drawing.Size(64, 25)
        Me.txtIdentificationNumber.TabIndex = 0
        Me.txtIdentificationNumber.WatermarkText = "ID No"
        '
        'lblcrt3
        '
        Me.lblcrt3.AutoSize = True
        '
        '
        '
        Me.lblcrt3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblcrt3.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcrt3.Location = New System.Drawing.Point(188, -102)
        Me.lblcrt3.Name = "lblcrt3"
        Me.lblcrt3.Size = New System.Drawing.Size(7, 22)
        Me.lblcrt3.TabIndex = 171
        Me.lblcrt3.Text = "<font color=""#ED1C24"">*</font><b></b>"
        '
        'lblIdentificationDate
        '
        Me.lblIdentificationDate.AutoSize = True
        '
        '
        '
        Me.lblIdentificationDate.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblIdentificationDate.Location = New System.Drawing.Point(404, 14)
        Me.lblIdentificationDate.Name = "lblIdentificationDate"
        Me.lblIdentificationDate.Size = New System.Drawing.Size(108, 18)
        Me.lblIdentificationDate.TabIndex = 168
        Me.lblIdentificationDate.Text = "Identification Date"
        '
        'lblIdentifiedBy
        '
        Me.lblIdentifiedBy.AutoSize = True
        '
        '
        '
        Me.lblIdentifiedBy.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblIdentifiedBy.Location = New System.Drawing.Point(717, 14)
        Me.lblIdentifiedBy.Name = "lblIdentifiedBy"
        Me.lblIdentifiedBy.Size = New System.Drawing.Size(75, 18)
        Me.lblIdentifiedBy.TabIndex = 166
        Me.lblIdentifiedBy.Text = "Identified By"
        '
        'dtIdentificationDate
        '
        Me.dtIdentificationDate.AutoAdvance = True
        Me.dtIdentificationDate.AutoSelectDate = True
        Me.dtIdentificationDate.BackColor = System.Drawing.SystemColors.Window
        '
        '
        '
        Me.dtIdentificationDate.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.dtIdentificationDate.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.dtIdentificationDate.ButtonClear.Image = CType(resources.GetObject("dtIdentificationDate.ButtonClear.Image"), System.Drawing.Image)
        Me.dtIdentificationDate.ButtonClear.Visible = True
        Me.dtIdentificationDate.ButtonDropDown.Visible = True
        Me.dtIdentificationDate.CustomFormat = "dd/MM/yyyy"
        Me.dtIdentificationDate.FocusHighlightEnabled = True
        Me.dtIdentificationDate.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtIdentificationDate.Format = DevComponents.Editors.eDateTimePickerFormat.Custom
        Me.dtIdentificationDate.IsPopupCalendarOpen = False
        Me.dtIdentificationDate.Location = New System.Drawing.Point(528, 11)
        Me.dtIdentificationDate.MaxDate = New Date(2100, 12, 31, 0, 0, 0, 0)
        Me.dtIdentificationDate.MinDate = New Date(1900, 1, 1, 0, 0, 0, 0)
        '
        '
        '
        '
        '
        '
        Me.dtIdentificationDate.MonthCalendar.BackgroundStyle.BackColor = System.Drawing.SystemColors.Window
        Me.dtIdentificationDate.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.dtIdentificationDate.MonthCalendar.ClearButtonVisible = True
        '
        '
        '
        Me.dtIdentificationDate.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2
        Me.dtIdentificationDate.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90
        Me.dtIdentificationDate.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.dtIdentificationDate.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.dtIdentificationDate.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder
        Me.dtIdentificationDate.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1
        Me.dtIdentificationDate.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.dtIdentificationDate.MonthCalendar.DaySize = New System.Drawing.Size(30, 15)
        Me.dtIdentificationDate.MonthCalendar.DisplayMonth = New Date(2008, 7, 1, 0, 0, 0, 0)
        Me.dtIdentificationDate.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday
        '
        '
        '
        Me.dtIdentificationDate.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.dtIdentificationDate.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90
        Me.dtIdentificationDate.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.dtIdentificationDate.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.dtIdentificationDate.MonthCalendar.TodayButtonVisible = True
        Me.dtIdentificationDate.Name = "dtIdentificationDate"
        Me.dtIdentificationDate.Size = New System.Drawing.Size(130, 25)
        Me.dtIdentificationDate.TabIndex = 3
        Me.dtIdentificationDate.WatermarkText = "Date of ID"
        '
        'cmbIdentifyingOfficer
        '
        Me.cmbIdentifyingOfficer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbIdentifyingOfficer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbIdentifyingOfficer.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbIdentifyingOfficer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbIdentifyingOfficer.FocusHighlightEnabled = True
        Me.cmbIdentifyingOfficer.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbIdentifyingOfficer.ForeColor = System.Drawing.Color.Black
        Me.cmbIdentifyingOfficer.FormattingEnabled = True
        Me.cmbIdentifyingOfficer.ItemHeight = 20
        Me.cmbIdentifyingOfficer.Location = New System.Drawing.Point(808, 10)
        Me.cmbIdentifyingOfficer.MaxDropDownItems = 15
        Me.cmbIdentifyingOfficer.MaxLength = 255
        Me.cmbIdentifyingOfficer.Name = "cmbIdentifyingOfficer"
        Me.cmbIdentifyingOfficer.Size = New System.Drawing.Size(208, 26)
        Me.cmbIdentifyingOfficer.TabIndex = 4
        Me.cmbIdentifyingOfficer.WatermarkBehavior = DevComponents.DotNetBar.eWatermarkBehavior.HideNonEmpty
        Me.cmbIdentifyingOfficer.WatermarkText = "Identifying Officer"
        '
        'lblCPsIdentified
        '
        Me.lblCPsIdentified.AutoSize = True
        '
        '
        '
        Me.lblCPsIdentified.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblCPsIdentified.Location = New System.Drawing.Point(12, 326)
        Me.lblCPsIdentified.Name = "lblCPsIdentified"
        Me.lblCPsIdentified.Size = New System.Drawing.Size(85, 18)
        Me.lblCPsIdentified.TabIndex = 165
        Me.lblCPsIdentified.Text = "No. of CPs IDd"
        '
        'txtCPsIdentified
        '
        '
        '
        '
        Me.txtCPsIdentified.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.txtCPsIdentified.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtCPsIdentified.FocusHighlightEnabled = True
        Me.txtCPsIdentified.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCPsIdentified.InputHorizontalAlignment = DevComponents.Editors.eHorizontalAlignment.Left
        Me.txtCPsIdentified.Location = New System.Drawing.Point(116, 323)
        Me.txtCPsIdentified.MaxValue = 999
        Me.txtCPsIdentified.MinValue = 1
        Me.txtCPsIdentified.Name = "txtCPsIdentified"
        Me.txtCPsIdentified.ShowUpDown = True
        Me.txtCPsIdentified.Size = New System.Drawing.Size(60, 25)
        Me.txtCPsIdentified.TabIndex = 7
        Me.txtCPsIdentified.Value = 1
        Me.txtCPsIdentified.WatermarkText = "CPs"
        '
        'LabelX6
        '
        Me.LabelX6.AutoSize = True
        '
        '
        '
        Me.LabelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX6.Location = New System.Drawing.Point(11, 14)
        Me.LabelX6.Name = "LabelX6"
        Me.LabelX6.Size = New System.Drawing.Size(102, 18)
        Me.LabelX6.TabIndex = 174
        Me.LabelX6.Text = "Identification No."
        '
        'PanelEx1
        '
        Me.PanelEx1.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.PanelEx1.Controls.Add(Me.GroupPanel1)
        Me.PanelEx1.Controls.Add(Me.lblSOCNumberWarning)
        Me.PanelEx1.Controls.Add(Me.LabelX20)
        Me.PanelEx1.Controls.Add(Me.txtSOCNumber)
        Me.PanelEx1.Controls.Add(Me.LabelX1)
        Me.PanelEx1.Controls.Add(Me.LabelX16)
        Me.PanelEx1.Controls.Add(Me.LabelX15)
        Me.PanelEx1.Controls.Add(Me.LabelX12)
        Me.PanelEx1.Controls.Add(Me.LabelX3)
        Me.PanelEx1.Controls.Add(Me.LabelX37)
        Me.PanelEx1.Controls.Add(Me.LabelX6)
        Me.PanelEx1.Controls.Add(Me.txtIdentificationNumber)
        Me.PanelEx1.Controls.Add(Me.lblcrt3)
        Me.PanelEx1.Controls.Add(Me.lblIdentificationDate)
        Me.PanelEx1.Controls.Add(Me.lblIdentifiedBy)
        Me.PanelEx1.Controls.Add(Me.cmbIdentifyingOfficer)
        Me.PanelEx1.Controls.Add(Me.dtIdentificationDate)
        Me.PanelEx1.DisabledBackColor = System.Drawing.Color.Empty
        Me.PanelEx1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelEx1.Location = New System.Drawing.Point(0, 0)
        Me.PanelEx1.Name = "PanelEx1"
        Me.PanelEx1.Size = New System.Drawing.Size(1041, 526)
        Me.PanelEx1.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx1.Style.GradientAngle = 90
        Me.PanelEx1.TabIndex = 176
        '
        'GroupPanel1
        '
        Me.GroupPanel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.GroupPanel1.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanel1.Controls.Add(Me.dgv)
        Me.GroupPanel1.Controls.Add(Me.btnEditCulprit)
        Me.GroupPanel1.Controls.Add(Me.btnClearFields)
        Me.GroupPanel1.Controls.Add(Me.btnRemoveCulprit)
        Me.GroupPanel1.Controls.Add(Me.btnAddCulprit)
        Me.GroupPanel1.Controls.Add(Me.LabelX21)
        Me.GroupPanel1.Controls.Add(Me.LabelX14)
        Me.GroupPanel1.Controls.Add(Me.LabelX7)
        Me.GroupPanel1.Controls.Add(Me.txtRemarks)
        Me.GroupPanel1.Controls.Add(Me.txtAddress)
        Me.GroupPanel1.Controls.Add(Me.txtCPsIdentified)
        Me.GroupPanel1.Controls.Add(Me.lblCPsIdentified)
        Me.GroupPanel1.Controls.Add(Me.txtCulpritName)
        Me.GroupPanel1.Controls.Add(Me.lblCPCountWarning)
        Me.GroupPanel1.Controls.Add(Me.btnSave)
        Me.GroupPanel1.Controls.Add(Me.btnCancel)
        Me.GroupPanel1.Controls.Add(Me.LabelX18)
        Me.GroupPanel1.Controls.Add(Me.LabelX19)
        Me.GroupPanel1.Controls.Add(Me.LabelX8)
        Me.GroupPanel1.Controls.Add(Me.LabelX9)
        Me.GroupPanel1.Controls.Add(Me.LabelX10)
        Me.GroupPanel1.Controls.Add(Me.LabelX11)
        Me.GroupPanel1.Controls.Add(Me.txtClassification)
        Me.GroupPanel1.Controls.Add(Me.LabelX17)
        Me.GroupPanel1.Controls.Add(Me.txtDANumber)
        Me.GroupPanel1.Controls.Add(Me.cmbIdentifiedFrom)
        Me.GroupPanel1.Controls.Add(Me.txtFingersIdentified)
        Me.GroupPanel1.Controls.Add(Me.LabelX2)
        Me.GroupPanel1.Controls.Add(Me.LabelX4)
        Me.GroupPanel1.Controls.Add(Me.LabelX13)
        Me.GroupPanel1.Controls.Add(Me.LabelX5)
        Me.GroupPanel1.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel1.Location = New System.Drawing.Point(11, 66)
        Me.GroupPanel1.Name = "GroupPanel1"
        Me.GroupPanel1.Size = New System.Drawing.Size(1018, 457)
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
        Me.GroupPanel1.TabIndex = 199
        Me.GroupPanel1.Text = "Culprit Details"
        '
        'dgv
        '
        Me.dgv.AllowUserToAddRows = False
        Me.dgv.AutoGenerateColumns = False
        Me.dgv.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgv.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.SlNumberDataGridViewTextBoxColumn, Me.IdentificationNumber, Me.CulpritNameDataGridViewTextBoxColumn, Me.AddressDataGridViewTextBoxColumn, Me.CPsIdentifiedDataGridViewTextBoxColumn, Me.FingersIdentifiedDataGridViewTextBoxColumn, Me.HenryClassificationDataGridViewTextBoxColumn, Me.DANumberDataGridViewTextBoxColumn, Me.IdentifiedFromDataGridViewTextBoxColumn, Me.IdentificationDetailsDataGridViewTextBoxColumn})
        Me.dgv.DataSource = Me.CulpritsRegisterBindingSource
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv.DefaultCellStyle = DataGridViewCellStyle3
        Me.dgv.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgv.EnableHeadersVisualStyles = False
        Me.dgv.GridColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.dgv.Location = New System.Drawing.Point(8, 3)
        Me.dgv.MultiSelect = False
        Me.dgv.Name = "dgv"
        Me.dgv.ReadOnly = True
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv.RowHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.dgv.RowHeadersWidth = 50
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv.RowsDefaultCellStyle = DataGridViewCellStyle5
        Me.dgv.RowTemplate.Height = 70
        Me.dgv.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv.SelectAllSignVisible = False
        Me.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv.Size = New System.Drawing.Size(996, 202)
        Me.dgv.TabIndex = 203
        Me.dgv.TabStop = False
        '
        'CulpritsRegisterBindingSource
        '
        Me.CulpritsRegisterBindingSource.DataMember = "CulpritsRegister"
        Me.CulpritsRegisterBindingSource.DataSource = Me.FingerPrintDataSet1
        '
        'FingerPrintDataSet1
        '
        Me.FingerPrintDataSet1.DataSetName = "FingerPrintDataSet"
        Me.FingerPrintDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'btnEditCulprit
        '
        Me.btnEditCulprit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnEditCulprit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnEditCulprit.Location = New System.Drawing.Point(889, 265)
        Me.btnEditCulprit.Name = "btnEditCulprit"
        Me.btnEditCulprit.Size = New System.Drawing.Size(105, 34)
        Me.btnEditCulprit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnEditCulprit.TabIndex = 14
        Me.btnEditCulprit.Text = "Edit Culprit"
        '
        'btnClearFields
        '
        Me.btnClearFields.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnClearFields.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnClearFields.Location = New System.Drawing.Point(757, 387)
        Me.btnClearFields.Name = "btnClearFields"
        Me.btnClearFields.Size = New System.Drawing.Size(105, 34)
        Me.btnClearFields.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnClearFields.TabIndex = 17
        Me.btnClearFields.Text = "Clear Fields"
        '
        'btnRemoveCulprit
        '
        Me.btnRemoveCulprit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnRemoveCulprit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnRemoveCulprit.Location = New System.Drawing.Point(889, 315)
        Me.btnRemoveCulprit.Name = "btnRemoveCulprit"
        Me.btnRemoveCulprit.Size = New System.Drawing.Size(105, 34)
        Me.btnRemoveCulprit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnRemoveCulprit.TabIndex = 15
        Me.btnRemoveCulprit.Text = "Remove Culprit"
        '
        'btnAddCulprit
        '
        Me.btnAddCulprit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnAddCulprit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnAddCulprit.Location = New System.Drawing.Point(889, 215)
        Me.btnAddCulprit.Name = "btnAddCulprit"
        Me.btnAddCulprit.Size = New System.Drawing.Size(105, 34)
        Me.btnAddCulprit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnAddCulprit.TabIndex = 13
        Me.btnAddCulprit.Text = "Add Culprit"
        '
        'LabelX21
        '
        Me.LabelX21.AutoSize = True
        '
        '
        '
        Me.LabelX21.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX21.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX21.Location = New System.Drawing.Point(801, 215)
        Me.LabelX21.Name = "LabelX21"
        Me.LabelX21.Size = New System.Drawing.Size(7, 22)
        Me.LabelX21.TabIndex = 202
        Me.LabelX21.Text = "<font color=""#ED1C24"">*</font><b></b>"
        '
        'LabelX14
        '
        Me.LabelX14.AutoSize = True
        '
        '
        '
        Me.LabelX14.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX14.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX14.Location = New System.Drawing.Point(331, 356)
        Me.LabelX14.Name = "LabelX14"
        Me.LabelX14.Size = New System.Drawing.Size(7, 22)
        Me.LabelX14.TabIndex = 201
        Me.LabelX14.Text = "<font color=""#ED1C24"">*</font><b></b>"
        '
        'LabelX7
        '
        Me.LabelX7.AutoSize = True
        '
        '
        '
        Me.LabelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX7.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX7.Location = New System.Drawing.Point(331, 215)
        Me.LabelX7.Name = "LabelX7"
        Me.LabelX7.Size = New System.Drawing.Size(7, 22)
        Me.LabelX7.TabIndex = 200
        Me.LabelX7.Text = "<font color=""#ED1C24"">*</font><b></b>"
        '
        'txtAddress
        '
        Me.txtAddress.AcceptsReturn = True
        Me.txtAddress.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtAddress.Border.Class = "TextBoxBorder"
        Me.txtAddress.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtAddress.ButtonCustom.Image = CType(resources.GetObject("txtAddress.ButtonCustom.Image"), System.Drawing.Image)
        Me.txtAddress.DisabledBackColor = System.Drawing.Color.White
        Me.txtAddress.FocusHighlightEnabled = True
        Me.txtAddress.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress.ForeColor = System.Drawing.Color.Black
        Me.txtAddress.Location = New System.Drawing.Point(116, 241)
        Me.txtAddress.MaxLength = 0
        Me.txtAddress.Multiline = True
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtAddress.Size = New System.Drawing.Size(214, 77)
        Me.txtAddress.TabIndex = 6
        Me.txtAddress.WatermarkBehavior = DevComponents.DotNetBar.eWatermarkBehavior.HideNonEmpty
        Me.txtAddress.WatermarkText = "Address of the identified criminal"
        '
        'lblCPCountWarning
        '
        Me.lblCPCountWarning.AutoSize = True
        '
        '
        '
        Me.lblCPCountWarning.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblCPCountWarning.ForeColor = System.Drawing.Color.Red
        Me.lblCPCountWarning.Location = New System.Drawing.Point(194, 326)
        Me.lblCPCountWarning.Name = "lblCPCountWarning"
        Me.lblCPCountWarning.Size = New System.Drawing.Size(159, 18)
        Me.lblCPCountWarning.TabIndex = 198
        Me.lblCPCountWarning.Text = "Error: CPs remaining is Zero"
        Me.lblCPCountWarning.Visible = False
        '
        'LabelX18
        '
        Me.LabelX18.AutoSize = True
        '
        '
        '
        Me.LabelX18.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX18.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX18.Location = New System.Drawing.Point(317, 217)
        Me.LabelX18.Name = "LabelX18"
        Me.LabelX18.Size = New System.Drawing.Size(7, 22)
        Me.LabelX18.TabIndex = 192
        Me.LabelX18.Text = "<font color=""#ED1C24"">*</font><b></b>"
        '
        'LabelX19
        '
        Me.LabelX19.AutoSize = True
        '
        '
        '
        Me.LabelX19.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX19.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX19.Location = New System.Drawing.Point(331, 242)
        Me.LabelX19.Name = "LabelX19"
        Me.LabelX19.Size = New System.Drawing.Size(7, 22)
        Me.LabelX19.TabIndex = 193
        Me.LabelX19.Text = "<font color=""#ED1C24"">*</font><b></b>"
        '
        'LabelX8
        '
        Me.LabelX8.AutoSize = True
        '
        '
        '
        Me.LabelX8.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX8.Location = New System.Drawing.Point(449, 262)
        Me.LabelX8.Name = "LabelX8"
        Me.LabelX8.Size = New System.Drawing.Size(70, 18)
        Me.LabelX8.TabIndex = 179
        Me.LabelX8.Text = "DA Number"
        '
        'LabelX9
        '
        Me.LabelX9.AutoSize = True
        '
        '
        '
        Me.LabelX9.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX9.Location = New System.Drawing.Point(449, 214)
        Me.LabelX9.Name = "LabelX9"
        Me.LabelX9.Size = New System.Drawing.Size(77, 18)
        Me.LabelX9.TabIndex = 180
        Me.LabelX9.Text = "Classification"
        '
        'LabelX10
        '
        Me.LabelX10.AutoSize = True
        '
        '
        '
        Me.LabelX10.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX10.Location = New System.Drawing.Point(12, 356)
        Me.LabelX10.Name = "LabelX10"
        Me.LabelX10.Size = New System.Drawing.Size(102, 18)
        Me.LabelX10.TabIndex = 181
        Me.LabelX10.Text = "Fingers Identified"
        '
        'LabelX11
        '
        Me.LabelX11.AutoSize = True
        '
        '
        '
        Me.LabelX11.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX11.Location = New System.Drawing.Point(449, 295)
        Me.LabelX11.Name = "LabelX11"
        Me.LabelX11.Size = New System.Drawing.Size(90, 18)
        Me.LabelX11.TabIndex = 182
        Me.LabelX11.Text = "Identified From"
        '
        'txtClassification
        '
        Me.txtClassification.AcceptsReturn = True
        Me.txtClassification.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtClassification.Border.Class = "TextBoxBorder"
        Me.txtClassification.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtClassification.ButtonCustom.Image = CType(resources.GetObject("txtClassification.ButtonCustom.Image"), System.Drawing.Image)
        Me.txtClassification.DisabledBackColor = System.Drawing.Color.White
        Me.txtClassification.FocusHighlightEnabled = True
        Me.txtClassification.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtClassification.ForeColor = System.Drawing.Color.Black
        Me.txtClassification.Location = New System.Drawing.Point(545, 211)
        Me.txtClassification.MaxLength = 255
        Me.txtClassification.Multiline = True
        Me.txtClassification.Name = "txtClassification"
        Me.txtClassification.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtClassification.Size = New System.Drawing.Size(255, 41)
        Me.txtClassification.TabIndex = 9
        Me.txtClassification.WatermarkBehavior = DevComponents.DotNetBar.eWatermarkBehavior.HideNonEmpty
        Me.txtClassification.WatermarkText = "Henry Classification"
        '
        'LabelX17
        '
        Me.LabelX17.AutoSize = True
        '
        '
        '
        Me.LabelX17.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX17.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX17.Location = New System.Drawing.Point(677, 295)
        Me.LabelX17.Name = "LabelX17"
        Me.LabelX17.Size = New System.Drawing.Size(7, 22)
        Me.LabelX17.TabIndex = 191
        Me.LabelX17.Text = "<font color=""#ED1C24"">*</font><b></b>"
        '
        'txtDANumber
        '
        Me.txtDANumber.AcceptsReturn = True
        Me.txtDANumber.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtDANumber.Border.Class = "TextBoxBorder"
        Me.txtDANumber.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtDANumber.ButtonCustom.Image = CType(resources.GetObject("txtDANumber.ButtonCustom.Image"), System.Drawing.Image)
        Me.txtDANumber.DisabledBackColor = System.Drawing.Color.White
        Me.txtDANumber.FocusHighlightEnabled = True
        Me.txtDANumber.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDANumber.ForeColor = System.Drawing.Color.Black
        Me.txtDANumber.Location = New System.Drawing.Point(545, 259)
        Me.txtDANumber.MaxLength = 255
        Me.txtDANumber.Name = "txtDANumber"
        Me.txtDANumber.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtDANumber.Size = New System.Drawing.Size(130, 25)
        Me.txtDANumber.TabIndex = 10
        Me.txtDANumber.WatermarkBehavior = DevComponents.DotNetBar.eWatermarkBehavior.HideNonEmpty
        Me.txtDANumber.WatermarkText = "DA Number"
        '
        'cmbIdentifiedFrom
        '
        Me.cmbIdentifiedFrom.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbIdentifiedFrom.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbIdentifiedFrom.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbIdentifiedFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbIdentifiedFrom.FocusHighlightEnabled = True
        Me.cmbIdentifiedFrom.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbIdentifiedFrom.ForeColor = System.Drawing.Color.Black
        Me.cmbIdentifiedFrom.FormattingEnabled = True
        Me.cmbIdentifiedFrom.ItemHeight = 20
        Me.cmbIdentifiedFrom.Location = New System.Drawing.Point(545, 291)
        Me.cmbIdentifiedFrom.MaxDropDownItems = 15
        Me.cmbIdentifiedFrom.MaxLength = 255
        Me.cmbIdentifiedFrom.Name = "cmbIdentifiedFrom"
        Me.cmbIdentifiedFrom.Size = New System.Drawing.Size(130, 26)
        Me.cmbIdentifiedFrom.Sorted = True
        Me.cmbIdentifiedFrom.TabIndex = 11
        Me.cmbIdentifiedFrom.WatermarkBehavior = DevComponents.DotNetBar.eWatermarkBehavior.HideNonEmpty
        Me.cmbIdentifiedFrom.WatermarkText = "Identified From"
        '
        'txtFingersIdentified
        '
        Me.txtFingersIdentified.AcceptsReturn = True
        Me.txtFingersIdentified.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtFingersIdentified.Border.Class = "TextBoxBorder"
        Me.txtFingersIdentified.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtFingersIdentified.ButtonCustom.Image = CType(resources.GetObject("txtFingersIdentified.ButtonCustom.Image"), System.Drawing.Image)
        Me.txtFingersIdentified.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtFingersIdentified.DisabledBackColor = System.Drawing.Color.White
        Me.txtFingersIdentified.FocusHighlightEnabled = True
        Me.txtFingersIdentified.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFingersIdentified.ForeColor = System.Drawing.Color.Black
        Me.txtFingersIdentified.Location = New System.Drawing.Point(116, 353)
        Me.txtFingersIdentified.MaxLength = 255
        Me.txtFingersIdentified.Name = "txtFingersIdentified"
        Me.txtFingersIdentified.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtFingersIdentified.Size = New System.Drawing.Size(214, 25)
        Me.txtFingersIdentified.TabIndex = 8
        Me.txtFingersIdentified.WatermarkBehavior = DevComponents.DotNetBar.eWatermarkBehavior.HideNonEmpty
        Me.txtFingersIdentified.WatermarkText = "Fingers Identified (RT, RI etc.)"
        '
        'LabelX2
        '
        Me.LabelX2.AutoSize = True
        '
        '
        '
        Me.LabelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX2.Location = New System.Drawing.Point(12, 246)
        Me.LabelX2.Name = "LabelX2"
        Me.LabelX2.Size = New System.Drawing.Size(40, 18)
        Me.LabelX2.TabIndex = 176
        Me.LabelX2.Text = "Adress"
        '
        'LabelX13
        '
        Me.LabelX13.AutoSize = True
        '
        '
        '
        Me.LabelX13.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX13.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX13.Location = New System.Drawing.Point(179, 327)
        Me.LabelX13.Name = "LabelX13"
        Me.LabelX13.Size = New System.Drawing.Size(7, 22)
        Me.LabelX13.TabIndex = 187
        Me.LabelX13.Text = "<font color=""#ED1C24"">*</font><b></b>"
        '
        'lblSOCNumberWarning
        '
        Me.lblSOCNumberWarning.AutoSize = True
        '
        '
        '
        Me.lblSOCNumberWarning.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblSOCNumberWarning.ForeColor = System.Drawing.Color.Red
        Me.lblSOCNumberWarning.Location = New System.Drawing.Point(271, 42)
        Me.lblSOCNumberWarning.Name = "lblSOCNumberWarning"
        Me.lblSOCNumberWarning.Size = New System.Drawing.Size(144, 18)
        Me.lblSOCNumberWarning.TabIndex = 197
        Me.lblSOCNumberWarning.Text = "Error: SOC No. not found"
        Me.lblSOCNumberWarning.Visible = False
        '
        'LabelX20
        '
        Me.LabelX20.AutoSize = True
        '
        '
        '
        Me.LabelX20.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX20.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX20.Location = New System.Drawing.Point(185, 15)
        Me.LabelX20.Name = "LabelX20"
        Me.LabelX20.Size = New System.Drawing.Size(7, 22)
        Me.LabelX20.TabIndex = 196
        Me.LabelX20.Text = "<font color=""#ED1C24"">*</font><b></b>"
        '
        'txtSOCNumber
        '
        Me.txtSOCNumber.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtSOCNumber.Border.Class = "TextBoxBorder"
        Me.txtSOCNumber.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtSOCNumber.DisabledBackColor = System.Drawing.Color.White
        Me.txtSOCNumber.FocusHighlightEnabled = True
        Me.txtSOCNumber.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSOCNumber.ForeColor = System.Drawing.Color.Black
        Me.txtSOCNumber.Location = New System.Drawing.Point(271, 11)
        Me.txtSOCNumber.MaxLength = 10
        Me.txtSOCNumber.Name = "txtSOCNumber"
        Me.txtSOCNumber.PreventEnterBeep = True
        Me.txtSOCNumber.Size = New System.Drawing.Size(85, 25)
        Me.txtSOCNumber.TabIndex = 1
        Me.txtSOCNumber.WatermarkText = "SOC No"
        '
        'LabelX1
        '
        Me.LabelX1.AutoSize = True
        '
        '
        '
        Me.LabelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX1.Location = New System.Drawing.Point(216, 14)
        Me.LabelX1.Name = "LabelX1"
        Me.LabelX1.Size = New System.Drawing.Size(49, 18)
        Me.LabelX1.TabIndex = 194
        Me.LabelX1.Text = "SOC No."
        '
        'LabelX16
        '
        Me.LabelX16.AutoSize = True
        '
        '
        '
        Me.LabelX16.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX16.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX16.Location = New System.Drawing.Point(349, 281)
        Me.LabelX16.Name = "LabelX16"
        Me.LabelX16.Size = New System.Drawing.Size(7, 22)
        Me.LabelX16.TabIndex = 190
        Me.LabelX16.Text = "<font color=""#ED1C24"">*</font><b></b>"
        '
        'LabelX15
        '
        Me.LabelX15.AutoSize = True
        '
        '
        '
        Me.LabelX15.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX15.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX15.Location = New System.Drawing.Point(349, 205)
        Me.LabelX15.Name = "LabelX15"
        Me.LabelX15.Size = New System.Drawing.Size(7, 22)
        Me.LabelX15.TabIndex = 189
        Me.LabelX15.Text = "<font color=""#ED1C24"">*</font><b></b>"
        '
        'LabelX12
        '
        Me.LabelX12.AutoSize = True
        '
        '
        '
        Me.LabelX12.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX12.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX12.Location = New System.Drawing.Point(1019, 15)
        Me.LabelX12.Name = "LabelX12"
        Me.LabelX12.Size = New System.Drawing.Size(7, 22)
        Me.LabelX12.TabIndex = 186
        Me.LabelX12.Text = "<font color=""#ED1C24"">*</font><b></b>"
        '
        'LabelX3
        '
        Me.LabelX3.AutoSize = True
        '
        '
        '
        Me.LabelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX3.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX3.Location = New System.Drawing.Point(661, 15)
        Me.LabelX3.Name = "LabelX3"
        Me.LabelX3.Size = New System.Drawing.Size(7, 22)
        Me.LabelX3.TabIndex = 185
        Me.LabelX3.Text = "<font color=""#ED1C24"">*</font><b></b>"
        '
        'LabelX37
        '
        Me.LabelX37.AutoSize = True
        '
        '
        '
        Me.LabelX37.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX37.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX37.Location = New System.Drawing.Point(358, 15)
        Me.LabelX37.Name = "LabelX37"
        Me.LabelX37.Size = New System.Drawing.Size(7, 22)
        Me.LabelX37.TabIndex = 183
        Me.LabelX37.Text = "<font color=""#ED1C24"">*</font><b></b>"
        '
        'IdentificationRegisterTableAdapter1
        '
        Me.IdentificationRegisterTableAdapter1.ClearBeforeFill = True
        '
        'SocRegisterTableAdapter1
        '
        Me.SocRegisterTableAdapter1.ClearBeforeFill = True
        '
        'SocRegisterAutoTextTableAdapter1
        '
        Me.SocRegisterAutoTextTableAdapter1.ClearBeforeFill = True
        '
        'CulpritsRegisterTableAdapter1
        '
        Me.CulpritsRegisterTableAdapter1.ClearBeforeFill = True
        '
        'SlNumberDataGridViewTextBoxColumn
        '
        Me.SlNumberDataGridViewTextBoxColumn.DataPropertyName = "SlNumber"
        Me.SlNumberDataGridViewTextBoxColumn.HeaderText = "SlNumber"
        Me.SlNumberDataGridViewTextBoxColumn.Name = "SlNumberDataGridViewTextBoxColumn"
        Me.SlNumberDataGridViewTextBoxColumn.ReadOnly = True
        '
        'IdentificationNumber
        '
        Me.IdentificationNumber.DataPropertyName = "IdentificationNumber"
        Me.IdentificationNumber.HeaderText = "IdentificationNumber"
        Me.IdentificationNumber.Name = "IdentificationNumber"
        Me.IdentificationNumber.ReadOnly = True
        '
        'CulpritNameDataGridViewTextBoxColumn
        '
        Me.CulpritNameDataGridViewTextBoxColumn.DataPropertyName = "CulpritName"
        Me.CulpritNameDataGridViewTextBoxColumn.HeaderText = "Culprit Name"
        Me.CulpritNameDataGridViewTextBoxColumn.Name = "CulpritNameDataGridViewTextBoxColumn"
        Me.CulpritNameDataGridViewTextBoxColumn.ReadOnly = True
        Me.CulpritNameDataGridViewTextBoxColumn.Width = 150
        '
        'AddressDataGridViewTextBoxColumn
        '
        Me.AddressDataGridViewTextBoxColumn.DataPropertyName = "Address"
        Me.AddressDataGridViewTextBoxColumn.HeaderText = "Address"
        Me.AddressDataGridViewTextBoxColumn.Name = "AddressDataGridViewTextBoxColumn"
        Me.AddressDataGridViewTextBoxColumn.ReadOnly = True
        Me.AddressDataGridViewTextBoxColumn.Width = 150
        '
        'CPsIdentifiedDataGridViewTextBoxColumn
        '
        Me.CPsIdentifiedDataGridViewTextBoxColumn.DataPropertyName = "CPsIdentified"
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter
        Me.CPsIdentifiedDataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle2
        Me.CPsIdentifiedDataGridViewTextBoxColumn.HeaderText = "No. of CPs Identified"
        Me.CPsIdentifiedDataGridViewTextBoxColumn.Name = "CPsIdentifiedDataGridViewTextBoxColumn"
        Me.CPsIdentifiedDataGridViewTextBoxColumn.ReadOnly = True
        Me.CPsIdentifiedDataGridViewTextBoxColumn.Width = 70
        '
        'FingersIdentifiedDataGridViewTextBoxColumn
        '
        Me.FingersIdentifiedDataGridViewTextBoxColumn.DataPropertyName = "FingersIdentified"
        Me.FingersIdentifiedDataGridViewTextBoxColumn.HeaderText = "Fingers Identified"
        Me.FingersIdentifiedDataGridViewTextBoxColumn.Name = "FingersIdentifiedDataGridViewTextBoxColumn"
        Me.FingersIdentifiedDataGridViewTextBoxColumn.ReadOnly = True
        '
        'HenryClassificationDataGridViewTextBoxColumn
        '
        Me.HenryClassificationDataGridViewTextBoxColumn.DataPropertyName = "HenryClassification"
        Me.HenryClassificationDataGridViewTextBoxColumn.HeaderText = "Henry Classification"
        Me.HenryClassificationDataGridViewTextBoxColumn.Name = "HenryClassificationDataGridViewTextBoxColumn"
        Me.HenryClassificationDataGridViewTextBoxColumn.ReadOnly = True
        Me.HenryClassificationDataGridViewTextBoxColumn.Width = 120
        '
        'DANumberDataGridViewTextBoxColumn
        '
        Me.DANumberDataGridViewTextBoxColumn.DataPropertyName = "DANumber"
        Me.DANumberDataGridViewTextBoxColumn.HeaderText = "DA Number"
        Me.DANumberDataGridViewTextBoxColumn.Name = "DANumberDataGridViewTextBoxColumn"
        Me.DANumberDataGridViewTextBoxColumn.ReadOnly = True
        '
        'IdentifiedFromDataGridViewTextBoxColumn
        '
        Me.IdentifiedFromDataGridViewTextBoxColumn.DataPropertyName = "IdentifiedFrom"
        Me.IdentifiedFromDataGridViewTextBoxColumn.HeaderText = "Identified From"
        Me.IdentifiedFromDataGridViewTextBoxColumn.Name = "IdentifiedFromDataGridViewTextBoxColumn"
        Me.IdentifiedFromDataGridViewTextBoxColumn.ReadOnly = True
        Me.IdentifiedFromDataGridViewTextBoxColumn.Width = 80
        '
        'IdentificationDetailsDataGridViewTextBoxColumn
        '
        Me.IdentificationDetailsDataGridViewTextBoxColumn.DataPropertyName = "IdentificationDetails"
        Me.IdentificationDetailsDataGridViewTextBoxColumn.HeaderText = "Remarks"
        Me.IdentificationDetailsDataGridViewTextBoxColumn.Name = "IdentificationDetailsDataGridViewTextBoxColumn"
        Me.IdentificationDetailsDataGridViewTextBoxColumn.ReadOnly = True
        Me.IdentificationDetailsDataGridViewTextBoxColumn.Width = 170
        '
        'FrmIdentificationRegisterDE
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1041, 526)
        Me.Controls.Add(Me.PanelEx1)
        Me.DoubleBuffered = True
        Me.EnableGlass = False
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "FrmIdentificationRegisterDE"
        Me.Text = "Identification Details"
        Me.TitleText = "<b>Identification Details</b>"
        CType(Me.dtIdentificationDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCPsIdentified, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelEx1.ResumeLayout(False)
        Me.PanelEx1.PerformLayout()
        Me.GroupPanel1.ResumeLayout(False)
        Me.GroupPanel1.PerformLayout()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CulpritsRegisterBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FingerPrintDataSet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LabelX4 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX5 As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtCulpritName As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents txtRemarks As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents btnSave As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnCancel As DevComponents.DotNetBar.ButtonX
    Friend WithEvents txtIdentificationNumber As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents lblcrt3 As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblIdentificationDate As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblIdentifiedBy As DevComponents.DotNetBar.LabelX
    Friend WithEvents dtIdentificationDate As DevComponents.Editors.DateTimeAdv.DateTimeInput
    Friend WithEvents cmbIdentifyingOfficer As DevComponents.DotNetBar.Controls.ComboBoxEx
    Friend WithEvents lblCPsIdentified As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtCPsIdentified As DevComponents.Editors.IntegerInput
    Friend WithEvents LabelX6 As DevComponents.DotNetBar.LabelX
    Friend WithEvents PanelEx1 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents LabelX10 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX9 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX8 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX2 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX11 As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtAddress As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents txtDANumber As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents txtClassification As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents cmbIdentifiedFrom As DevComponents.DotNetBar.Controls.ComboBoxEx
    Friend WithEvents txtFingersIdentified As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX19 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX18 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX17 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX16 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX15 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX13 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX12 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX3 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX37 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX20 As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtSOCNumber As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents IdentificationRegisterTableAdapter1 As FingerprintInformationSystem.FingerPrintDataSetTableAdapters.IdentificationRegisterTableAdapter
    Friend WithEvents FingerPrintDataSet1 As FingerprintInformationSystem.FingerPrintDataSet
    Friend WithEvents SocRegisterTableAdapter1 As FingerprintInformationSystem.FingerPrintDataSetTableAdapters.SOCRegisterTableAdapter
    Friend WithEvents SocRegisterAutoTextTableAdapter1 As FingerprintInformationSystem.FingerPrintDataSetTableAdapters.SOCRegisterAutoTextTableAdapter
    Friend WithEvents lblCPCountWarning As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblSOCNumberWarning As DevComponents.DotNetBar.LabelX
    Friend WithEvents btnClearFields As DevComponents.DotNetBar.ButtonX
    Friend WithEvents GroupPanel1 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents CulpritsRegisterBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents CulpritsRegisterTableAdapter1 As FingerprintInformationSystem.FingerPrintDataSetTableAdapters.CulpritsRegisterTableAdapter
    Friend WithEvents LabelX21 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX14 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX7 As DevComponents.DotNetBar.LabelX
    Friend WithEvents btnEditCulprit As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnRemoveCulprit As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnAddCulprit As DevComponents.DotNetBar.ButtonX
    Friend WithEvents dgv As DevComponents.DotNetBar.Controls.DataGridViewX
    Friend WithEvents SlNumberDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IdentificationNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CulpritNameDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AddressDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CPsIdentifiedDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FingersIdentifiedDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents HenryClassificationDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DANumberDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IdentifiedFromDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IdentificationDetailsDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
