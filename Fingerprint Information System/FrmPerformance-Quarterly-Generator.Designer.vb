<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmQuarterlyPerformance
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmQuarterlyPerformance))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.PerformanceBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.FingerPrintDataSet = New FingerprintInformationSystem.FingerPrintDataSet()
        Me.btnInsertBlankValues = New DevComponents.DotNetBar.ButtonX()
        Me.btnGenerateSelectedQuarterFromDB = New DevComponents.DotNetBar.ButtonX()
        Me.LabelX5 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX6 = New DevComponents.DotNetBar.LabelX()
        Me.txtQuarterYear = New DevComponents.Editors.IntegerInput()
        Me.txtBlankCellValue = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.txtQuarter = New DevComponents.Editors.IntegerInput()
        Me.PerformanceTableAdapter = New FingerprintInformationSystem.FingerPrintDataSetTableAdapters.PerformanceTableAdapter()
        Me.FpARegisterTableAdapter = New FingerprintInformationSystem.FingerPrintDataSetTableAdapters.FPAttestationRegisterTableAdapter()
        Me.SOCRegisterTableAdapter = New FingerprintInformationSystem.FingerPrintDataSetTableAdapters.SOCRegisterTableAdapter()
        Me.DaRegisterTableAdapter = New FingerprintInformationSystem.FingerPrintDataSetTableAdapters.DARegisterTableAdapter()
        Me.CdRegisterTableAdapter = New FingerprintInformationSystem.FingerPrintDataSetTableAdapters.CDRegisterTableAdapter()
        Me.btnGeneratePreviousQuarterValues = New DevComponents.DotNetBar.ButtonX()
        Me.PanelEx2 = New DevComponents.DotNetBar.PanelEx()
        Me.GroupPanel4 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.CheckBoxX2 = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.chkBlankValue = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.GroupPanel3 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.chkGeneratePreviousQuarterValuesFromFile = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.chkGeneratePreviousQuarterValuesfromDataBase = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.GroupPanel2 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.chkGenerateSelectedQuarterValuesFromESelectedQuarterFile = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.chkGenerateSelectedQuarterValuesFromSavedMonthlyStatements = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.chkGenerateSelectedQuaterValuesFromDataBase = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.GroupPanel1 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.PanelEx1 = New DevComponents.DotNetBar.PanelEx()
        Me.lblPeriod = New DevComponents.DotNetBar.LabelX()
        Me.PanelEx3 = New DevComponents.DotNetBar.PanelEx()
        Me.CircularProgress1 = New DevComponents.DotNetBar.Controls.CircularProgress()
        Me.lblPreviousQuarter = New DevComponents.DotNetBar.LabelX()
        Me.btnClearAllFields = New DevComponents.DotNetBar.ButtonX()
        Me.lblSelectedQuarter = New DevComponents.DotNetBar.LabelX()
        Me.btnStatement = New DevComponents.DotNetBar.ButtonX()
        Me.DataGridViewX1 = New DevComponents.DotNetBar.Controls.DataGridViewX()
        Me.bgwStatement = New System.ComponentModel.BackgroundWorker()
        Me.btnOpenFolder = New DevComponents.DotNetBar.ButtonX()
        Me.SlNoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DetailsOfWorkDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PreviousDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Month1DataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Month2DataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Month3DataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PresentDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RemarksDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.PerformanceBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FingerPrintDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtQuarterYear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtQuarter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelEx2.SuspendLayout()
        Me.GroupPanel4.SuspendLayout()
        Me.GroupPanel3.SuspendLayout()
        Me.GroupPanel2.SuspendLayout()
        Me.GroupPanel1.SuspendLayout()
        Me.PanelEx1.SuspendLayout()
        Me.PanelEx3.SuspendLayout()
        CType(Me.DataGridViewX1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        'btnInsertBlankValues
        '
        Me.btnInsertBlankValues.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnInsertBlankValues.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnInsertBlankValues.Location = New System.Drawing.Point(267, 6)
        Me.btnInsertBlankValues.Name = "btnInsertBlankValues"
        Me.btnInsertBlankValues.Size = New System.Drawing.Size(99, 58)
        Me.btnInsertBlankValues.TabIndex = 5
        Me.btnInsertBlankValues.Text = "INSERT"
        '
        'btnGenerateSelectedQuarterFromDB
        '
        Me.btnGenerateSelectedQuarterFromDB.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnGenerateSelectedQuarterFromDB.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnGenerateSelectedQuarterFromDB.Location = New System.Drawing.Point(267, 17)
        Me.btnGenerateSelectedQuarterFromDB.Name = "btnGenerateSelectedQuarterFromDB"
        Me.btnGenerateSelectedQuarterFromDB.Size = New System.Drawing.Size(99, 58)
        Me.btnGenerateSelectedQuarterFromDB.TabIndex = 3
        Me.btnGenerateSelectedQuarterFromDB.Text = "GENERATE"
        '
        'LabelX5
        '
        Me.LabelX5.AutoSize = True
        '
        '
        '
        Me.LabelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX5.Location = New System.Drawing.Point(193, 19)
        Me.LabelX5.Name = "LabelX5"
        Me.LabelX5.Size = New System.Drawing.Size(28, 18)
        Me.LabelX5.TabIndex = 38
        Me.LabelX5.Text = "Year"
        '
        'LabelX6
        '
        Me.LabelX6.AutoSize = True
        '
        '
        '
        Me.LabelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX6.Location = New System.Drawing.Point(11, 19)
        Me.LabelX6.Name = "LabelX6"
        Me.LabelX6.Size = New System.Drawing.Size(46, 18)
        Me.LabelX6.TabIndex = 37
        Me.LabelX6.Text = "Quarter"
        '
        'txtQuarterYear
        '
        '
        '
        '
        Me.txtQuarterYear.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.txtQuarterYear.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtQuarterYear.ButtonCustom.Image = CType(resources.GetObject("txtQuarterYear.ButtonCustom.Image"), System.Drawing.Image)
        Me.txtQuarterYear.FocusHighlightEnabled = True
        Me.txtQuarterYear.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtQuarterYear.InputHorizontalAlignment = DevComponents.Editors.eHorizontalAlignment.Left
        Me.txtQuarterYear.Location = New System.Drawing.Point(227, 14)
        Me.txtQuarterYear.MaxValue = 2099
        Me.txtQuarterYear.MinValue = 1900
        Me.txtQuarterYear.Name = "txtQuarterYear"
        Me.txtQuarterYear.ShowUpDown = True
        Me.txtQuarterYear.Size = New System.Drawing.Size(75, 29)
        Me.txtQuarterYear.TabIndex = 2
        Me.txtQuarterYear.Value = 1900
        Me.txtQuarterYear.WatermarkText = "Year"
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
        Me.txtBlankCellValue.Location = New System.Drawing.Point(130, 7)
        Me.txtBlankCellValue.Name = "txtBlankCellValue"
        Me.txtBlankCellValue.Size = New System.Drawing.Size(82, 27)
        Me.txtBlankCellValue.TabIndex = 5
        '
        'txtQuarter
        '
        '
        '
        '
        Me.txtQuarter.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.txtQuarter.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtQuarter.ButtonCustom.Image = CType(resources.GetObject("txtQuarter.ButtonCustom.Image"), System.Drawing.Image)
        Me.txtQuarter.FocusHighlightEnabled = True
        Me.txtQuarter.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtQuarter.InputHorizontalAlignment = DevComponents.Editors.eHorizontalAlignment.Left
        Me.txtQuarter.Location = New System.Drawing.Point(68, 14)
        Me.txtQuarter.MaxValue = 4
        Me.txtQuarter.MinValue = 1
        Me.txtQuarter.Name = "txtQuarter"
        Me.txtQuarter.ShowUpDown = True
        Me.txtQuarter.Size = New System.Drawing.Size(75, 29)
        Me.txtQuarter.TabIndex = 1
        Me.txtQuarter.Value = 4
        Me.txtQuarter.WatermarkText = "Quarter"
        '
        'PerformanceTableAdapter
        '
        Me.PerformanceTableAdapter.ClearBeforeFill = True
        '
        'FpARegisterTableAdapter
        '
        Me.FpARegisterTableAdapter.ClearBeforeFill = True
        '
        'SOCRegisterTableAdapter
        '
        Me.SOCRegisterTableAdapter.ClearBeforeFill = True
        '
        'DaRegisterTableAdapter
        '
        Me.DaRegisterTableAdapter.ClearBeforeFill = True
        '
        'CdRegisterTableAdapter
        '
        Me.CdRegisterTableAdapter.ClearBeforeFill = True
        '
        'btnGeneratePreviousQuarterValues
        '
        Me.btnGeneratePreviousQuarterValues.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnGeneratePreviousQuarterValues.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnGeneratePreviousQuarterValues.Location = New System.Drawing.Point(267, 5)
        Me.btnGeneratePreviousQuarterValues.Name = "btnGeneratePreviousQuarterValues"
        Me.btnGeneratePreviousQuarterValues.Size = New System.Drawing.Size(99, 58)
        Me.btnGeneratePreviousQuarterValues.TabIndex = 4
        Me.btnGeneratePreviousQuarterValues.Text = "GENERATE"
        '
        'PanelEx2
        '
        Me.PanelEx2.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.PanelEx2.Controls.Add(Me.GroupPanel4)
        Me.PanelEx2.Controls.Add(Me.GroupPanel3)
        Me.PanelEx2.Controls.Add(Me.GroupPanel2)
        Me.PanelEx2.Controls.Add(Me.GroupPanel1)
        Me.PanelEx2.DisabledBackColor = System.Drawing.Color.Empty
        Me.PanelEx2.Dock = System.Windows.Forms.DockStyle.Left
        Me.PanelEx2.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PanelEx2.Location = New System.Drawing.Point(0, 0)
        Me.PanelEx2.Name = "PanelEx2"
        Me.PanelEx2.Size = New System.Drawing.Size(395, 733)
        Me.PanelEx2.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx2.Style.GradientAngle = 90
        Me.PanelEx2.TabIndex = 4
        '
        'GroupPanel4
        '
        Me.GroupPanel4.BackColor = System.Drawing.Color.Transparent
        Me.GroupPanel4.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanel4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanel4.Controls.Add(Me.txtBlankCellValue)
        Me.GroupPanel4.Controls.Add(Me.CheckBoxX2)
        Me.GroupPanel4.Controls.Add(Me.chkBlankValue)
        Me.GroupPanel4.Controls.Add(Me.btnInsertBlankValues)
        Me.GroupPanel4.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel4.Location = New System.Drawing.Point(7, 483)
        Me.GroupPanel4.Name = "GroupPanel4"
        Me.GroupPanel4.Size = New System.Drawing.Size(378, 96)
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
        Me.GroupPanel4.TabIndex = 35
        Me.GroupPanel4.Text = "STEP 4 : Blank Cell Value"
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
        Me.GroupPanel3.Controls.Add(Me.btnGeneratePreviousQuarterValues)
        Me.GroupPanel3.Controls.Add(Me.chkGeneratePreviousQuarterValuesFromFile)
        Me.GroupPanel3.Controls.Add(Me.chkGeneratePreviousQuarterValuesfromDataBase)
        Me.GroupPanel3.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel3.Location = New System.Drawing.Point(7, 331)
        Me.GroupPanel3.Name = "GroupPanel3"
        Me.GroupPanel3.Size = New System.Drawing.Size(378, 96)
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
        Me.GroupPanel3.TabIndex = 34
        Me.GroupPanel3.Text = "STEP 3 : Generate values for Previous Quarter from"
        '
        'chkGeneratePreviousQuarterValuesFromFile
        '
        Me.chkGeneratePreviousQuarterValuesFromFile.AutoSize = True
        '
        '
        '
        Me.chkGeneratePreviousQuarterValuesFromFile.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.chkGeneratePreviousQuarterValuesFromFile.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton
        Me.chkGeneratePreviousQuarterValuesFromFile.Location = New System.Drawing.Point(3, 40)
        Me.chkGeneratePreviousQuarterValuesFromFile.Name = "chkGeneratePreviousQuarterValuesFromFile"
        Me.chkGeneratePreviousQuarterValuesFromFile.Size = New System.Drawing.Size(250, 18)
        Me.chkGeneratePreviousQuarterValuesFromFile.TabIndex = 3
        Me.chkGeneratePreviousQuarterValuesFromFile.TabStop = False
        Me.chkGeneratePreviousQuarterValuesFromFile.Text = "Saved statement of the previous quarter"
        '
        'chkGeneratePreviousQuarterValuesfromDataBase
        '
        Me.chkGeneratePreviousQuarterValuesfromDataBase.AutoSize = True
        '
        '
        '
        Me.chkGeneratePreviousQuarterValuesfromDataBase.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.chkGeneratePreviousQuarterValuesfromDataBase.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton
        Me.chkGeneratePreviousQuarterValuesfromDataBase.Checked = True
        Me.chkGeneratePreviousQuarterValuesfromDataBase.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkGeneratePreviousQuarterValuesfromDataBase.CheckValue = "Y"
        Me.chkGeneratePreviousQuarterValuesfromDataBase.Location = New System.Drawing.Point(3, 13)
        Me.chkGeneratePreviousQuarterValuesfromDataBase.Name = "chkGeneratePreviousQuarterValuesfromDataBase"
        Me.chkGeneratePreviousQuarterValuesfromDataBase.Size = New System.Drawing.Size(75, 18)
        Me.chkGeneratePreviousQuarterValuesfromDataBase.TabIndex = 2
        Me.chkGeneratePreviousQuarterValuesfromDataBase.TabStop = False
        Me.chkGeneratePreviousQuarterValuesfromDataBase.Text = "Database"
        '
        'GroupPanel2
        '
        Me.GroupPanel2.BackColor = System.Drawing.Color.Transparent
        Me.GroupPanel2.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanel2.Controls.Add(Me.chkGenerateSelectedQuarterValuesFromESelectedQuarterFile)
        Me.GroupPanel2.Controls.Add(Me.chkGenerateSelectedQuarterValuesFromSavedMonthlyStatements)
        Me.GroupPanel2.Controls.Add(Me.chkGenerateSelectedQuaterValuesFromDataBase)
        Me.GroupPanel2.Controls.Add(Me.btnGenerateSelectedQuarterFromDB)
        Me.GroupPanel2.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel2.Location = New System.Drawing.Point(7, 152)
        Me.GroupPanel2.Name = "GroupPanel2"
        Me.GroupPanel2.Size = New System.Drawing.Size(378, 118)
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
        Me.GroupPanel2.TabIndex = 33
        Me.GroupPanel2.Text = "STEP 2 : Generate values for Selected Quarter from"
        '
        'chkGenerateSelectedQuarterValuesFromESelectedQuarterFile
        '
        Me.chkGenerateSelectedQuarterValuesFromESelectedQuarterFile.AutoSize = True
        '
        '
        '
        Me.chkGenerateSelectedQuarterValuesFromESelectedQuarterFile.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.chkGenerateSelectedQuarterValuesFromESelectedQuarterFile.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton
        Me.chkGenerateSelectedQuarterValuesFromESelectedQuarterFile.Location = New System.Drawing.Point(3, 63)
        Me.chkGenerateSelectedQuarterValuesFromESelectedQuarterFile.Name = "chkGenerateSelectedQuarterValuesFromESelectedQuarterFile"
        Me.chkGenerateSelectedQuarterValuesFromESelectedQuarterFile.Size = New System.Drawing.Size(255, 18)
        Me.chkGenerateSelectedQuarterValuesFromESelectedQuarterFile.TabIndex = 4
        Me.chkGenerateSelectedQuarterValuesFromESelectedQuarterFile.TabStop = False
        Me.chkGenerateSelectedQuarterValuesFromESelectedQuarterFile.Text = "Load saved values of the selected quarter"
        '
        'chkGenerateSelectedQuarterValuesFromSavedMonthlyStatements
        '
        Me.chkGenerateSelectedQuarterValuesFromSavedMonthlyStatements.AutoSize = True
        '
        '
        '
        Me.chkGenerateSelectedQuarterValuesFromSavedMonthlyStatements.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.chkGenerateSelectedQuarterValuesFromSavedMonthlyStatements.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton
        Me.chkGenerateSelectedQuarterValuesFromSavedMonthlyStatements.Location = New System.Drawing.Point(3, 39)
        Me.chkGenerateSelectedQuarterValuesFromSavedMonthlyStatements.Name = "chkGenerateSelectedQuarterValuesFromSavedMonthlyStatements"
        Me.chkGenerateSelectedQuarterValuesFromSavedMonthlyStatements.Size = New System.Drawing.Size(211, 18)
        Me.chkGenerateSelectedQuarterValuesFromSavedMonthlyStatements.TabIndex = 1
        Me.chkGenerateSelectedQuarterValuesFromSavedMonthlyStatements.TabStop = False
        Me.chkGenerateSelectedQuarterValuesFromSavedMonthlyStatements.Text = "Saved values of the three months"
        '
        'chkGenerateSelectedQuaterValuesFromDataBase
        '
        Me.chkGenerateSelectedQuaterValuesFromDataBase.AutoSize = True
        '
        '
        '
        Me.chkGenerateSelectedQuaterValuesFromDataBase.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.chkGenerateSelectedQuaterValuesFromDataBase.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton
        Me.chkGenerateSelectedQuaterValuesFromDataBase.Checked = True
        Me.chkGenerateSelectedQuaterValuesFromDataBase.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkGenerateSelectedQuaterValuesFromDataBase.CheckValue = "Y"
        Me.chkGenerateSelectedQuaterValuesFromDataBase.Location = New System.Drawing.Point(3, 12)
        Me.chkGenerateSelectedQuaterValuesFromDataBase.Name = "chkGenerateSelectedQuaterValuesFromDataBase"
        Me.chkGenerateSelectedQuaterValuesFromDataBase.Size = New System.Drawing.Size(75, 18)
        Me.chkGenerateSelectedQuaterValuesFromDataBase.TabIndex = 0
        Me.chkGenerateSelectedQuaterValuesFromDataBase.TabStop = False
        Me.chkGenerateSelectedQuaterValuesFromDataBase.Text = "Database"
        '
        'GroupPanel1
        '
        Me.GroupPanel1.BackColor = System.Drawing.Color.Transparent
        Me.GroupPanel1.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanel1.Controls.Add(Me.LabelX6)
        Me.GroupPanel1.Controls.Add(Me.txtQuarter)
        Me.GroupPanel1.Controls.Add(Me.txtQuarterYear)
        Me.GroupPanel1.Controls.Add(Me.LabelX5)
        Me.GroupPanel1.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel1.Location = New System.Drawing.Point(7, 12)
        Me.GroupPanel1.Name = "GroupPanel1"
        Me.GroupPanel1.Size = New System.Drawing.Size(378, 76)
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
        Me.GroupPanel1.Text = "STEP 1 : Select Quarter and Year"
        '
        'PanelEx1
        '
        Me.PanelEx1.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.PanelEx1.Controls.Add(Me.lblPeriod)
        Me.PanelEx1.DisabledBackColor = System.Drawing.Color.Empty
        Me.PanelEx1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelEx1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PanelEx1.Location = New System.Drawing.Point(395, 0)
        Me.PanelEx1.Name = "PanelEx1"
        Me.PanelEx1.Size = New System.Drawing.Size(959, 45)
        Me.PanelEx1.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx1.Style.GradientAngle = 90
        Me.PanelEx1.TabIndex = 5
        '
        'lblPeriod
        '
        '
        '
        '
        Me.lblPeriod.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblPeriod.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblPeriod.Font = New System.Drawing.Font("Segoe UI", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPeriod.Location = New System.Drawing.Point(0, 0)
        Me.lblPeriod.Name = "lblPeriod"
        Me.lblPeriod.Size = New System.Drawing.Size(959, 45)
        Me.lblPeriod.TabIndex = 34
        Me.lblPeriod.Text = "STATEMENT OF PERFORMANCE"
        Me.lblPeriod.TextAlignment = System.Drawing.StringAlignment.Center
        '
        'PanelEx3
        '
        Me.PanelEx3.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.PanelEx3.Controls.Add(Me.CircularProgress1)
        Me.PanelEx3.Controls.Add(Me.btnOpenFolder)
        Me.PanelEx3.Controls.Add(Me.lblPreviousQuarter)
        Me.PanelEx3.Controls.Add(Me.btnClearAllFields)
        Me.PanelEx3.Controls.Add(Me.lblSelectedQuarter)
        Me.PanelEx3.Controls.Add(Me.btnStatement)
        Me.PanelEx3.DisabledBackColor = System.Drawing.Color.Empty
        Me.PanelEx3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelEx3.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PanelEx3.Location = New System.Drawing.Point(395, 637)
        Me.PanelEx3.Name = "PanelEx3"
        Me.PanelEx3.Size = New System.Drawing.Size(959, 96)
        Me.PanelEx3.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx3.Style.GradientAngle = 90
        Me.PanelEx3.TabIndex = 11
        '
        'CircularProgress1
        '
        '
        '
        '
        Me.CircularProgress1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.CircularProgress1.FocusCuesEnabled = False
        Me.CircularProgress1.Location = New System.Drawing.Point(338, 4)
        Me.CircularProgress1.Name = "CircularProgress1"
        Me.CircularProgress1.ProgressColor = System.Drawing.Color.Red
        Me.CircularProgress1.ProgressTextVisible = True
        Me.CircularProgress1.Size = New System.Drawing.Size(142, 78)
        Me.CircularProgress1.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP
        Me.CircularProgress1.TabIndex = 56
        Me.CircularProgress1.TabStop = False
        '
        'lblPreviousQuarter
        '
        Me.lblPreviousQuarter.AutoSize = True
        '
        '
        '
        Me.lblPreviousQuarter.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblPreviousQuarter.Location = New System.Drawing.Point(486, 55)
        Me.lblPreviousQuarter.Name = "lblPreviousQuarter"
        Me.lblPreviousQuarter.Size = New System.Drawing.Size(106, 20)
        Me.lblPreviousQuarter.TabIndex = 31
        Me.lblPreviousQuarter.Text = "Previous Quarter"
        '
        'btnClearAllFields
        '
        Me.btnClearAllFields.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnClearAllFields.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnClearAllFields.Image = CType(resources.GetObject("btnClearAllFields.Image"), System.Drawing.Image)
        Me.btnClearAllFields.Location = New System.Drawing.Point(26, 19)
        Me.btnClearAllFields.Name = "btnClearAllFields"
        Me.btnClearAllFields.Size = New System.Drawing.Size(126, 56)
        Me.btnClearAllFields.TabIndex = 6
        Me.btnClearAllFields.Text = "CLEAR"
        '
        'lblSelectedQuarter
        '
        Me.lblSelectedQuarter.AutoSize = True
        '
        '
        '
        Me.lblSelectedQuarter.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblSelectedQuarter.Location = New System.Drawing.Point(487, 19)
        Me.lblSelectedQuarter.Name = "lblSelectedQuarter"
        Me.lblSelectedQuarter.Size = New System.Drawing.Size(105, 20)
        Me.lblSelectedQuarter.TabIndex = 30
        Me.lblSelectedQuarter.Text = "Selected Quarter"
        '
        'btnStatement
        '
        Me.btnStatement.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnStatement.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnStatement.Image = CType(resources.GetObject("btnStatement.Image"), System.Drawing.Image)
        Me.btnStatement.Location = New System.Drawing.Point(183, 19)
        Me.btnStatement.Name = "btnStatement"
        Me.btnStatement.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlP)
        Me.btnStatement.Size = New System.Drawing.Size(126, 56)
        Me.btnStatement.TabIndex = 7
        Me.btnStatement.Text = "PRINT"
        '
        'DataGridViewX1
        '
        Me.DataGridViewX1.AllowUserToAddRows = False
        Me.DataGridViewX1.AllowUserToDeleteRows = False
        Me.DataGridViewX1.AutoGenerateColumns = False
        Me.DataGridViewX1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewX1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridViewX1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewX1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.SlNoDataGridViewTextBoxColumn, Me.DetailsOfWorkDataGridViewTextBoxColumn, Me.PreviousDataGridViewTextBoxColumn, Me.Month1DataGridViewTextBoxColumn, Me.Month2DataGridViewTextBoxColumn, Me.Month3DataGridViewTextBoxColumn, Me.PresentDataGridViewTextBoxColumn, Me.RemarksDataGridViewTextBoxColumn})
        Me.DataGridViewX1.DataSource = Me.PerformanceBindingSource
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridViewX1.DefaultCellStyle = DataGridViewCellStyle8
        Me.DataGridViewX1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridViewX1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke
        Me.DataGridViewX1.GridColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.DataGridViewX1.Location = New System.Drawing.Point(395, 45)
        Me.DataGridViewX1.MultiSelect = False
        Me.DataGridViewX1.Name = "DataGridViewX1"
        Me.DataGridViewX1.RowHeadersVisible = False
        DataGridViewCellStyle9.Font = New System.Drawing.Font("Rupee Foradian", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DataGridViewX1.RowsDefaultCellStyle = DataGridViewCellStyle9
        Me.DataGridViewX1.RowTemplate.Height = 25
        Me.DataGridViewX1.Size = New System.Drawing.Size(959, 592)
        Me.DataGridViewX1.TabIndex = 14
        '
        'bgwStatement
        '
        Me.bgwStatement.WorkerReportsProgress = True
        Me.bgwStatement.WorkerSupportsCancellation = True
        '
        'btnOpenFolder
        '
        Me.btnOpenFolder.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnOpenFolder.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnOpenFolder.Image = CType(resources.GetObject("btnOpenFolder.Image"), System.Drawing.Image)
        Me.btnOpenFolder.Location = New System.Drawing.Point(340, 19)
        Me.btnOpenFolder.Name = "btnOpenFolder"
        Me.btnOpenFolder.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlW)
        Me.btnOpenFolder.Size = New System.Drawing.Size(126, 56)
        Me.btnOpenFolder.TabIndex = 8
        Me.btnOpenFolder.Text = "Open Folder"
        '
        'SlNoDataGridViewTextBoxColumn
        '
        Me.SlNoDataGridViewTextBoxColumn.DataPropertyName = "SlNo"
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter
        Me.SlNoDataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle2
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
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter
        Me.PreviousDataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle3
        Me.PreviousDataGridViewTextBoxColumn.HeaderText = "Previous Quarter/Month"
        Me.PreviousDataGridViewTextBoxColumn.Name = "PreviousDataGridViewTextBoxColumn"
        Me.PreviousDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.PreviousDataGridViewTextBoxColumn.Width = 80
        '
        'Month1DataGridViewTextBoxColumn
        '
        Me.Month1DataGridViewTextBoxColumn.DataPropertyName = "Month1"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter
        Me.Month1DataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle4
        Me.Month1DataGridViewTextBoxColumn.HeaderText = "Month1"
        Me.Month1DataGridViewTextBoxColumn.Name = "Month1DataGridViewTextBoxColumn"
        Me.Month1DataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Month1DataGridViewTextBoxColumn.Width = 80
        '
        'Month2DataGridViewTextBoxColumn
        '
        Me.Month2DataGridViewTextBoxColumn.DataPropertyName = "Month2"
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter
        Me.Month2DataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle5
        Me.Month2DataGridViewTextBoxColumn.HeaderText = "Month2"
        Me.Month2DataGridViewTextBoxColumn.Name = "Month2DataGridViewTextBoxColumn"
        Me.Month2DataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Month2DataGridViewTextBoxColumn.Width = 80
        '
        'Month3DataGridViewTextBoxColumn
        '
        Me.Month3DataGridViewTextBoxColumn.DataPropertyName = "Month3"
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter
        Me.Month3DataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle6
        Me.Month3DataGridViewTextBoxColumn.HeaderText = "Month3"
        Me.Month3DataGridViewTextBoxColumn.Name = "Month3DataGridViewTextBoxColumn"
        Me.Month3DataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Month3DataGridViewTextBoxColumn.Width = 80
        '
        'PresentDataGridViewTextBoxColumn
        '
        Me.PresentDataGridViewTextBoxColumn.DataPropertyName = "Present"
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter
        Me.PresentDataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle7
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
        Me.RemarksDataGridViewTextBoxColumn.Width = 169
        '
        'frmQuarterlyPerformance
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
        Me.Name = "frmQuarterlyPerformance"
        Me.Text = "Quarterly Performance Statement"
        Me.TitleText = "<b>Quarterly Performance Statement</b>"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.PerformanceBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FingerPrintDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtQuarterYear, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtQuarter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelEx2.ResumeLayout(False)
        Me.GroupPanel4.ResumeLayout(False)
        Me.GroupPanel4.PerformLayout()
        Me.GroupPanel3.ResumeLayout(False)
        Me.GroupPanel3.PerformLayout()
        Me.GroupPanel2.ResumeLayout(False)
        Me.GroupPanel2.PerformLayout()
        Me.GroupPanel1.ResumeLayout(False)
        Me.GroupPanel1.PerformLayout()
        Me.PanelEx1.ResumeLayout(False)
        Me.PanelEx3.ResumeLayout(False)
        Me.PanelEx3.PerformLayout()
        CType(Me.DataGridViewX1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PerformanceTableAdapter As FingerprintInformationSystem.FingerPrintDataSetTableAdapters.PerformanceTableAdapter
    Friend WithEvents PerformanceBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents FingerPrintDataSet As FingerprintInformationSystem.FingerPrintDataSet
    Friend WithEvents FpARegisterTableAdapter As FingerprintInformationSystem.FingerPrintDataSetTableAdapters.FPAttestationRegisterTableAdapter
    Friend WithEvents SOCRegisterTableAdapter As FingerprintInformationSystem.FingerPrintDataSetTableAdapters.SOCRegisterTableAdapter
    Friend WithEvents btnInsertBlankValues As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnGenerateSelectedQuarterFromDB As DevComponents.DotNetBar.ButtonX
    Friend WithEvents LabelX5 As DevComponents.DotNetBar.LabelX
    Friend WithEvents DaRegisterTableAdapter As FingerprintInformationSystem.FingerPrintDataSetTableAdapters.DARegisterTableAdapter
    Friend WithEvents LabelX6 As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtQuarterYear As DevComponents.Editors.IntegerInput
    Friend WithEvents CdRegisterTableAdapter As FingerprintInformationSystem.FingerPrintDataSetTableAdapters.CDRegisterTableAdapter
    Friend WithEvents txtQuarter As DevComponents.Editors.IntegerInput
    Friend WithEvents txtBlankCellValue As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents PanelEx2 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents GroupPanel4 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents CheckBoxX2 As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents chkBlankValue As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents GroupPanel3 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents btnGeneratePreviousQuarterValues As DevComponents.DotNetBar.ButtonX
    Friend WithEvents chkGeneratePreviousQuarterValuesFromFile As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents chkGeneratePreviousQuarterValuesfromDataBase As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents GroupPanel2 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents chkGenerateSelectedQuarterValuesFromSavedMonthlyStatements As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents chkGenerateSelectedQuaterValuesFromDataBase As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents GroupPanel1 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents chkGenerateSelectedQuarterValuesFromESelectedQuarterFile As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents PanelEx1 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents lblPeriod As DevComponents.DotNetBar.LabelX
    Friend WithEvents PanelEx3 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents lblPreviousQuarter As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblSelectedQuarter As DevComponents.DotNetBar.LabelX
    Friend WithEvents btnClearAllFields As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnStatement As DevComponents.DotNetBar.ButtonX
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
End Class
