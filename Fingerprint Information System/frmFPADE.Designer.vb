﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFPADE
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFPADE))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.LabelX162 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX161 = New DevComponents.DotNetBar.LabelX()
        Me.dtChalanDate = New DevComponents.Editors.DateTimeAdv.DateTimeInput()
        Me.txtHeadOfAccount = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX94 = New DevComponents.DotNetBar.LabelX()
        Me.txtAmount = New DevComponents.Editors.IntegerInput()
        Me.LabelX67 = New DevComponents.DotNetBar.LabelX()
        Me.txtTreasury = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX66 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX62 = New DevComponents.DotNetBar.LabelX()
        Me.btnAddChalan = New DevComponents.DotNetBar.ButtonX()
        Me.GroupPanel6 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.dgv = New DevComponents.DotNetBar.Controls.DataGridViewX()
        Me.chkFPATwodigits = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.txtFPANumberOnly = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.btnSaveFPA = New DevComponents.DotNetBar.ButtonX()
        Me.LabelX60 = New DevComponents.DotNetBar.LabelX()
        Me.txtRemarks = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX61 = New DevComponents.DotNetBar.LabelX()
        Me.txtAddress = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.txtPassportNumber = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX63 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX64 = New DevComponents.DotNetBar.LabelX()
        Me.txtName = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX65 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX54 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX55 = New DevComponents.DotNetBar.LabelX()
        Me.chkAppendFPAYear = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.txtFPAYear = New DevComponents.Editors.IntegerInput()
        Me.dtFPADate = New DevComponents.Editors.DateTimeAdv.DateTimeInput()
        Me.txtFPANumber = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX57 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX58 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX59 = New DevComponents.DotNetBar.LabelX()
        Me.btnEditChalan = New DevComponents.DotNetBar.ButtonX()
        Me.btnRemoveChalan = New DevComponents.DotNetBar.ButtonX()
        Me.FingerPrintDataSet1 = New FingerprintInformationSystem.FingerPrintDataSet()
        Me.FPARegisterAutoTextTableAdapter1 = New FingerprintInformationSystem.FingerPrintDataSetTableAdapters.FPRegisterAutoTextTableAdapter()
        Me.FPARegisterTableAdapter1 = New FingerprintInformationSystem.FingerPrintDataSetTableAdapters.FPAttestationRegisterTableAdapter()
        Me.LabelX1 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX2 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX3 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX4 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX5 = New DevComponents.DotNetBar.LabelX()
        Me.txtChalanNumber = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.ChalanTableBindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.ChalanTableTableAdapter1 = New FingerprintInformationSystem.FingerPrintDataSetTableAdapters.ChalanTableTableAdapter()
        Me.SerialNumberDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FPNumberDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FPDateDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ChalanNumberDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ChalanDateDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.HeadOfAccountDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TreasuryDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AmountRemittedDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.dtChalanDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupPanel6.SuspendLayout()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFPAYear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtFPADate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FingerPrintDataSet1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChalanTableBindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelX162
        '
        Me.LabelX162.AutoSize = True
        '
        '
        '
        Me.LabelX162.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX162.Location = New System.Drawing.Point(680, 73)
        Me.LabelX162.Name = "LabelX162"
        Me.LabelX162.Size = New System.Drawing.Size(32, 18)
        Me.LabelX162.TabIndex = 154
        Me.LabelX162.Text = "Head"
        '
        'LabelX161
        '
        Me.LabelX161.AutoSize = True
        '
        '
        '
        Me.LabelX161.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX161.Location = New System.Drawing.Point(680, 39)
        Me.LabelX161.Name = "LabelX161"
        Me.LabelX161.Size = New System.Drawing.Size(71, 18)
        Me.LabelX161.TabIndex = 153
        Me.LabelX161.Text = "Chalan Date"
        '
        'dtChalanDate
        '
        Me.dtChalanDate.AutoAdvance = True
        Me.dtChalanDate.AutoSelectDate = True
        Me.dtChalanDate.BackColor = System.Drawing.SystemColors.Window
        '
        '
        '
        Me.dtChalanDate.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.dtChalanDate.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.dtChalanDate.ButtonClear.Image = CType(resources.GetObject("dtChalanDate.ButtonClear.Image"), System.Drawing.Image)
        Me.dtChalanDate.ButtonClear.Visible = True
        Me.dtChalanDate.ButtonDropDown.Visible = True
        Me.dtChalanDate.CustomFormat = "dd/MM/yyyy"
        Me.dtChalanDate.FocusHighlightEnabled = True
        Me.dtChalanDate.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtChalanDate.Format = DevComponents.Editors.eDateTimePickerFormat.Custom
        Me.dtChalanDate.IsPopupCalendarOpen = False
        Me.dtChalanDate.Location = New System.Drawing.Point(756, 36)
        Me.dtChalanDate.MaxDate = New Date(2100, 12, 31, 0, 0, 0, 0)
        Me.dtChalanDate.MinDate = New Date(1900, 1, 1, 0, 0, 0, 0)
        '
        '
        '
        '
        '
        '
        Me.dtChalanDate.MonthCalendar.BackgroundStyle.BackColor = System.Drawing.SystemColors.Window
        Me.dtChalanDate.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.dtChalanDate.MonthCalendar.ClearButtonVisible = True
        '
        '
        '
        Me.dtChalanDate.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2
        Me.dtChalanDate.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90
        Me.dtChalanDate.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.dtChalanDate.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.dtChalanDate.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder
        Me.dtChalanDate.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1
        Me.dtChalanDate.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.dtChalanDate.MonthCalendar.DaySize = New System.Drawing.Size(30, 15)
        Me.dtChalanDate.MonthCalendar.DisplayMonth = New Date(2008, 7, 1, 0, 0, 0, 0)
        Me.dtChalanDate.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday
        '
        '
        '
        Me.dtChalanDate.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.dtChalanDate.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90
        Me.dtChalanDate.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.dtChalanDate.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.dtChalanDate.MonthCalendar.TodayButtonVisible = True
        Me.dtChalanDate.Name = "dtChalanDate"
        Me.dtChalanDate.Size = New System.Drawing.Size(227, 25)
        Me.dtChalanDate.TabIndex = 9
        Me.dtChalanDate.WatermarkText = "Chalan Date"
        '
        'txtHeadOfAccount
        '
        Me.txtHeadOfAccount.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.txtHeadOfAccount.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtHeadOfAccount.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtHeadOfAccount.Border.Class = "TextBoxBorder"
        Me.txtHeadOfAccount.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtHeadOfAccount.ButtonCustom.Image = CType(resources.GetObject("txtHeadOfAccount.ButtonCustom.Image"), System.Drawing.Image)
        Me.txtHeadOfAccount.ButtonCustom.Visible = True
        Me.txtHeadOfAccount.DisabledBackColor = System.Drawing.Color.White
        Me.txtHeadOfAccount.FocusHighlightEnabled = True
        Me.txtHeadOfAccount.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHeadOfAccount.ForeColor = System.Drawing.Color.Black
        Me.txtHeadOfAccount.Location = New System.Drawing.Point(756, 70)
        Me.txtHeadOfAccount.MaxLength = 255
        Me.txtHeadOfAccount.Name = "txtHeadOfAccount"
        Me.txtHeadOfAccount.Size = New System.Drawing.Size(227, 25)
        Me.txtHeadOfAccount.TabIndex = 10
        Me.txtHeadOfAccount.WatermarkBehavior = DevComponents.DotNetBar.eWatermarkBehavior.HideNonEmpty
        Me.txtHeadOfAccount.WatermarkText = "Head of Account"
        '
        'LabelX94
        '
        Me.LabelX94.AutoSize = True
        '
        '
        '
        Me.LabelX94.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX94.Font = New System.Drawing.Font("Rupee Foradian", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX94.Location = New System.Drawing.Point(732, 136)
        Me.LabelX94.Name = "LabelX94"
        Me.LabelX94.Size = New System.Drawing.Size(9, 16)
        Me.LabelX94.TabIndex = 152
        Me.LabelX94.Text = "`"
        '
        'txtAmount
        '
        '
        '
        '
        Me.txtAmount.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.txtAmount.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtAmount.FocusHighlightEnabled = True
        Me.txtAmount.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmount.InputHorizontalAlignment = DevComponents.Editors.eHorizontalAlignment.Left
        Me.txtAmount.Location = New System.Drawing.Point(756, 131)
        Me.txtAmount.MaxValue = 9999
        Me.txtAmount.MinValue = 0
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.ShowUpDown = True
        Me.txtAmount.Size = New System.Drawing.Size(91, 25)
        Me.txtAmount.TabIndex = 12
        Me.txtAmount.WatermarkText = "Amount"
        '
        'LabelX67
        '
        Me.LabelX67.AutoSize = True
        '
        '
        '
        Me.LabelX67.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX67.Location = New System.Drawing.Point(680, 134)
        Me.LabelX67.Name = "LabelX67"
        Me.LabelX67.Size = New System.Drawing.Size(48, 18)
        Me.LabelX67.TabIndex = 150
        Me.LabelX67.Text = "Amount"
        '
        'txtTreasury
        '
        Me.txtTreasury.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.txtTreasury.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtTreasury.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtTreasury.Border.Class = "TextBoxBorder"
        Me.txtTreasury.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtTreasury.ButtonCustom.Image = CType(resources.GetObject("txtFPATreasury.ButtonCustom.Image"), System.Drawing.Image)
        Me.txtTreasury.ButtonCustom.Visible = True
        Me.txtTreasury.DisabledBackColor = System.Drawing.Color.White
        Me.txtTreasury.FocusHighlightEnabled = True
        Me.txtTreasury.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTreasury.ForeColor = System.Drawing.Color.Black
        Me.txtTreasury.Location = New System.Drawing.Point(756, 100)
        Me.txtTreasury.MaxLength = 255
        Me.txtTreasury.Name = "txtTreasury"
        Me.txtTreasury.Size = New System.Drawing.Size(227, 25)
        Me.txtTreasury.TabIndex = 11
        Me.txtTreasury.WatermarkBehavior = DevComponents.DotNetBar.eWatermarkBehavior.HideNonEmpty
        Me.txtTreasury.WatermarkText = "Treasury"
        '
        'LabelX66
        '
        Me.LabelX66.AutoSize = True
        '
        '
        '
        Me.LabelX66.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX66.Location = New System.Drawing.Point(680, 103)
        Me.LabelX66.Name = "LabelX66"
        Me.LabelX66.Size = New System.Drawing.Size(51, 18)
        Me.LabelX66.TabIndex = 149
        Me.LabelX66.Text = "Treasury"
        '
        'LabelX62
        '
        Me.LabelX62.AutoSize = True
        '
        '
        '
        Me.LabelX62.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX62.Location = New System.Drawing.Point(680, 8)
        Me.LabelX62.Name = "LabelX62"
        Me.LabelX62.Size = New System.Drawing.Size(64, 18)
        Me.LabelX62.TabIndex = 148
        Me.LabelX62.Text = "Chalan No."
        '
        'btnAddChalan
        '
        Me.btnAddChalan.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnAddChalan.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnAddChalan.Location = New System.Drawing.Point(1011, 5)
        Me.btnAddChalan.Name = "btnAddChalan"
        Me.btnAddChalan.Size = New System.Drawing.Size(108, 34)
        Me.btnAddChalan.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnAddChalan.TabIndex = 13
        Me.btnAddChalan.Text = "Add Chalan"
        '
        'GroupPanel6
        '
        Me.GroupPanel6.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.GroupPanel6.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanel6.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanel6.Controls.Add(Me.dgv)
        Me.GroupPanel6.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel6.Location = New System.Drawing.Point(3, 212)
        Me.GroupPanel6.Name = "GroupPanel6"
        Me.GroupPanel6.Size = New System.Drawing.Size(1116, 199)
        '
        '
        '
        Me.GroupPanel6.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.GroupPanel6.Style.BackColorGradientAngle = 90
        Me.GroupPanel6.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.GroupPanel6.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel6.Style.BorderBottomWidth = 1
        Me.GroupPanel6.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.GroupPanel6.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel6.Style.BorderLeftWidth = 1
        Me.GroupPanel6.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel6.Style.BorderRightWidth = 1
        Me.GroupPanel6.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel6.Style.BorderTopWidth = 1
        Me.GroupPanel6.Style.CornerDiameter = 4
        Me.GroupPanel6.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.GroupPanel6.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.GroupPanel6.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.GroupPanel6.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.GroupPanel6.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.GroupPanel6.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.GroupPanel6.TabIndex = 226
        Me.GroupPanel6.Text = "Chalan Details"
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
        Me.dgv.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.SerialNumberDataGridViewTextBoxColumn, Me.FPNumberDataGridViewTextBoxColumn, Me.FPDateDataGridViewTextBoxColumn, Me.ChalanNumberDataGridViewTextBoxColumn, Me.ChalanDateDataGridViewTextBoxColumn, Me.HeadOfAccountDataGridViewTextBoxColumn, Me.TreasuryDataGridViewTextBoxColumn, Me.AmountRemittedDataGridViewTextBoxColumn})
        Me.dgv.DataSource = Me.ChalanTableBindingSource1
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv.DefaultCellStyle = DataGridViewCellStyle5
        Me.dgv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgv.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgv.EnableHeadersVisualStyles = False
        Me.dgv.GridColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.dgv.Location = New System.Drawing.Point(0, 0)
        Me.dgv.MultiSelect = False
        Me.dgv.Name = "dgv"
        Me.dgv.ReadOnly = True
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv.RowHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.dgv.RowHeadersWidth = 30
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv.RowsDefaultCellStyle = DataGridViewCellStyle7
        Me.dgv.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.dgv.RowTemplate.Height = 30
        Me.dgv.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgv.SelectAllSignVisible = False
        Me.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv.Size = New System.Drawing.Size(1110, 175)
        Me.dgv.TabIndex = 17
        Me.dgv.TabStop = False
        '
        'chkFPATwodigits
        '
        Me.chkFPATwodigits.AutoSize = True
        '
        '
        '
        Me.chkFPATwodigits.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.chkFPATwodigits.Location = New System.Drawing.Point(160, 22)
        Me.chkFPATwodigits.Name = "chkFPATwodigits"
        Me.chkFPATwodigits.Size = New System.Drawing.Size(126, 18)
        Me.chkFPATwodigits.TabIndex = 224
        Me.chkFPATwodigits.TabStop = False
        Me.chkFPATwodigits.Text = "Use last two digits"
        '
        'txtFPANumberOnly
        '
        Me.txtFPANumberOnly.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtFPANumberOnly.Border.Class = "TextBoxBorder"
        Me.txtFPANumberOnly.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtFPANumberOnly.ButtonCustom.Image = CType(resources.GetObject("txtFPANumberOnly.ButtonCustom.Image"), System.Drawing.Image)
        Me.txtFPANumberOnly.ButtonCustom.Visible = True
        Me.txtFPANumberOnly.DisabledBackColor = System.Drawing.Color.White
        Me.txtFPANumberOnly.FocusHighlightEnabled = True
        Me.txtFPANumberOnly.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFPANumberOnly.ForeColor = System.Drawing.Color.Black
        Me.txtFPANumberOnly.Location = New System.Drawing.Point(295, 8)
        Me.txtFPANumberOnly.MaxLength = 45
        Me.txtFPANumberOnly.Name = "txtFPANumberOnly"
        Me.txtFPANumberOnly.Size = New System.Drawing.Size(26, 23)
        Me.txtFPANumberOnly.TabIndex = 222
        Me.txtFPANumberOnly.TabStop = False
        Me.txtFPANumberOnly.Visible = False
        '
        'btnSaveFPA
        '
        Me.btnSaveFPA.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnSaveFPA.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnSaveFPA.Image = CType(resources.GetObject("btnSaveFPA.Image"), System.Drawing.Image)
        Me.btnSaveFPA.Location = New System.Drawing.Point(1011, 130)
        Me.btnSaveFPA.Name = "btnSaveFPA"
        Me.btnSaveFPA.Size = New System.Drawing.Size(108, 61)
        Me.btnSaveFPA.TabIndex = 16
        Me.btnSaveFPA.Text = "Save Record"
        '
        'LabelX60
        '
        Me.LabelX60.AutoSize = True
        '
        '
        '
        Me.LabelX60.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX60.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX60.Location = New System.Drawing.Point(314, 104)
        Me.LabelX60.Name = "LabelX60"
        Me.LabelX60.Size = New System.Drawing.Size(7, 22)
        Me.LabelX60.TabIndex = 221
        Me.LabelX60.Text = "<font color=""#ED1C24"">*</font><b></b>"
        '
        'txtRemarks
        '
        Me.txtRemarks.AcceptsReturn = True
        Me.txtRemarks.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.txtRemarks.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtRemarks.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtRemarks.Border.Class = "TextBoxBorder"
        Me.txtRemarks.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtRemarks.ButtonCustom.Image = CType(resources.GetObject("txtFPARemarks.ButtonCustom.Image"), System.Drawing.Image)
        Me.txtRemarks.DisabledBackColor = System.Drawing.Color.White
        Me.txtRemarks.FocusHighlightEnabled = True
        Me.txtRemarks.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.ForeColor = System.Drawing.Color.Black
        Me.txtRemarks.Location = New System.Drawing.Point(424, 36)
        Me.txtRemarks.MaxLength = 255
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtRemarks.Size = New System.Drawing.Size(227, 85)
        Me.txtRemarks.TabIndex = 7
        Me.txtRemarks.WatermarkBehavior = DevComponents.DotNetBar.eWatermarkBehavior.HideNonEmpty
        Me.txtRemarks.WatermarkText = "Remarks"
        '
        'LabelX61
        '
        Me.LabelX61.AutoSize = True
        '
        '
        '
        Me.LabelX61.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX61.Location = New System.Drawing.Point(345, 47)
        Me.LabelX61.Name = "LabelX61"
        Me.LabelX61.Size = New System.Drawing.Size(51, 18)
        Me.LabelX61.TabIndex = 220
        Me.LabelX61.Text = "Remarks"
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
        Me.txtAddress.ButtonCustom.Image = CType(resources.GetObject("txtFPAAddress.ButtonCustom.Image"), System.Drawing.Image)
        Me.txtAddress.DisabledBackColor = System.Drawing.Color.White
        Me.txtAddress.FocusHighlightEnabled = True
        Me.txtAddress.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress.ForeColor = System.Drawing.Color.Black
        Me.txtAddress.Location = New System.Drawing.Point(81, 131)
        Me.txtAddress.MaxLength = 255
        Me.txtAddress.Multiline = True
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtAddress.Size = New System.Drawing.Size(227, 75)
        Me.txtAddress.TabIndex = 5
        Me.txtAddress.WatermarkBehavior = DevComponents.DotNetBar.eWatermarkBehavior.HideNonEmpty
        Me.txtAddress.WatermarkText = "Address"
        '
        'txtPassportNumber
        '
        Me.txtPassportNumber.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtPassportNumber.Border.Class = "TextBoxBorder"
        Me.txtPassportNumber.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtPassportNumber.ButtonCustom.Image = CType(resources.GetObject("txtFPAPassportNumber.ButtonCustom.Image"), System.Drawing.Image)
        Me.txtPassportNumber.ButtonCustom.Visible = True
        Me.txtPassportNumber.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPassportNumber.DisabledBackColor = System.Drawing.Color.White
        Me.txtPassportNumber.FocusHighlightEnabled = True
        Me.txtPassportNumber.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPassportNumber.ForeColor = System.Drawing.Color.Black
        Me.txtPassportNumber.Location = New System.Drawing.Point(424, 5)
        Me.txtPassportNumber.MaxLength = 50
        Me.txtPassportNumber.Name = "txtPassportNumber"
        Me.txtPassportNumber.Size = New System.Drawing.Size(227, 25)
        Me.txtPassportNumber.TabIndex = 6
        Me.txtPassportNumber.WatermarkBehavior = DevComponents.DotNetBar.eWatermarkBehavior.HideNonEmpty
        Me.txtPassportNumber.WatermarkText = "Passport Number"
        '
        'LabelX63
        '
        Me.LabelX63.AutoSize = True
        '
        '
        '
        Me.LabelX63.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX63.Location = New System.Drawing.Point(345, 8)
        Me.LabelX63.Name = "LabelX63"
        Me.LabelX63.Size = New System.Drawing.Size(74, 18)
        Me.LabelX63.TabIndex = 219
        Me.LabelX63.Text = "Passport No."
        '
        'LabelX64
        '
        Me.LabelX64.AutoSize = True
        '
        '
        '
        Me.LabelX64.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX64.Location = New System.Drawing.Point(3, 134)
        Me.LabelX64.Name = "LabelX64"
        Me.LabelX64.Size = New System.Drawing.Size(48, 18)
        Me.LabelX64.TabIndex = 218
        Me.LabelX64.Text = "Address"
        '
        'txtName
        '
        Me.txtName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.txtName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtName.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtName.Border.Class = "TextBoxBorder"
        Me.txtName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtName.ButtonCustom.Image = CType(resources.GetObject("txtFPAName.ButtonCustom.Image"), System.Drawing.Image)
        Me.txtName.ButtonCustom.Visible = True
        Me.txtName.DisabledBackColor = System.Drawing.Color.White
        Me.txtName.FocusHighlightEnabled = True
        Me.txtName.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtName.ForeColor = System.Drawing.Color.Black
        Me.txtName.Location = New System.Drawing.Point(81, 100)
        Me.txtName.MaxLength = 255
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(227, 25)
        Me.txtName.TabIndex = 4
        Me.txtName.WatermarkBehavior = DevComponents.DotNetBar.eWatermarkBehavior.HideNonEmpty
        Me.txtName.WatermarkText = "Name"
        '
        'LabelX65
        '
        Me.LabelX65.AutoSize = True
        '
        '
        '
        Me.LabelX65.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX65.Location = New System.Drawing.Point(3, 103)
        Me.LabelX65.Name = "LabelX65"
        Me.LabelX65.Size = New System.Drawing.Size(36, 18)
        Me.LabelX65.TabIndex = 217
        Me.LabelX65.Text = "Name"
        '
        'LabelX54
        '
        Me.LabelX54.AutoSize = True
        '
        '
        '
        Me.LabelX54.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX54.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX54.Location = New System.Drawing.Point(314, 73)
        Me.LabelX54.Name = "LabelX54"
        Me.LabelX54.Size = New System.Drawing.Size(7, 22)
        Me.LabelX54.TabIndex = 212
        Me.LabelX54.Text = "<font color=""#ED1C24"">*</font><b></b>"
        '
        'LabelX55
        '
        Me.LabelX55.AutoSize = True
        '
        '
        '
        Me.LabelX55.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX55.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX55.Location = New System.Drawing.Point(314, 42)
        Me.LabelX55.Name = "LabelX55"
        Me.LabelX55.Size = New System.Drawing.Size(7, 22)
        Me.LabelX55.TabIndex = 211
        Me.LabelX55.Text = "<font color=""#ED1C24"">*</font><b></b>"
        '
        'chkAppendFPAYear
        '
        Me.chkAppendFPAYear.AutoSize = True
        '
        '
        '
        Me.chkAppendFPAYear.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.chkAppendFPAYear.Location = New System.Drawing.Point(160, 2)
        Me.chkAppendFPAYear.Name = "chkAppendFPAYear"
        Me.chkAppendFPAYear.Size = New System.Drawing.Size(124, 18)
        Me.chkAppendFPAYear.TabIndex = 210
        Me.chkAppendFPAYear.TabStop = False
        Me.chkAppendFPAYear.Text = "Auto append year"
        '
        'txtFPAYear
        '
        '
        '
        '
        Me.txtFPAYear.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.txtFPAYear.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtFPAYear.ButtonCustom.Image = CType(resources.GetObject("txtFPAYear.ButtonCustom.Image"), System.Drawing.Image)
        Me.txtFPAYear.FocusHighlightEnabled = True
        Me.txtFPAYear.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFPAYear.InputHorizontalAlignment = DevComponents.Editors.eHorizontalAlignment.Left
        Me.txtFPAYear.Location = New System.Drawing.Point(82, 5)
        Me.txtFPAYear.MaxValue = 2099
        Me.txtFPAYear.MinValue = 1900
        Me.txtFPAYear.Name = "txtFPAYear"
        Me.txtFPAYear.ShowUpDown = True
        Me.txtFPAYear.Size = New System.Drawing.Size(58, 25)
        Me.txtFPAYear.TabIndex = 1
        Me.txtFPAYear.Value = 1900
        Me.txtFPAYear.WatermarkText = "Year"
        '
        'dtFPADate
        '
        Me.dtFPADate.AutoAdvance = True
        Me.dtFPADate.AutoSelectDate = True
        Me.dtFPADate.BackColor = System.Drawing.SystemColors.Window
        '
        '
        '
        Me.dtFPADate.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.dtFPADate.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.dtFPADate.ButtonClear.Image = CType(resources.GetObject("dtFPADate.ButtonClear.Image"), System.Drawing.Image)
        Me.dtFPADate.ButtonClear.Visible = True
        Me.dtFPADate.ButtonDropDown.Visible = True
        Me.dtFPADate.CustomFormat = "dd/MM/yyyy"
        Me.dtFPADate.FocusHighlightEnabled = True
        Me.dtFPADate.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtFPADate.Format = DevComponents.Editors.eDateTimePickerFormat.Custom
        Me.dtFPADate.IsPopupCalendarOpen = False
        Me.dtFPADate.Location = New System.Drawing.Point(81, 70)
        Me.dtFPADate.MaxDate = New Date(2100, 12, 31, 0, 0, 0, 0)
        Me.dtFPADate.MinDate = New Date(1900, 1, 1, 0, 0, 0, 0)
        '
        '
        '
        '
        '
        '
        Me.dtFPADate.MonthCalendar.BackgroundStyle.BackColor = System.Drawing.SystemColors.Window
        Me.dtFPADate.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.dtFPADate.MonthCalendar.ClearButtonVisible = True
        '
        '
        '
        Me.dtFPADate.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2
        Me.dtFPADate.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90
        Me.dtFPADate.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.dtFPADate.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.dtFPADate.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder
        Me.dtFPADate.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1
        Me.dtFPADate.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.dtFPADate.MonthCalendar.DaySize = New System.Drawing.Size(30, 15)
        Me.dtFPADate.MonthCalendar.DisplayMonth = New Date(2008, 7, 1, 0, 0, 0, 0)
        Me.dtFPADate.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday
        '
        '
        '
        Me.dtFPADate.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.dtFPADate.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90
        Me.dtFPADate.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.dtFPADate.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.dtFPADate.MonthCalendar.TodayButtonVisible = True
        Me.dtFPADate.Name = "dtFPADate"
        Me.dtFPADate.Size = New System.Drawing.Size(227, 25)
        Me.dtFPADate.TabIndex = 3
        Me.dtFPADate.WatermarkText = "Date of Attestation"
        '
        'txtFPANumber
        '
        Me.txtFPANumber.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtFPANumber.Border.Class = "TextBoxBorder"
        Me.txtFPANumber.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtFPANumber.ButtonCustom.Image = CType(resources.GetObject("txtFPANumber.ButtonCustom.Image"), System.Drawing.Image)
        Me.txtFPANumber.ButtonCustom.Visible = True
        Me.txtFPANumber.DisabledBackColor = System.Drawing.Color.White
        Me.txtFPANumber.FocusHighlightEnabled = True
        Me.txtFPANumber.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFPANumber.ForeColor = System.Drawing.Color.Black
        Me.txtFPANumber.Location = New System.Drawing.Point(81, 40)
        Me.txtFPANumber.MaxLength = 10
        Me.txtFPANumber.Name = "txtFPANumber"
        Me.txtFPANumber.Size = New System.Drawing.Size(227, 25)
        Me.txtFPANumber.TabIndex = 2
        Me.txtFPANumber.WatermarkBehavior = DevComponents.DotNetBar.eWatermarkBehavior.HideNonEmpty
        Me.txtFPANumber.WatermarkText = "FPA Number"
        '
        'LabelX57
        '
        Me.LabelX57.AutoSize = True
        '
        '
        '
        Me.LabelX57.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX57.Location = New System.Drawing.Point(3, 73)
        Me.LabelX57.Name = "LabelX57"
        Me.LabelX57.Size = New System.Drawing.Size(29, 18)
        Me.LabelX57.TabIndex = 208
        Me.LabelX57.Text = "Date"
        '
        'LabelX58
        '
        Me.LabelX58.AutoSize = True
        '
        '
        '
        Me.LabelX58.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX58.Location = New System.Drawing.Point(3, 8)
        Me.LabelX58.Name = "LabelX58"
        Me.LabelX58.Size = New System.Drawing.Size(28, 18)
        Me.LabelX58.TabIndex = 206
        Me.LabelX58.Text = "Year"
        '
        'LabelX59
        '
        Me.LabelX59.AutoSize = True
        '
        '
        '
        Me.LabelX59.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX59.Location = New System.Drawing.Point(3, 43)
        Me.LabelX59.Name = "LabelX59"
        Me.LabelX59.Size = New System.Drawing.Size(74, 18)
        Me.LabelX59.TabIndex = 204
        Me.LabelX59.Text = "FPA Number"
        '
        'btnEditChalan
        '
        Me.btnEditChalan.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnEditChalan.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnEditChalan.Location = New System.Drawing.Point(1011, 45)
        Me.btnEditChalan.Name = "btnEditChalan"
        Me.btnEditChalan.Size = New System.Drawing.Size(108, 34)
        Me.btnEditChalan.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnEditChalan.TabIndex = 14
        Me.btnEditChalan.Text = "Edit Chalan"
        '
        'btnRemoveChalan
        '
        Me.btnRemoveChalan.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnRemoveChalan.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnRemoveChalan.Location = New System.Drawing.Point(1011, 85)
        Me.btnRemoveChalan.Name = "btnRemoveChalan"
        Me.btnRemoveChalan.Size = New System.Drawing.Size(108, 34)
        Me.btnRemoveChalan.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnRemoveChalan.TabIndex = 15
        Me.btnRemoveChalan.Text = "Remove Chalan"
        '
        'FingerPrintDataSet1
        '
        Me.FingerPrintDataSet1.DataSetName = "FingerPrintDataSet"
        Me.FingerPrintDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'FPARegisterAutoTextTableAdapter1
        '
        Me.FPARegisterAutoTextTableAdapter1.ClearBeforeFill = True
        '
        'FPARegisterTableAdapter1
        '
        Me.FPARegisterTableAdapter1.ClearBeforeFill = True
        '
        'LabelX1
        '
        Me.LabelX1.AutoSize = True
        '
        '
        '
        Me.LabelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX1.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX1.Location = New System.Drawing.Point(989, 73)
        Me.LabelX1.Name = "LabelX1"
        Me.LabelX1.Size = New System.Drawing.Size(7, 22)
        Me.LabelX1.TabIndex = 229
        Me.LabelX1.Text = "<font color=""#ED1C24"">*</font><b></b>"
        '
        'LabelX2
        '
        Me.LabelX2.AutoSize = True
        '
        '
        '
        Me.LabelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX2.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX2.Location = New System.Drawing.Point(989, 39)
        Me.LabelX2.Name = "LabelX2"
        Me.LabelX2.Size = New System.Drawing.Size(7, 22)
        Me.LabelX2.TabIndex = 228
        Me.LabelX2.Text = "<font color=""#ED1C24"">*</font><b></b>"
        '
        'LabelX3
        '
        Me.LabelX3.AutoSize = True
        '
        '
        '
        Me.LabelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX3.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX3.Location = New System.Drawing.Point(989, 8)
        Me.LabelX3.Name = "LabelX3"
        Me.LabelX3.Size = New System.Drawing.Size(7, 22)
        Me.LabelX3.TabIndex = 227
        Me.LabelX3.Text = "<font color=""#ED1C24"">*</font><b></b>"
        '
        'LabelX4
        '
        Me.LabelX4.AutoSize = True
        '
        '
        '
        Me.LabelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX4.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX4.Location = New System.Drawing.Point(989, 101)
        Me.LabelX4.Name = "LabelX4"
        Me.LabelX4.Size = New System.Drawing.Size(7, 22)
        Me.LabelX4.TabIndex = 230
        Me.LabelX4.Text = "<font color=""#ED1C24"">*</font><b></b>"
        '
        'LabelX5
        '
        Me.LabelX5.AutoSize = True
        '
        '
        '
        Me.LabelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX5.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX5.Location = New System.Drawing.Point(852, 135)
        Me.LabelX5.Name = "LabelX5"
        Me.LabelX5.Size = New System.Drawing.Size(7, 22)
        Me.LabelX5.TabIndex = 231
        Me.LabelX5.Text = "<font color=""#ED1C24"">*</font><b></b>"
        '
        'txtChalanNumber
        '
        Me.txtChalanNumber.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.txtChalanNumber.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtChalanNumber.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtChalanNumber.Border.Class = "TextBoxBorder"
        Me.txtChalanNumber.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtChalanNumber.ButtonCustom.Image = CType(resources.GetObject("txtFPAChalanNumber.ButtonCustom.Image"), System.Drawing.Image)
        Me.txtChalanNumber.ButtonCustom.Visible = True
        Me.txtChalanNumber.DisabledBackColor = System.Drawing.Color.White
        Me.txtChalanNumber.FocusHighlightEnabled = True
        Me.txtChalanNumber.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtChalanNumber.ForeColor = System.Drawing.Color.Black
        Me.txtChalanNumber.Location = New System.Drawing.Point(756, 5)
        Me.txtChalanNumber.MaxLength = 255
        Me.txtChalanNumber.Name = "txtChalanNumber"
        Me.txtChalanNumber.Size = New System.Drawing.Size(227, 25)
        Me.txtChalanNumber.TabIndex = 8
        Me.txtChalanNumber.WatermarkBehavior = DevComponents.DotNetBar.eWatermarkBehavior.HideNonEmpty
        Me.txtChalanNumber.WatermarkText = "Chalan Number"
        '
        'ChalanTableBindingSource1
        '
        Me.ChalanTableBindingSource1.DataMember = "ChalanTable"
        Me.ChalanTableBindingSource1.DataSource = Me.FingerPrintDataSet1
        '
        'ChalanTableTableAdapter1
        '
        Me.ChalanTableTableAdapter1.ClearBeforeFill = True
        '
        'SerialNumberDataGridViewTextBoxColumn
        '
        Me.SerialNumberDataGridViewTextBoxColumn.DataPropertyName = "SerialNumber"
        Me.SerialNumberDataGridViewTextBoxColumn.HeaderText = "SerialNumber"
        Me.SerialNumberDataGridViewTextBoxColumn.Name = "SerialNumberDataGridViewTextBoxColumn"
        Me.SerialNumberDataGridViewTextBoxColumn.ReadOnly = True
        Me.SerialNumberDataGridViewTextBoxColumn.Visible = False
        '
        'FPNumberDataGridViewTextBoxColumn
        '
        Me.FPNumberDataGridViewTextBoxColumn.DataPropertyName = "FPNumber"
        Me.FPNumberDataGridViewTextBoxColumn.HeaderText = "FPA Number"
        Me.FPNumberDataGridViewTextBoxColumn.Name = "FPNumberDataGridViewTextBoxColumn"
        Me.FPNumberDataGridViewTextBoxColumn.ReadOnly = True
        '
        'FPDateDataGridViewTextBoxColumn
        '
        Me.FPDateDataGridViewTextBoxColumn.DataPropertyName = "FPDate"
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.FPDateDataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle2
        Me.FPDateDataGridViewTextBoxColumn.HeaderText = "FPA Date"
        Me.FPDateDataGridViewTextBoxColumn.Name = "FPDateDataGridViewTextBoxColumn"
        Me.FPDateDataGridViewTextBoxColumn.ReadOnly = True
        '
        'ChalanNumberDataGridViewTextBoxColumn
        '
        Me.ChalanNumberDataGridViewTextBoxColumn.DataPropertyName = "ChalanNumber"
        Me.ChalanNumberDataGridViewTextBoxColumn.HeaderText = "Chalan Number"
        Me.ChalanNumberDataGridViewTextBoxColumn.Name = "ChalanNumberDataGridViewTextBoxColumn"
        Me.ChalanNumberDataGridViewTextBoxColumn.ReadOnly = True
        Me.ChalanNumberDataGridViewTextBoxColumn.Width = 150
        '
        'ChalanDateDataGridViewTextBoxColumn
        '
        Me.ChalanDateDataGridViewTextBoxColumn.DataPropertyName = "ChalanDate"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.Format = "dd/MM/yyyy"
        Me.ChalanDateDataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle3
        Me.ChalanDateDataGridViewTextBoxColumn.HeaderText = "Chalan Date"
        Me.ChalanDateDataGridViewTextBoxColumn.Name = "ChalanDateDataGridViewTextBoxColumn"
        Me.ChalanDateDataGridViewTextBoxColumn.ReadOnly = True
        '
        'HeadOfAccountDataGridViewTextBoxColumn
        '
        Me.HeadOfAccountDataGridViewTextBoxColumn.DataPropertyName = "HeadOfAccount"
        Me.HeadOfAccountDataGridViewTextBoxColumn.HeaderText = "Head of Account"
        Me.HeadOfAccountDataGridViewTextBoxColumn.Name = "HeadOfAccountDataGridViewTextBoxColumn"
        Me.HeadOfAccountDataGridViewTextBoxColumn.ReadOnly = True
        '
        'TreasuryDataGridViewTextBoxColumn
        '
        Me.TreasuryDataGridViewTextBoxColumn.DataPropertyName = "Treasury"
        Me.TreasuryDataGridViewTextBoxColumn.HeaderText = "Treasury"
        Me.TreasuryDataGridViewTextBoxColumn.Name = "TreasuryDataGridViewTextBoxColumn"
        Me.TreasuryDataGridViewTextBoxColumn.ReadOnly = True
        Me.TreasuryDataGridViewTextBoxColumn.Width = 150
        '
        'AmountRemittedDataGridViewTextBoxColumn
        '
        Me.AmountRemittedDataGridViewTextBoxColumn.DataPropertyName = "AmountRemitted"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.AmountRemittedDataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle4
        Me.AmountRemittedDataGridViewTextBoxColumn.HeaderText = "Amount Remitted"
        Me.AmountRemittedDataGridViewTextBoxColumn.Name = "AmountRemittedDataGridViewTextBoxColumn"
        Me.AmountRemittedDataGridViewTextBoxColumn.ReadOnly = True
        '
        'frmFPADE
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1125, 412)
        Me.Controls.Add(Me.txtChalanNumber)
        Me.Controls.Add(Me.LabelX5)
        Me.Controls.Add(Me.LabelX4)
        Me.Controls.Add(Me.LabelX1)
        Me.Controls.Add(Me.LabelX2)
        Me.Controls.Add(Me.LabelX3)
        Me.Controls.Add(Me.btnEditChalan)
        Me.Controls.Add(Me.GroupPanel6)
        Me.Controls.Add(Me.btnRemoveChalan)
        Me.Controls.Add(Me.chkFPATwodigits)
        Me.Controls.Add(Me.txtFPANumberOnly)
        Me.Controls.Add(Me.LabelX60)
        Me.Controls.Add(Me.txtRemarks)
        Me.Controls.Add(Me.btnSaveFPA)
        Me.Controls.Add(Me.LabelX61)
        Me.Controls.Add(Me.txtAddress)
        Me.Controls.Add(Me.txtPassportNumber)
        Me.Controls.Add(Me.txtTreasury)
        Me.Controls.Add(Me.LabelX63)
        Me.Controls.Add(Me.txtAmount)
        Me.Controls.Add(Me.LabelX64)
        Me.Controls.Add(Me.txtHeadOfAccount)
        Me.Controls.Add(Me.txtName)
        Me.Controls.Add(Me.dtChalanDate)
        Me.Controls.Add(Me.LabelX65)
        Me.Controls.Add(Me.btnAddChalan)
        Me.Controls.Add(Me.LabelX54)
        Me.Controls.Add(Me.LabelX62)
        Me.Controls.Add(Me.LabelX66)
        Me.Controls.Add(Me.LabelX55)
        Me.Controls.Add(Me.LabelX67)
        Me.Controls.Add(Me.chkAppendFPAYear)
        Me.Controls.Add(Me.LabelX94)
        Me.Controls.Add(Me.txtFPAYear)
        Me.Controls.Add(Me.LabelX161)
        Me.Controls.Add(Me.dtFPADate)
        Me.Controls.Add(Me.LabelX162)
        Me.Controls.Add(Me.txtFPANumber)
        Me.Controls.Add(Me.LabelX57)
        Me.Controls.Add(Me.LabelX58)
        Me.Controls.Add(Me.LabelX59)
        Me.DoubleBuffered = True
        Me.EnableGlass = False
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmFPADE"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "FP Attestation Details"
        Me.TitleText = "<b>FP Attestation Details</b>"
        CType(Me.dtChalanDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAmount, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupPanel6.ResumeLayout(False)
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFPAYear, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtFPADate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FingerPrintDataSet1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChalanTableBindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelX162 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX161 As DevComponents.DotNetBar.LabelX
    Friend WithEvents dtChalanDate As DevComponents.Editors.DateTimeAdv.DateTimeInput
    Friend WithEvents txtHeadOfAccount As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX94 As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtAmount As DevComponents.Editors.IntegerInput
    Friend WithEvents LabelX67 As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtTreasury As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX66 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX62 As DevComponents.DotNetBar.LabelX
    Friend WithEvents btnAddChalan As DevComponents.DotNetBar.ButtonX
    Friend WithEvents GroupPanel6 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents dgv As DevComponents.DotNetBar.Controls.DataGridViewX
    Friend WithEvents chkFPATwodigits As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents txtFPANumberOnly As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents btnSaveFPA As DevComponents.DotNetBar.ButtonX
    Friend WithEvents LabelX60 As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtRemarks As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX61 As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtAddress As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents txtPassportNumber As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX63 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX64 As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtName As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX65 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX54 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX55 As DevComponents.DotNetBar.LabelX
    Friend WithEvents chkAppendFPAYear As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents txtFPAYear As DevComponents.Editors.IntegerInput
    Friend WithEvents dtFPADate As DevComponents.Editors.DateTimeAdv.DateTimeInput
    Friend WithEvents txtFPANumber As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX57 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX58 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX59 As DevComponents.DotNetBar.LabelX
    Friend WithEvents btnEditChalan As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnRemoveChalan As DevComponents.DotNetBar.ButtonX
    Friend WithEvents FingerPrintDataSet1 As FingerprintInformationSystem.FingerPrintDataSet
    Friend WithEvents FPARegisterAutoTextTableAdapter1 As FingerprintInformationSystem.FingerPrintDataSetTableAdapters.FPRegisterAutoTextTableAdapter
    Friend WithEvents FPARegisterTableAdapter1 As FingerprintInformationSystem.FingerPrintDataSetTableAdapters.FPAttestationRegisterTableAdapter
    Friend WithEvents LabelX1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX2 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX3 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX4 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX5 As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtChalanNumber As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents ChalanTableBindingSource1 As System.Windows.Forms.BindingSource
    Friend WithEvents ChalanTableTableAdapter1 As FingerprintInformationSystem.FingerPrintDataSetTableAdapters.ChalanTableTableAdapter
    Friend WithEvents SerialNumberDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FPNumberDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FPDateDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ChalanNumberDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ChalanDateDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents HeadOfAccountDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TreasuryDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AmountRemittedDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
