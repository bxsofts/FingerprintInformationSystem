<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmIdentificationRegister
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmIdentificationRegister))
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle15 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.LabelX4 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX5 = New DevComponents.DotNetBar.LabelX()
        Me.txtCulpritName = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.txtIdentificationDetails = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.btnSave = New DevComponents.DotNetBar.ButtonX()
        Me.btnCancel = New DevComponents.DotNetBar.ButtonX()
        Me.txtIdentificationNumber = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.lblcrt3 = New DevComponents.DotNetBar.LabelX()
        Me.lblIdentificationDate = New DevComponents.DotNetBar.LabelX()
        Me.lblIdentificationNumber = New DevComponents.DotNetBar.LabelX()
        Me.lblIdentifiedBy = New DevComponents.DotNetBar.LabelX()
        Me.dtIdentificationDate = New DevComponents.Editors.DateTimeAdv.DateTimeInput()
        Me.cmbIdentifyingOfficer = New DevComponents.DotNetBar.Controls.ComboBoxEx()
        Me.lblCPsIdentified = New DevComponents.DotNetBar.LabelX()
        Me.txtCPsIdentified = New DevComponents.Editors.IntegerInput()
        Me.txtSOCNumber = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX6 = New DevComponents.DotNetBar.LabelX()
        Me.PanelEx1 = New DevComponents.DotNetBar.PanelEx()
        Me.LabelX1 = New DevComponents.DotNetBar.LabelX()
        Me.cmbIdentifiedFrom = New DevComponents.DotNetBar.Controls.ComboBoxEx()
        Me.txtDANumber = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.txtClassification = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.btnSelectFingers = New DevComponents.DotNetBar.ButtonX()
        Me.ButtonX1 = New DevComponents.DotNetBar.ButtonX()
        Me.txtAddress = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.txtCulpritCount = New DevComponents.Editors.IntegerInput()
        Me.LabelX11 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX10 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX9 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX8 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX7 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX2 = New DevComponents.DotNetBar.LabelX()
        Me.DataGridViewX1 = New DevComponents.DotNetBar.Controls.DataGridViewX()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.dtIdentificationDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCPsIdentified, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelEx1.SuspendLayout()
        CType(Me.txtCulpritCount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridViewX1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelX4
        '
        Me.LabelX4.AutoSize = True
        '
        '
        '
        Me.LabelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX4.Location = New System.Drawing.Point(369, 140)
        Me.LabelX4.Name = "LabelX4"
        Me.LabelX4.Size = New System.Drawing.Size(120, 18)
        Me.LabelX4.TabIndex = 3
        Me.LabelX4.Text = "Identification Details"
        '
        'LabelX5
        '
        Me.LabelX5.AutoSize = True
        '
        '
        '
        Me.LabelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX5.Location = New System.Drawing.Point(369, 15)
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
        Me.txtCulpritName.Location = New System.Drawing.Point(495, 12)
        Me.txtCulpritName.MaxLength = 255
        Me.txtCulpritName.Name = "txtCulpritName"
        Me.txtCulpritName.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtCulpritName.Size = New System.Drawing.Size(546, 25)
        Me.txtCulpritName.TabIndex = 12
        Me.txtCulpritName.WatermarkBehavior = DevComponents.DotNetBar.eWatermarkBehavior.HideNonEmpty
        Me.txtCulpritName.WatermarkText = "Name of the identified criminal"
        '
        'txtIdentificationDetails
        '
        Me.txtIdentificationDetails.AcceptsReturn = True
        Me.txtIdentificationDetails.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtIdentificationDetails.Border.Class = "TextBoxBorder"
        Me.txtIdentificationDetails.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtIdentificationDetails.ButtonCustom.Image = CType(resources.GetObject("txtIdentificationDetails.ButtonCustom.Image"), System.Drawing.Image)
        Me.txtIdentificationDetails.DisabledBackColor = System.Drawing.Color.White
        Me.txtIdentificationDetails.FocusHighlightEnabled = True
        Me.txtIdentificationDetails.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIdentificationDetails.ForeColor = System.Drawing.Color.Black
        Me.txtIdentificationDetails.Location = New System.Drawing.Point(495, 137)
        Me.txtIdentificationDetails.MaxLength = 0
        Me.txtIdentificationDetails.Multiline = True
        Me.txtIdentificationDetails.Name = "txtIdentificationDetails"
        Me.txtIdentificationDetails.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtIdentificationDetails.Size = New System.Drawing.Size(546, 156)
        Me.txtIdentificationDetails.TabIndex = 14
        Me.txtIdentificationDetails.WatermarkBehavior = DevComponents.DotNetBar.eWatermarkBehavior.HideNonEmpty
        Me.txtIdentificationDetails.WatermarkText = "Identification Details"
        '
        'btnSave
        '
        Me.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnSave.Location = New System.Drawing.Point(728, 300)
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
        Me.btnCancel.Location = New System.Drawing.Point(936, 300)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(105, 34)
        Me.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnCancel.TabIndex = 17
        Me.btnCancel.Text = "Cancel"
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
        Me.txtIdentificationNumber.Location = New System.Drawing.Point(135, 12)
        Me.txtIdentificationNumber.MaxLength = 10
        Me.txtIdentificationNumber.Name = "txtIdentificationNumber"
        Me.txtIdentificationNumber.PreventEnterBeep = True
        Me.txtIdentificationNumber.Size = New System.Drawing.Size(130, 25)
        Me.txtIdentificationNumber.TabIndex = 1
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
        Me.lblIdentificationDate.Location = New System.Drawing.Point(11, 77)
        Me.lblIdentificationDate.Name = "lblIdentificationDate"
        Me.lblIdentificationDate.Size = New System.Drawing.Size(108, 18)
        Me.lblIdentificationDate.TabIndex = 168
        Me.lblIdentificationDate.Text = "Identification Date"
        '
        'lblIdentificationNumber
        '
        Me.lblIdentificationNumber.AutoSize = True
        '
        '
        '
        Me.lblIdentificationNumber.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblIdentificationNumber.Location = New System.Drawing.Point(11, 15)
        Me.lblIdentificationNumber.Name = "lblIdentificationNumber"
        Me.lblIdentificationNumber.Size = New System.Drawing.Size(102, 18)
        Me.lblIdentificationNumber.TabIndex = 167
        Me.lblIdentificationNumber.Text = "Identification No."
        '
        'lblIdentifiedBy
        '
        Me.lblIdentifiedBy.AutoSize = True
        '
        '
        '
        Me.lblIdentifiedBy.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblIdentifiedBy.Location = New System.Drawing.Point(11, 109)
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
        Me.dtIdentificationDate.ButtonClear.Enabled = False
        Me.dtIdentificationDate.ButtonClear.Image = CType(resources.GetObject("dtIdentificationDate.ButtonClear.Image"), System.Drawing.Image)
        Me.dtIdentificationDate.ButtonClear.Visible = True
        Me.dtIdentificationDate.ButtonDropDown.Visible = True
        Me.dtIdentificationDate.CustomFormat = "dd/MM/yyyy"
        Me.dtIdentificationDate.FocusHighlightEnabled = True
        Me.dtIdentificationDate.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtIdentificationDate.Format = DevComponents.Editors.eDateTimePickerFormat.Custom
        Me.dtIdentificationDate.IsPopupCalendarOpen = False
        Me.dtIdentificationDate.Location = New System.Drawing.Point(135, 74)
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
        Me.cmbIdentifyingOfficer.Location = New System.Drawing.Point(135, 105)
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
        Me.lblCPsIdentified.Location = New System.Drawing.Point(11, 140)
        Me.lblCPsIdentified.Name = "lblCPsIdentified"
        Me.lblCPsIdentified.Size = New System.Drawing.Size(120, 18)
        Me.lblCPsIdentified.TabIndex = 165
        Me.lblCPsIdentified.Text = "No. of CPs Identified"
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
        Me.txtCPsIdentified.Location = New System.Drawing.Point(135, 137)
        Me.txtCPsIdentified.MaxValue = 999
        Me.txtCPsIdentified.MinValue = 1
        Me.txtCPsIdentified.Name = "txtCPsIdentified"
        Me.txtCPsIdentified.ShowUpDown = True
        Me.txtCPsIdentified.Size = New System.Drawing.Size(130, 25)
        Me.txtCPsIdentified.TabIndex = 5
        Me.txtCPsIdentified.Value = 1
        Me.txtCPsIdentified.WatermarkText = "No. of CPs Identified"
        '
        'txtSOCNumber
        '
        Me.txtSOCNumber.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtSOCNumber.Border.Class = "TextBoxBorder"
        Me.txtSOCNumber.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtSOCNumber.ButtonCustom.Image = CType(resources.GetObject("txtSOCNumber.ButtonCustom.Image"), System.Drawing.Image)
        Me.txtSOCNumber.DisabledBackColor = System.Drawing.Color.White
        Me.txtSOCNumber.FocusHighlightEnabled = True
        Me.txtSOCNumber.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSOCNumber.ForeColor = System.Drawing.Color.Black
        Me.txtSOCNumber.Location = New System.Drawing.Point(135, 43)
        Me.txtSOCNumber.MaxLength = 10
        Me.txtSOCNumber.Name = "txtSOCNumber"
        Me.txtSOCNumber.Size = New System.Drawing.Size(130, 25)
        Me.txtSOCNumber.TabIndex = 2
        Me.txtSOCNumber.WatermarkBehavior = DevComponents.DotNetBar.eWatermarkBehavior.HideNonEmpty
        Me.txtSOCNumber.WatermarkText = "SOC Number"
        '
        'LabelX6
        '
        Me.LabelX6.AutoSize = True
        '
        '
        '
        Me.LabelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX6.Location = New System.Drawing.Point(11, 46)
        Me.LabelX6.Name = "LabelX6"
        Me.LabelX6.Size = New System.Drawing.Size(76, 18)
        Me.LabelX6.TabIndex = 174
        Me.LabelX6.Text = "SOC Number"
        '
        'PanelEx1
        '
        Me.PanelEx1.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.PanelEx1.Controls.Add(Me.DataGridViewX1)
        Me.PanelEx1.Controls.Add(Me.LabelX1)
        Me.PanelEx1.Controls.Add(Me.cmbIdentifiedFrom)
        Me.PanelEx1.Controls.Add(Me.txtDANumber)
        Me.PanelEx1.Controls.Add(Me.txtClassification)
        Me.PanelEx1.Controls.Add(Me.btnSelectFingers)
        Me.PanelEx1.Controls.Add(Me.ButtonX1)
        Me.PanelEx1.Controls.Add(Me.txtAddress)
        Me.PanelEx1.Controls.Add(Me.txtCulpritCount)
        Me.PanelEx1.Controls.Add(Me.LabelX11)
        Me.PanelEx1.Controls.Add(Me.LabelX10)
        Me.PanelEx1.Controls.Add(Me.LabelX9)
        Me.PanelEx1.Controls.Add(Me.LabelX8)
        Me.PanelEx1.Controls.Add(Me.LabelX7)
        Me.PanelEx1.Controls.Add(Me.LabelX2)
        Me.PanelEx1.Controls.Add(Me.txtSOCNumber)
        Me.PanelEx1.Controls.Add(Me.LabelX4)
        Me.PanelEx1.Controls.Add(Me.LabelX6)
        Me.PanelEx1.Controls.Add(Me.LabelX5)
        Me.PanelEx1.Controls.Add(Me.txtIdentificationDetails)
        Me.PanelEx1.Controls.Add(Me.txtIdentificationNumber)
        Me.PanelEx1.Controls.Add(Me.txtCulpritName)
        Me.PanelEx1.Controls.Add(Me.lblcrt3)
        Me.PanelEx1.Controls.Add(Me.btnSave)
        Me.PanelEx1.Controls.Add(Me.btnCancel)
        Me.PanelEx1.Controls.Add(Me.lblIdentificationDate)
        Me.PanelEx1.Controls.Add(Me.txtCPsIdentified)
        Me.PanelEx1.Controls.Add(Me.lblIdentificationNumber)
        Me.PanelEx1.Controls.Add(Me.lblCPsIdentified)
        Me.PanelEx1.Controls.Add(Me.lblIdentifiedBy)
        Me.PanelEx1.Controls.Add(Me.cmbIdentifyingOfficer)
        Me.PanelEx1.Controls.Add(Me.dtIdentificationDate)
        Me.PanelEx1.DisabledBackColor = System.Drawing.Color.Empty
        Me.PanelEx1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelEx1.Location = New System.Drawing.Point(0, 0)
        Me.PanelEx1.Name = "PanelEx1"
        Me.PanelEx1.Size = New System.Drawing.Size(1097, 530)
        Me.PanelEx1.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx1.Style.GradientAngle = 90
        Me.PanelEx1.TabIndex = 176
        '
        'LabelX1
        '
        Me.LabelX1.AutoSize = True
        '
        '
        '
        Me.LabelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX1.Location = New System.Drawing.Point(270, 205)
        Me.LabelX1.Name = "LabelX1"
        Me.LabelX1.Size = New System.Drawing.Size(102, 18)
        Me.LabelX1.TabIndex = 190
        Me.LabelX1.Text = "Fingers Identified"
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
        Me.cmbIdentifiedFrom.Location = New System.Drawing.Point(135, 297)
        Me.cmbIdentifiedFrom.MaxDropDownItems = 15
        Me.cmbIdentifiedFrom.MaxLength = 255
        Me.cmbIdentifiedFrom.Name = "cmbIdentifiedFrom"
        Me.cmbIdentifiedFrom.Size = New System.Drawing.Size(130, 26)
        Me.cmbIdentifiedFrom.Sorted = True
        Me.cmbIdentifiedFrom.TabIndex = 11
        Me.cmbIdentifiedFrom.WatermarkBehavior = DevComponents.DotNetBar.eWatermarkBehavior.HideNonEmpty
        Me.cmbIdentifiedFrom.WatermarkText = "Identified From"
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
        Me.txtDANumber.Location = New System.Drawing.Point(135, 266)
        Me.txtDANumber.MaxLength = 255
        Me.txtDANumber.Name = "txtDANumber"
        Me.txtDANumber.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtDANumber.Size = New System.Drawing.Size(208, 25)
        Me.txtDANumber.TabIndex = 10
        Me.txtDANumber.WatermarkBehavior = DevComponents.DotNetBar.eWatermarkBehavior.HideNonEmpty
        Me.txtDANumber.WatermarkText = "DA Number"
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
        Me.txtClassification.Location = New System.Drawing.Point(135, 235)
        Me.txtClassification.MaxLength = 255
        Me.txtClassification.Name = "txtClassification"
        Me.txtClassification.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtClassification.Size = New System.Drawing.Size(208, 25)
        Me.txtClassification.TabIndex = 9
        Me.txtClassification.WatermarkBehavior = DevComponents.DotNetBar.eWatermarkBehavior.HideNonEmpty
        Me.txtClassification.WatermarkText = "Henry Classification"
        '
        'btnSelectFingers
        '
        Me.btnSelectFingers.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnSelectFingers.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnSelectFingers.Location = New System.Drawing.Point(135, 199)
        Me.btnSelectFingers.Name = "btnSelectFingers"
        Me.btnSelectFingers.Size = New System.Drawing.Size(129, 30)
        Me.btnSelectFingers.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnSelectFingers.TabIndex = 8
        Me.btnSelectFingers.Text = "Select Fingers"
        '
        'ButtonX1
        '
        Me.ButtonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.ButtonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.ButtonX1.Location = New System.Drawing.Point(495, 300)
        Me.ButtonX1.Name = "ButtonX1"
        Me.ButtonX1.Size = New System.Drawing.Size(130, 34)
        Me.ButtonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.ButtonX1.TabIndex = 15
        Me.ButtonX1.Text = "Add Culprit Details"
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
        Me.txtAddress.Location = New System.Drawing.Point(495, 43)
        Me.txtAddress.MaxLength = 0
        Me.txtAddress.Multiline = True
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtAddress.Size = New System.Drawing.Size(546, 88)
        Me.txtAddress.TabIndex = 13
        Me.txtAddress.WatermarkBehavior = DevComponents.DotNetBar.eWatermarkBehavior.HideNonEmpty
        Me.txtAddress.WatermarkText = "Address of the identified criminal"
        '
        'txtCulpritCount
        '
        '
        '
        '
        Me.txtCulpritCount.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.txtCulpritCount.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtCulpritCount.FocusHighlightEnabled = True
        Me.txtCulpritCount.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCulpritCount.InputHorizontalAlignment = DevComponents.Editors.eHorizontalAlignment.Left
        Me.txtCulpritCount.Location = New System.Drawing.Point(135, 168)
        Me.txtCulpritCount.MaxValue = 999
        Me.txtCulpritCount.MinValue = 1
        Me.txtCulpritCount.Name = "txtCulpritCount"
        Me.txtCulpritCount.ShowUpDown = True
        Me.txtCulpritCount.Size = New System.Drawing.Size(130, 25)
        Me.txtCulpritCount.TabIndex = 6
        Me.txtCulpritCount.Value = 1
        Me.txtCulpritCount.WatermarkText = "No. of Culprits"
        '
        'LabelX11
        '
        Me.LabelX11.AutoSize = True
        '
        '
        '
        Me.LabelX11.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX11.Location = New System.Drawing.Point(11, 301)
        Me.LabelX11.Name = "LabelX11"
        Me.LabelX11.Size = New System.Drawing.Size(90, 18)
        Me.LabelX11.TabIndex = 182
        Me.LabelX11.Text = "Identified From"
        '
        'LabelX10
        '
        Me.LabelX10.AutoSize = True
        '
        '
        '
        Me.LabelX10.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX10.Location = New System.Drawing.Point(11, 205)
        Me.LabelX10.Name = "LabelX10"
        Me.LabelX10.Size = New System.Drawing.Size(102, 18)
        Me.LabelX10.TabIndex = 181
        Me.LabelX10.Text = "Fingers Identified"
        '
        'LabelX9
        '
        Me.LabelX9.AutoSize = True
        '
        '
        '
        Me.LabelX9.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX9.Location = New System.Drawing.Point(11, 238)
        Me.LabelX9.Name = "LabelX9"
        Me.LabelX9.Size = New System.Drawing.Size(77, 18)
        Me.LabelX9.TabIndex = 180
        Me.LabelX9.Text = "Classification"
        '
        'LabelX8
        '
        Me.LabelX8.AutoSize = True
        '
        '
        '
        Me.LabelX8.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX8.Location = New System.Drawing.Point(11, 269)
        Me.LabelX8.Name = "LabelX8"
        Me.LabelX8.Size = New System.Drawing.Size(70, 18)
        Me.LabelX8.TabIndex = 179
        Me.LabelX8.Text = "DA Number"
        '
        'LabelX7
        '
        Me.LabelX7.AutoSize = True
        '
        '
        '
        Me.LabelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX7.Location = New System.Drawing.Point(11, 171)
        Me.LabelX7.Name = "LabelX7"
        Me.LabelX7.Size = New System.Drawing.Size(85, 18)
        Me.LabelX7.TabIndex = 178
        Me.LabelX7.Text = "No. of Culprits"
        '
        'LabelX2
        '
        Me.LabelX2.AutoSize = True
        '
        '
        '
        Me.LabelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX2.Location = New System.Drawing.Point(369, 46)
        Me.LabelX2.Name = "LabelX2"
        Me.LabelX2.Size = New System.Drawing.Size(40, 18)
        Me.LabelX2.TabIndex = 176
        Me.LabelX2.Text = "Adress"
        '
        'DataGridViewX1
        '
        DataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter
        DataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle13.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewX1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle13
        Me.DataGridViewX1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewX1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2, Me.Column3, Me.Column4, Me.Column5, Me.Column6, Me.Column7})
        DataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle14.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle14.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle14.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridViewX1.DefaultCellStyle = DataGridViewCellStyle14
        Me.DataGridViewX1.GridColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.DataGridViewX1.Location = New System.Drawing.Point(11, 345)
        Me.DataGridViewX1.Name = "DataGridViewX1"
        DataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle15.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewX1.RowHeadersDefaultCellStyle = DataGridViewCellStyle15
        Me.DataGridViewX1.RowTemplate.Height = 50
        Me.DataGridViewX1.Size = New System.Drawing.Size(1030, 150)
        Me.DataGridViewX1.TabIndex = 191
        '
        'Column1
        '
        Me.Column1.HeaderText = "Name of Culprit"
        Me.Column1.Name = "Column1"
        Me.Column1.Width = 150
        '
        'Column2
        '
        Me.Column2.HeaderText = "Address"
        Me.Column2.Name = "Column2"
        Me.Column2.Width = 200
        '
        'Column3
        '
        Me.Column3.HeaderText = "Fingers Identified"
        Me.Column3.Name = "Column3"
        '
        'Column4
        '
        Me.Column4.HeaderText = "Classification"
        Me.Column4.Name = "Column4"
        '
        'Column5
        '
        Me.Column5.HeaderText = "DA Number"
        Me.Column5.Name = "Column5"
        Me.Column5.Width = 80
        '
        'Column6
        '
        Me.Column6.HeaderText = "Identified From"
        Me.Column6.Name = "Column6"
        Me.Column6.Width = 70
        '
        'Column7
        '
        Me.Column7.HeaderText = "Identification Details"
        Me.Column7.Name = "Column7"
        Me.Column7.Width = 300
        '
        'FrmIdentificationRegister
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1097, 530)
        Me.Controls.Add(Me.PanelEx1)
        Me.DoubleBuffered = True
        Me.EnableGlass = False
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "FrmIdentificationRegister"
        Me.ShowInTaskbar = False
        Me.Text = "Identification Details"
        Me.TitleText = "<b>Identification Details</b>"
        CType(Me.dtIdentificationDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCPsIdentified, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelEx1.ResumeLayout(False)
        Me.PanelEx1.PerformLayout()
        CType(Me.txtCulpritCount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridViewX1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LabelX4 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX5 As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtCulpritName As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents txtIdentificationDetails As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents btnSave As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnCancel As DevComponents.DotNetBar.ButtonX
    Friend WithEvents txtIdentificationNumber As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents lblcrt3 As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblIdentificationDate As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblIdentificationNumber As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblIdentifiedBy As DevComponents.DotNetBar.LabelX
    Friend WithEvents dtIdentificationDate As DevComponents.Editors.DateTimeAdv.DateTimeInput
    Friend WithEvents cmbIdentifyingOfficer As DevComponents.DotNetBar.Controls.ComboBoxEx
    Friend WithEvents lblCPsIdentified As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtCPsIdentified As DevComponents.Editors.IntegerInput
    Friend WithEvents txtSOCNumber As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX6 As DevComponents.DotNetBar.LabelX
    Friend WithEvents PanelEx1 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents LabelX10 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX9 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX8 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX7 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX2 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX11 As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtCulpritCount As DevComponents.Editors.IntegerInput
    Friend WithEvents txtAddress As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents ButtonX1 As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnSelectFingers As DevComponents.DotNetBar.ButtonX
    Friend WithEvents txtDANumber As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents txtClassification As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents cmbIdentifiedFrom As DevComponents.DotNetBar.Controls.ComboBoxEx
    Friend WithEvents DataGridViewX1 As DevComponents.DotNetBar.Controls.DataGridViewX
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column7 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
