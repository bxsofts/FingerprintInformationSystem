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
        Me.LabelX5 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX6 = New DevComponents.DotNetBar.LabelX()
        Me.txtQuarterYear = New DevComponents.Editors.IntegerInput()
        Me.txtQuarter = New DevComponents.Editors.IntegerInput()
        Me.PerformanceTableAdapter = New FingerprintInformationSystem.FingerPrintDataSetTableAdapters.PerformanceTableAdapter()
        Me.FpARegisterTableAdapter = New FingerprintInformationSystem.FingerPrintDataSetTableAdapters.FPAttestationRegisterTableAdapter()
        Me.SOCRegisterTableAdapter = New FingerprintInformationSystem.FingerPrintDataSetTableAdapters.SOCRegisterTableAdapter()
        Me.DaRegisterTableAdapter = New FingerprintInformationSystem.FingerPrintDataSetTableAdapters.DARegisterTableAdapter()
        Me.CdRegisterTableAdapter = New FingerprintInformationSystem.FingerPrintDataSetTableAdapters.CDRegisterTableAdapter()
        Me.PanelEx2 = New DevComponents.DotNetBar.PanelEx()
        Me.lblMonth3 = New DevComponents.DotNetBar.LabelX()
        Me.lblMonth2 = New DevComponents.DotNetBar.LabelX()
        Me.lblMonth1 = New DevComponents.DotNetBar.LabelX()
        Me.lblPreviousQuarter = New DevComponents.DotNetBar.LabelX()
        Me.GroupPanel2 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.CircularProgress1 = New DevComponents.DotNetBar.Controls.CircularProgress()
        Me.btnClearAllFields = New DevComponents.DotNetBar.ButtonX()
        Me.btnOpenFolder = New DevComponents.DotNetBar.ButtonX()
        Me.btnStatement = New DevComponents.DotNetBar.ButtonX()
        Me.GroupPanel1 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.btnGeneratePerformanceStatement = New DevComponents.DotNetBar.ButtonX()
        Me.PanelEx1 = New DevComponents.DotNetBar.PanelEx()
        Me.lblPeriod = New DevComponents.DotNetBar.LabelX()
        Me.DataGridViewX1 = New DevComponents.DotNetBar.Controls.DataGridViewX()
        Me.SlNoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DetailsOfWorkDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PreviousDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Month1DataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Month2DataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Month3DataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PresentDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RemarksDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.bgwSaveStatement = New System.ComponentModel.BackgroundWorker()
        Me.IdentificationRegisterTableAdapter1 = New FingerprintInformationSystem.FingerPrintDataSetTableAdapters.IdentificationRegisterTableAdapter()
        CType(Me.PerformanceBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FingerPrintDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtQuarterYear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtQuarter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelEx2.SuspendLayout()
        Me.GroupPanel2.SuspendLayout()
        Me.GroupPanel1.SuspendLayout()
        Me.PanelEx1.SuspendLayout()
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
        'LabelX5
        '
        Me.LabelX5.AutoSize = True
        '
        '
        '
        Me.LabelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX5.Location = New System.Drawing.Point(147, 19)
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
        Me.LabelX6.Location = New System.Drawing.Point(6, 19)
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
        Me.txtQuarterYear.Location = New System.Drawing.Point(181, 14)
        Me.txtQuarterYear.MaxValue = 2099
        Me.txtQuarterYear.MinValue = 1900
        Me.txtQuarterYear.MouseWheelValueChangeEnabled = False
        Me.txtQuarterYear.Name = "txtQuarterYear"
        Me.txtQuarterYear.ShowUpDown = True
        Me.txtQuarterYear.Size = New System.Drawing.Size(75, 29)
        Me.txtQuarterYear.TabIndex = 2
        Me.txtQuarterYear.Value = 1900
        Me.txtQuarterYear.WatermarkText = "Year"
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
        Me.txtQuarter.Location = New System.Drawing.Point(59, 14)
        Me.txtQuarter.MaxValue = 4
        Me.txtQuarter.MinValue = 1
        Me.txtQuarter.MouseWheelValueChangeEnabled = False
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
        'PanelEx2
        '
        Me.PanelEx2.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.PanelEx2.Controls.Add(Me.lblMonth3)
        Me.PanelEx2.Controls.Add(Me.lblMonth2)
        Me.PanelEx2.Controls.Add(Me.lblMonth1)
        Me.PanelEx2.Controls.Add(Me.lblPreviousQuarter)
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
        'lblMonth3
        '
        Me.lblMonth3.AutoSize = True
        '
        '
        '
        Me.lblMonth3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblMonth3.Location = New System.Drawing.Point(16, 381)
        Me.lblMonth3.Name = "lblMonth3"
        Me.lblMonth3.Size = New System.Drawing.Size(51, 18)
        Me.lblMonth3.TabIndex = 60
        Me.lblMonth3.Text = "Month 3"
        '
        'lblMonth2
        '
        Me.lblMonth2.AutoSize = True
        '
        '
        '
        Me.lblMonth2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblMonth2.Location = New System.Drawing.Point(16, 349)
        Me.lblMonth2.Name = "lblMonth2"
        Me.lblMonth2.Size = New System.Drawing.Size(51, 18)
        Me.lblMonth2.TabIndex = 61
        Me.lblMonth2.Text = "Month 2"
        '
        'lblMonth1
        '
        Me.lblMonth1.AutoSize = True
        '
        '
        '
        Me.lblMonth1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblMonth1.Location = New System.Drawing.Point(16, 317)
        Me.lblMonth1.Name = "lblMonth1"
        Me.lblMonth1.Size = New System.Drawing.Size(51, 18)
        Me.lblMonth1.TabIndex = 58
        Me.lblMonth1.Text = "Month 1"
        '
        'lblPreviousQuarter
        '
        Me.lblPreviousQuarter.AutoSize = True
        '
        '
        '
        Me.lblPreviousQuarter.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblPreviousQuarter.Location = New System.Drawing.Point(16, 285)
        Me.lblPreviousQuarter.Name = "lblPreviousQuarter"
        Me.lblPreviousQuarter.Size = New System.Drawing.Size(98, 18)
        Me.lblPreviousQuarter.TabIndex = 59
        Me.lblPreviousQuarter.Text = "Previous Quarter"
        '
        'GroupPanel2
        '
        Me.GroupPanel2.BackColor = System.Drawing.Color.Transparent
        Me.GroupPanel2.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanel2.Controls.Add(Me.CircularProgress1)
        Me.GroupPanel2.Controls.Add(Me.btnClearAllFields)
        Me.GroupPanel2.Controls.Add(Me.btnOpenFolder)
        Me.GroupPanel2.Controls.Add(Me.btnStatement)
        Me.GroupPanel2.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel2.Location = New System.Drawing.Point(7, 146)
        Me.GroupPanel2.Name = "GroupPanel2"
        Me.GroupPanel2.Size = New System.Drawing.Size(380, 100)
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
        Me.GroupPanel2.TabIndex = 57
        Me.GroupPanel2.Text = "Print Statement In MS Word"
        '
        'CircularProgress1
        '
        '
        '
        '
        Me.CircularProgress1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.CircularProgress1.FocusCuesEnabled = False
        Me.CircularProgress1.Location = New System.Drawing.Point(117, 5)
        Me.CircularProgress1.Name = "CircularProgress1"
        Me.CircularProgress1.ProgressColor = System.Drawing.Color.Red
        Me.CircularProgress1.ProgressTextVisible = True
        Me.CircularProgress1.Size = New System.Drawing.Size(140, 67)
        Me.CircularProgress1.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP
        Me.CircularProgress1.TabIndex = 56
        Me.CircularProgress1.TabStop = False
        '
        'btnClearAllFields
        '
        Me.btnClearAllFields.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnClearAllFields.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnClearAllFields.Image = CType(resources.GetObject("btnClearAllFields.Image"), System.Drawing.Image)
        Me.btnClearAllFields.Location = New System.Drawing.Point(6, 9)
        Me.btnClearAllFields.Name = "btnClearAllFields"
        Me.btnClearAllFields.Size = New System.Drawing.Size(107, 56)
        Me.btnClearAllFields.TabIndex = 6
        Me.btnClearAllFields.Text = "CLEAR"
        '
        'btnOpenFolder
        '
        Me.btnOpenFolder.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnOpenFolder.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnOpenFolder.Image = CType(resources.GetObject("btnOpenFolder.Image"), System.Drawing.Image)
        Me.btnOpenFolder.Location = New System.Drawing.Point(262, 9)
        Me.btnOpenFolder.Name = "btnOpenFolder"
        Me.btnOpenFolder.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlW)
        Me.btnOpenFolder.Size = New System.Drawing.Size(107, 56)
        Me.btnOpenFolder.TabIndex = 8
        Me.btnOpenFolder.Text = "Open Folder"
        '
        'btnStatement
        '
        Me.btnStatement.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnStatement.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnStatement.Image = CType(resources.GetObject("btnStatement.Image"), System.Drawing.Image)
        Me.btnStatement.Location = New System.Drawing.Point(134, 9)
        Me.btnStatement.Name = "btnStatement"
        Me.btnStatement.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlP)
        Me.btnStatement.Size = New System.Drawing.Size(107, 56)
        Me.btnStatement.TabIndex = 7
        Me.btnStatement.Text = "PRINT"
        '
        'GroupPanel1
        '
        Me.GroupPanel1.BackColor = System.Drawing.Color.Transparent
        Me.GroupPanel1.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanel1.Controls.Add(Me.btnGeneratePerformanceStatement)
        Me.GroupPanel1.Controls.Add(Me.LabelX6)
        Me.GroupPanel1.Controls.Add(Me.txtQuarter)
        Me.GroupPanel1.Controls.Add(Me.txtQuarterYear)
        Me.GroupPanel1.Controls.Add(Me.LabelX5)
        Me.GroupPanel1.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel1.Location = New System.Drawing.Point(7, 12)
        Me.GroupPanel1.Name = "GroupPanel1"
        Me.GroupPanel1.Size = New System.Drawing.Size(380, 90)
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
        Me.GroupPanel1.Text = "Generate Statement for Selected Quarter"
        '
        'btnGeneratePerformanceStatement
        '
        Me.btnGeneratePerformanceStatement.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnGeneratePerformanceStatement.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnGeneratePerformanceStatement.Location = New System.Drawing.Point(267, 4)
        Me.btnGeneratePerformanceStatement.Name = "btnGeneratePerformanceStatement"
        Me.btnGeneratePerformanceStatement.Size = New System.Drawing.Size(99, 58)
        Me.btnGeneratePerformanceStatement.TabIndex = 3
        Me.btnGeneratePerformanceStatement.Text = "GENERATE"
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
        Me.DataGridViewX1.GridColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.DataGridViewX1.Location = New System.Drawing.Point(395, 45)
        Me.DataGridViewX1.MultiSelect = False
        Me.DataGridViewX1.Name = "DataGridViewX1"
        Me.DataGridViewX1.RowHeadersVisible = False
        DataGridViewCellStyle9.Font = New System.Drawing.Font("Rupee Foradian", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DataGridViewX1.RowsDefaultCellStyle = DataGridViewCellStyle9
        Me.DataGridViewX1.RowTemplate.Height = 25
        Me.DataGridViewX1.SelectAllSignVisible = False
        Me.DataGridViewX1.Size = New System.Drawing.Size(959, 688)
        Me.DataGridViewX1.TabIndex = 14
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
        'bgwSaveStatement
        '
        Me.bgwSaveStatement.WorkerReportsProgress = True
        Me.bgwSaveStatement.WorkerSupportsCancellation = True
        '
        'IdentificationRegisterTableAdapter1
        '
        Me.IdentificationRegisterTableAdapter1.ClearBeforeFill = True
        '
        'frmQuarterlyPerformance
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1354, 733)
        Me.Controls.Add(Me.DataGridViewX1)
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
        Me.PanelEx2.PerformLayout()
        Me.GroupPanel2.ResumeLayout(False)
        Me.GroupPanel1.ResumeLayout(False)
        Me.GroupPanel1.PerformLayout()
        Me.PanelEx1.ResumeLayout(False)
        CType(Me.DataGridViewX1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PerformanceTableAdapter As FingerprintInformationSystem.FingerPrintDataSetTableAdapters.PerformanceTableAdapter
    Friend WithEvents PerformanceBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents FingerPrintDataSet As FingerprintInformationSystem.FingerPrintDataSet
    Friend WithEvents FpARegisterTableAdapter As FingerprintInformationSystem.FingerPrintDataSetTableAdapters.FPAttestationRegisterTableAdapter
    Friend WithEvents SOCRegisterTableAdapter As FingerprintInformationSystem.FingerPrintDataSetTableAdapters.SOCRegisterTableAdapter
    Friend WithEvents LabelX5 As DevComponents.DotNetBar.LabelX
    Friend WithEvents DaRegisterTableAdapter As FingerprintInformationSystem.FingerPrintDataSetTableAdapters.DARegisterTableAdapter
    Friend WithEvents LabelX6 As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtQuarterYear As DevComponents.Editors.IntegerInput
    Friend WithEvents CdRegisterTableAdapter As FingerprintInformationSystem.FingerPrintDataSetTableAdapters.CDRegisterTableAdapter
    Friend WithEvents txtQuarter As DevComponents.Editors.IntegerInput
    Friend WithEvents PanelEx2 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents GroupPanel1 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents PanelEx1 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents lblPeriod As DevComponents.DotNetBar.LabelX
    Friend WithEvents btnClearAllFields As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnStatement As DevComponents.DotNetBar.ButtonX
    Friend WithEvents DataGridViewX1 As DevComponents.DotNetBar.Controls.DataGridViewX
    Friend WithEvents CircularProgress1 As DevComponents.DotNetBar.Controls.CircularProgress
    Friend WithEvents bgwSaveStatement As System.ComponentModel.BackgroundWorker
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
    Friend WithEvents GroupPanel2 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents lblMonth1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblPreviousQuarter As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblMonth3 As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblMonth2 As DevComponents.DotNetBar.LabelX
    Friend WithEvents IdentificationRegisterTableAdapter1 As FingerprintInformationSystem.FingerPrintDataSetTableAdapters.IdentificationRegisterTableAdapter
End Class
