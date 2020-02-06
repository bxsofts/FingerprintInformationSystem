<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAnnualPerformance
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAnnualPerformance))
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.PerformanceBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.FingerPrintDataSet = New FingerprintInformationSystem.FingerPrintDataSet()
        Me.FPARegisterTableAdapter = New FingerprintInformationSystem.FingerPrintDataSetTableAdapters.FPAttestationRegisterTableAdapter()
        Me.SOCRegisterTableAdapter = New FingerprintInformationSystem.FingerPrintDataSetTableAdapters.SOCRegisterTableAdapter()
        Me.DaRegisterTableAdapter = New FingerprintInformationSystem.FingerPrintDataSetTableAdapters.DARegisterTableAdapter()
        Me.PerformanceTableAdapter = New FingerprintInformationSystem.FingerPrintDataSetTableAdapters.PerformanceTableAdapter()
        Me.CdRegisterTableAdapter = New FingerprintInformationSystem.FingerPrintDataSetTableAdapters.CDRegisterTableAdapter()
        Me.DataGridViewX1 = New DevComponents.DotNetBar.Controls.DataGridViewX()
        Me.bgwStatement = New System.ComponentModel.BackgroundWorker()
        Me.IdentificationRegisterTableAdapter1 = New FingerprintInformationSystem.FingerPrintDataSetTableAdapters.IdentificationRegisterTableAdapter()
        Me.PanelEx1 = New DevComponents.DotNetBar.PanelEx()
        Me.lblHeader = New DevComponents.DotNetBar.LabelX()
        Me.PanelEx2 = New DevComponents.DotNetBar.PanelEx()
        Me.lblQuarter1 = New DevComponents.DotNetBar.LabelX()
        Me.GroupPanel2 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.CircularProgress1 = New DevComponents.DotNetBar.Controls.CircularProgress()
        Me.btnClearAllFields = New DevComponents.DotNetBar.ButtonX()
        Me.btnOpenInWord = New DevComponents.DotNetBar.ButtonX()
        Me.btnOpenFolder = New DevComponents.DotNetBar.ButtonX()
        Me.GroupPanel1 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.btnGeneratePerformanceStatement = New DevComponents.DotNetBar.ButtonX()
        Me.LabelX4 = New DevComponents.DotNetBar.LabelX()
        Me.txtYear = New DevComponents.Editors.IntegerInput()
        Me.lblQuarter2 = New DevComponents.DotNetBar.LabelX()
        Me.lblQuarter3 = New DevComponents.DotNetBar.LabelX()
        Me.lblQuarter4 = New DevComponents.DotNetBar.LabelX()
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
        CType(Me.DataGridViewX1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelEx1.SuspendLayout()
        Me.PanelEx2.SuspendLayout()
        Me.GroupPanel2.SuspendLayout()
        Me.GroupPanel1.SuspendLayout()
        CType(Me.txtYear, System.ComponentModel.ISupportInitialize).BeginInit()
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
        'DataGridViewX1
        '
        Me.DataGridViewX1.AllowUserToAddRows = False
        Me.DataGridViewX1.AllowUserToDeleteRows = False
        Me.DataGridViewX1.AutoGenerateColumns = False
        Me.DataGridViewX1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridViewX1.DefaultCellStyle = DataGridViewCellStyle8
        Me.DataGridViewX1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridViewX1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke
        Me.DataGridViewX1.GridColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.DataGridViewX1.Location = New System.Drawing.Point(395, 45)
        Me.DataGridViewX1.MultiSelect = False
        Me.DataGridViewX1.Name = "DataGridViewX1"
        Me.DataGridViewX1.RowHeadersVisible = False
        DataGridViewCellStyle9.Font = New System.Drawing.Font("Rupee Foradian", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DataGridViewX1.RowsDefaultCellStyle = DataGridViewCellStyle9
        Me.DataGridViewX1.RowTemplate.Height = 25
        Me.DataGridViewX1.SelectAllSignVisible = False
        Me.DataGridViewX1.Size = New System.Drawing.Size(690, 474)
        Me.DataGridViewX1.TabIndex = 24
        '
        'bgwStatement
        '
        Me.bgwStatement.WorkerReportsProgress = True
        Me.bgwStatement.WorkerSupportsCancellation = True
        '
        'IdentificationRegisterTableAdapter1
        '
        Me.IdentificationRegisterTableAdapter1.ClearBeforeFill = True
        '
        'PanelEx1
        '
        Me.PanelEx1.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.PanelEx1.Controls.Add(Me.lblHeader)
        Me.PanelEx1.DisabledBackColor = System.Drawing.Color.Empty
        Me.PanelEx1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelEx1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PanelEx1.Location = New System.Drawing.Point(395, 0)
        Me.PanelEx1.Name = "PanelEx1"
        Me.PanelEx1.Size = New System.Drawing.Size(690, 45)
        Me.PanelEx1.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx1.Style.GradientAngle = 90
        Me.PanelEx1.TabIndex = 22
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
        Me.lblHeader.Size = New System.Drawing.Size(690, 45)
        Me.lblHeader.TabIndex = 34
        Me.lblHeader.Text = "WORK DONE STATEMENT"
        Me.lblHeader.TextAlignment = System.Drawing.StringAlignment.Center
        '
        'PanelEx2
        '
        Me.PanelEx2.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.PanelEx2.Controls.Add(Me.lblQuarter4)
        Me.PanelEx2.Controls.Add(Me.lblQuarter3)
        Me.PanelEx2.Controls.Add(Me.lblQuarter2)
        Me.PanelEx2.Controls.Add(Me.lblQuarter1)
        Me.PanelEx2.Controls.Add(Me.GroupPanel2)
        Me.PanelEx2.Controls.Add(Me.GroupPanel1)
        Me.PanelEx2.DisabledBackColor = System.Drawing.Color.Empty
        Me.PanelEx2.Dock = System.Windows.Forms.DockStyle.Left
        Me.PanelEx2.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PanelEx2.Location = New System.Drawing.Point(0, 0)
        Me.PanelEx2.Name = "PanelEx2"
        Me.PanelEx2.Size = New System.Drawing.Size(395, 519)
        Me.PanelEx2.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx2.Style.GradientAngle = 90
        Me.PanelEx2.TabIndex = 23
        '
        'lblQuarter1
        '
        Me.lblQuarter1.AutoSize = True
        '
        '
        '
        Me.lblQuarter1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblQuarter1.Location = New System.Drawing.Point(7, 285)
        Me.lblQuarter1.Name = "lblQuarter1"
        Me.lblQuarter1.Size = New System.Drawing.Size(56, 18)
        Me.lblQuarter1.TabIndex = 35
        Me.lblQuarter1.Text = "Quarter 1"
        '
        'GroupPanel2
        '
        Me.GroupPanel2.BackColor = System.Drawing.Color.Transparent
        Me.GroupPanel2.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanel2.Controls.Add(Me.CircularProgress1)
        Me.GroupPanel2.Controls.Add(Me.btnClearAllFields)
        Me.GroupPanel2.Controls.Add(Me.btnOpenInWord)
        Me.GroupPanel2.Controls.Add(Me.btnOpenFolder)
        Me.GroupPanel2.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel2.Location = New System.Drawing.Point(7, 135)
        Me.GroupPanel2.Name = "GroupPanel2"
        Me.GroupPanel2.Size = New System.Drawing.Size(380, 109)
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
        Me.GroupPanel2.Text = "Print Statement In MS Word"
        '
        'CircularProgress1
        '
        '
        '
        '
        Me.CircularProgress1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.CircularProgress1.FocusCuesEnabled = False
        Me.CircularProgress1.Location = New System.Drawing.Point(119, 14)
        Me.CircularProgress1.Name = "CircularProgress1"
        Me.CircularProgress1.ProgressColor = System.Drawing.Color.Red
        Me.CircularProgress1.ProgressTextVisible = True
        Me.CircularProgress1.Size = New System.Drawing.Size(136, 56)
        Me.CircularProgress1.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP
        Me.CircularProgress1.TabIndex = 55
        Me.CircularProgress1.TabStop = False
        '
        'btnClearAllFields
        '
        Me.btnClearAllFields.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnClearAllFields.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnClearAllFields.Image = CType(resources.GetObject("btnClearAllFields.Image"), System.Drawing.Image)
        Me.btnClearAllFields.Location = New System.Drawing.Point(5, 14)
        Me.btnClearAllFields.Name = "btnClearAllFields"
        Me.btnClearAllFields.Size = New System.Drawing.Size(112, 56)
        Me.btnClearAllFields.TabIndex = 7
        Me.btnClearAllFields.Text = "CLEAR"
        '
        'btnOpenInWord
        '
        Me.btnOpenInWord.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnOpenInWord.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnOpenInWord.Image = CType(resources.GetObject("btnOpenInWord.Image"), System.Drawing.Image)
        Me.btnOpenInWord.Location = New System.Drawing.Point(131, 14)
        Me.btnOpenInWord.Name = "btnOpenInWord"
        Me.btnOpenInWord.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlW)
        Me.btnOpenInWord.Size = New System.Drawing.Size(112, 56)
        Me.btnOpenInWord.TabIndex = 8
        Me.btnOpenInWord.Text = "OPEN IN WORD"
        '
        'btnOpenFolder
        '
        Me.btnOpenFolder.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnOpenFolder.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnOpenFolder.Image = CType(resources.GetObject("btnOpenFolder.Image"), System.Drawing.Image)
        Me.btnOpenFolder.Location = New System.Drawing.Point(257, 14)
        Me.btnOpenFolder.Name = "btnOpenFolder"
        Me.btnOpenFolder.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlW)
        Me.btnOpenFolder.Size = New System.Drawing.Size(112, 56)
        Me.btnOpenFolder.TabIndex = 9
        Me.btnOpenFolder.Text = "Open Folder"
        '
        'GroupPanel1
        '
        Me.GroupPanel1.BackColor = System.Drawing.Color.Transparent
        Me.GroupPanel1.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanel1.Controls.Add(Me.btnGeneratePerformanceStatement)
        Me.GroupPanel1.Controls.Add(Me.LabelX4)
        Me.GroupPanel1.Controls.Add(Me.txtYear)
        Me.GroupPanel1.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel1.Location = New System.Drawing.Point(7, 12)
        Me.GroupPanel1.Name = "GroupPanel1"
        Me.GroupPanel1.Size = New System.Drawing.Size(380, 106)
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
        Me.btnGeneratePerformanceStatement.Location = New System.Drawing.Point(267, 12)
        Me.btnGeneratePerformanceStatement.Name = "btnGeneratePerformanceStatement"
        Me.btnGeneratePerformanceStatement.Size = New System.Drawing.Size(102, 58)
        Me.btnGeneratePerformanceStatement.TabIndex = 3
        Me.btnGeneratePerformanceStatement.Text = "GENERATE"
        '
        'LabelX4
        '
        Me.LabelX4.AutoSize = True
        '
        '
        '
        Me.LabelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX4.Location = New System.Drawing.Point(3, 32)
        Me.LabelX4.Name = "LabelX4"
        Me.LabelX4.Size = New System.Drawing.Size(28, 18)
        Me.LabelX4.TabIndex = 30
        Me.LabelX4.Text = "Year"
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
        Me.txtYear.Location = New System.Drawing.Point(50, 27)
        Me.txtYear.MaxValue = 2099
        Me.txtYear.MinValue = 1900
        Me.txtYear.Name = "txtYear"
        Me.txtYear.ShowUpDown = True
        Me.txtYear.Size = New System.Drawing.Size(119, 29)
        Me.txtYear.TabIndex = 2
        Me.txtYear.Value = 1900
        Me.txtYear.WatermarkText = "Year"
        '
        'lblQuarter2
        '
        Me.lblQuarter2.AutoSize = True
        '
        '
        '
        Me.lblQuarter2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblQuarter2.Location = New System.Drawing.Point(7, 309)
        Me.lblQuarter2.Name = "lblQuarter2"
        Me.lblQuarter2.Size = New System.Drawing.Size(56, 18)
        Me.lblQuarter2.TabIndex = 36
        Me.lblQuarter2.Text = "Quarter 2"
        '
        'lblQuarter3
        '
        Me.lblQuarter3.AutoSize = True
        '
        '
        '
        Me.lblQuarter3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblQuarter3.Location = New System.Drawing.Point(7, 333)
        Me.lblQuarter3.Name = "lblQuarter3"
        Me.lblQuarter3.Size = New System.Drawing.Size(56, 18)
        Me.lblQuarter3.TabIndex = 37
        Me.lblQuarter3.Text = "Quarter 3"
        '
        'lblQuarter4
        '
        Me.lblQuarter4.AutoSize = True
        '
        '
        '
        Me.lblQuarter4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblQuarter4.Location = New System.Drawing.Point(7, 357)
        Me.lblQuarter4.Name = "lblQuarter4"
        Me.lblQuarter4.Size = New System.Drawing.Size(56, 18)
        Me.lblQuarter4.TabIndex = 38
        Me.lblQuarter4.Text = "Quarter 4"
        '
        'SlNoDataGridViewTextBoxColumn
        '
        Me.SlNoDataGridViewTextBoxColumn.DataPropertyName = "SlNo"
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter
        Me.SlNoDataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle2
        Me.SlNoDataGridViewTextBoxColumn.HeaderText = "Sl.No."
        Me.SlNoDataGridViewTextBoxColumn.Name = "SlNoDataGridViewTextBoxColumn"
        Me.SlNoDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.SlNoDataGridViewTextBoxColumn.Width = 50
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
        Me.PreviousDataGridViewTextBoxColumn.HeaderText = "Quarter 1"
        Me.PreviousDataGridViewTextBoxColumn.Name = "PreviousDataGridViewTextBoxColumn"
        Me.PreviousDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.PreviousDataGridViewTextBoxColumn.Width = 85
        '
        'Month1DataGridViewTextBoxColumn
        '
        Me.Month1DataGridViewTextBoxColumn.DataPropertyName = "Month1"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter
        Me.Month1DataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle4
        Me.Month1DataGridViewTextBoxColumn.HeaderText = "Quarter 2"
        Me.Month1DataGridViewTextBoxColumn.Name = "Month1DataGridViewTextBoxColumn"
        Me.Month1DataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Month1DataGridViewTextBoxColumn.Width = 85
        '
        'Month2DataGridViewTextBoxColumn
        '
        Me.Month2DataGridViewTextBoxColumn.DataPropertyName = "Month2"
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter
        Me.Month2DataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle5
        Me.Month2DataGridViewTextBoxColumn.HeaderText = "Quarter 3"
        Me.Month2DataGridViewTextBoxColumn.Name = "Month2DataGridViewTextBoxColumn"
        Me.Month2DataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Month2DataGridViewTextBoxColumn.Width = 85
        '
        'Month3DataGridViewTextBoxColumn
        '
        Me.Month3DataGridViewTextBoxColumn.DataPropertyName = "Month3"
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter
        Me.Month3DataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle6
        Me.Month3DataGridViewTextBoxColumn.HeaderText = "Quarter 4"
        Me.Month3DataGridViewTextBoxColumn.Name = "Month3DataGridViewTextBoxColumn"
        Me.Month3DataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Month3DataGridViewTextBoxColumn.Width = 85
        '
        'PresentDataGridViewTextBoxColumn
        '
        Me.PresentDataGridViewTextBoxColumn.DataPropertyName = "Present"
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter
        Me.PresentDataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle7
        Me.PresentDataGridViewTextBoxColumn.HeaderText = "Total"
        Me.PresentDataGridViewTextBoxColumn.Name = "PresentDataGridViewTextBoxColumn"
        Me.PresentDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.PresentDataGridViewTextBoxColumn.Width = 85
        '
        'RemarksDataGridViewTextBoxColumn
        '
        Me.RemarksDataGridViewTextBoxColumn.DataPropertyName = "Remarks"
        Me.RemarksDataGridViewTextBoxColumn.HeaderText = "Remarks"
        Me.RemarksDataGridViewTextBoxColumn.Name = "RemarksDataGridViewTextBoxColumn"
        Me.RemarksDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.RemarksDataGridViewTextBoxColumn.Width = 140
        '
        'frmAnnualPerformance
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1085, 519)
        Me.Controls.Add(Me.DataGridViewX1)
        Me.Controls.Add(Me.PanelEx1)
        Me.Controls.Add(Me.PanelEx2)
        Me.DoubleBuffered = True
        Me.EnableGlass = False
        Me.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "frmAnnualPerformance"
        Me.Text = "Annual Performance"
        Me.TitleText = "<b>Annual Performance</b>"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.PerformanceBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FingerPrintDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridViewX1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelEx1.ResumeLayout(False)
        Me.PanelEx2.ResumeLayout(False)
        Me.PanelEx2.PerformLayout()
        Me.GroupPanel2.ResumeLayout(False)
        Me.GroupPanel1.ResumeLayout(False)
        Me.GroupPanel1.PerformLayout()
        CType(Me.txtYear, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PerformanceBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents FingerPrintDataSet As FingerprintInformationSystem.FingerPrintDataSet
    Friend WithEvents FPARegisterTableAdapter As FingerprintInformationSystem.FingerPrintDataSetTableAdapters.FPAttestationRegisterTableAdapter
    Friend WithEvents SOCRegisterTableAdapter As FingerprintInformationSystem.FingerPrintDataSetTableAdapters.SOCRegisterTableAdapter
    Friend WithEvents DaRegisterTableAdapter As FingerprintInformationSystem.FingerPrintDataSetTableAdapters.DARegisterTableAdapter
    Friend WithEvents PerformanceTableAdapter As FingerprintInformationSystem.FingerPrintDataSetTableAdapters.PerformanceTableAdapter
    Friend WithEvents CdRegisterTableAdapter As FingerprintInformationSystem.FingerPrintDataSetTableAdapters.CDRegisterTableAdapter
    Friend WithEvents DataGridViewX1 As DevComponents.DotNetBar.Controls.DataGridViewX
    Friend WithEvents bgwStatement As System.ComponentModel.BackgroundWorker
    Friend WithEvents IdentificationRegisterTableAdapter1 As FingerprintInformationSystem.FingerPrintDataSetTableAdapters.IdentificationRegisterTableAdapter
    Friend WithEvents PanelEx1 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents lblHeader As DevComponents.DotNetBar.LabelX
    Friend WithEvents PanelEx2 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents lblQuarter1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents GroupPanel2 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents CircularProgress1 As DevComponents.DotNetBar.Controls.CircularProgress
    Friend WithEvents btnClearAllFields As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnOpenInWord As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnOpenFolder As DevComponents.DotNetBar.ButtonX
    Friend WithEvents GroupPanel1 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents btnGeneratePerformanceStatement As DevComponents.DotNetBar.ButtonX
    Friend WithEvents LabelX4 As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtYear As DevComponents.Editors.IntegerInput
    Friend WithEvents lblQuarter4 As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblQuarter3 As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblQuarter2 As DevComponents.DotNetBar.LabelX
    Friend WithEvents SlNoDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DetailsOfWorkDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PreviousDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Month1DataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Month2DataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Month3DataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PresentDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RemarksDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
