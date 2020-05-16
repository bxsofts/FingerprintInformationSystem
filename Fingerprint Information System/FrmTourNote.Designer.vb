<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmTourNote
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
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle15 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle16 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmTourNote))
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.PanelEx1 = New DevComponents.DotNetBar.PanelEx()
        Me.PanelEx3 = New DevComponents.DotNetBar.PanelEx()
        Me.PanelEx6 = New DevComponents.DotNetBar.PanelEx()
        Me.dgvWD = New DevComponents.DotNetBar.Controls.DataGridViewX()
        Me.WeeklyDiaryBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.WeeklyDiaryDataSet1 = New FingerprintInformationSystem.WeeklyDiaryDataSet()
        Me.dgvSOC = New DevComponents.DotNetBar.Controls.DataGridViewX()
        Me.SOCNumberDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SelectRow = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.DateOfInspectionDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PoliceStationDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CrimeNumberDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PlaceOfOccurrenceDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.InvestigatingOfficer = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SOCRegisterBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.FingerPrintDataSet = New FingerprintInformationSystem.FingerPrintDataSet()
        Me.StatusBar = New DevComponents.DotNetBar.Bar()
        Me.lblTickedRecords = New DevComponents.DotNetBar.LabelItem()
        Me.btnSelectAll = New DevComponents.DotNetBar.ButtonItem()
        Me.btnDeselectAll = New DevComponents.DotNetBar.ButtonItem()
        Me.lblOfficerName = New DevComponents.DotNetBar.LabelItem()
        Me.lblPEN = New DevComponents.DotNetBar.LabelItem()
        Me.lblBasicPay = New DevComponents.DotNetBar.LabelItem()
        Me.lblDA = New DevComponents.DotNetBar.LabelItem()
        Me.PanelSOC = New DevComponents.DotNetBar.PanelEx()
        Me.PanelEx2 = New DevComponents.DotNetBar.PanelEx()
        Me.pnlBackup = New DevComponents.DotNetBar.PanelEx()
        Me.cprgBackup = New DevComponents.DotNetBar.Controls.CircularProgress()
        Me.btnUploadAllFiles = New DevComponents.DotNetBar.ButtonX()
        Me.btnUploadSelectedMonthTAFiles = New DevComponents.DotNetBar.ButtonItem()
        Me.lblBackup = New DevComponents.DotNetBar.LabelX()
        Me.GroupPanel3 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.cprgBlankForms = New DevComponents.DotNetBar.Controls.CircularProgress()
        Me.btnGenerateBlankTR56A = New DevComponents.DotNetBar.ButtonX()
        Me.btnGenerateBlankTourNote = New DevComponents.DotNetBar.ButtonX()
        Me.btnGenerateBlankTR47 = New DevComponents.DotNetBar.ButtonX()
        Me.btnGenerateBlankTR47Outer = New DevComponents.DotNetBar.ButtonItem()
        Me.GroupPanel2 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.cprgGenerateFiles = New DevComponents.DotNetBar.Controls.CircularProgress()
        Me.PanelEx4 = New DevComponents.DotNetBar.PanelEx()
        Me.LabelX7 = New DevComponents.DotNetBar.LabelX()
        Me.chkThreeRows = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.chkSingleRow = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.btnOpenTABillFolder = New DevComponents.DotNetBar.ButtonX()
        Me.btnGenerateTourNote = New DevComponents.DotNetBar.ButtonX()
        Me.btnGenerateTABill = New DevComponents.DotNetBar.ButtonX()
        Me.btnGenerateTABillOuter = New DevComponents.DotNetBar.ButtonItem()
        Me.GroupPanel1 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.LabelX3 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX4 = New DevComponents.DotNetBar.LabelX()
        Me.cmbSOCOfficer = New DevComponents.DotNetBar.Controls.ComboBoxEx()
        Me.txtYear = New DevComponents.Editors.IntegerInput()
        Me.cmbMonth = New DevComponents.DotNetBar.Controls.ComboBoxEx()
        Me.LabelX1 = New DevComponents.DotNetBar.LabelX()
        Me.GroupPanel5 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.txtDVNumber = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX6 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX5 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX2 = New DevComponents.DotNetBar.LabelX()
        Me.chkUsePS = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.chkUsePO = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.txtStartingLocation = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.pnlStatus = New DevComponents.DotNetBar.PanelEx()
        Me.lblSavedTABill = New DevComponents.DotNetBar.LabelX()
        Me.lblSavedTourNote = New DevComponents.DotNetBar.LabelX()
        Me.SocRegisterTableAdapter1 = New FingerprintInformationSystem.FingerPrintDataSetTableAdapters.SOCRegisterTableAdapter()
        Me.PoliceStationListTableAdapter1 = New FingerprintInformationSystem.FingerPrintDataSetTableAdapters.PoliceStationListTableAdapter()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.bgwBlankForms = New System.ComponentModel.BackgroundWorker()
        Me.bgwSingleTN = New System.ComponentModel.BackgroundWorker()
        Me.bgwThreeTN = New System.ComponentModel.BackgroundWorker()
        Me.bgwTR56 = New System.ComponentModel.BackgroundWorker()
        Me.bgwTR56ThreeLine = New System.ComponentModel.BackgroundWorker()
        Me.bgwTR47 = New System.ComponentModel.BackgroundWorker()
        Me.bgwTR47ThreeLine = New System.ComponentModel.BackgroundWorker()
        Me.bgwUploadFile = New System.ComponentModel.BackgroundWorker()
        Me.bgwUploadAllFiles = New System.ComponentModel.BackgroundWorker()
        Me.CommonSettingsTableAdapter1 = New FingerprintInformationSystem.FingerPrintDataSetTableAdapters.CommonSettingsTableAdapter()
        Me.WeeklyDiaryTableAdapter1 = New FingerprintInformationSystem.WeeklyDiaryDataSetTableAdapters.WeeklyDiaryTableAdapter()
        Me.IDDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DiaryDateDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.WorkDoneDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RemarksDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SelectRecord = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.TourFrom = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TourTo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TourPurpose = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PanelEx1.SuspendLayout()
        Me.PanelEx3.SuspendLayout()
        Me.PanelEx6.SuspendLayout()
        CType(Me.dgvWD, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.WeeklyDiaryBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.WeeklyDiaryDataSet1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvSOC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SOCRegisterBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FingerPrintDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelEx2.SuspendLayout()
        Me.pnlBackup.SuspendLayout()
        Me.GroupPanel3.SuspendLayout()
        Me.GroupPanel2.SuspendLayout()
        Me.PanelEx4.SuspendLayout()
        Me.GroupPanel1.SuspendLayout()
        CType(Me.txtYear, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupPanel5.SuspendLayout()
        Me.pnlStatus.SuspendLayout()
        Me.SuspendLayout()
        '
        'PanelEx1
        '
        Me.PanelEx1.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.PanelEx1.Controls.Add(Me.PanelEx3)
        Me.PanelEx1.Controls.Add(Me.PanelEx2)
        Me.PanelEx1.DisabledBackColor = System.Drawing.Color.Empty
        Me.PanelEx1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelEx1.Location = New System.Drawing.Point(0, 0)
        Me.PanelEx1.Name = "PanelEx1"
        Me.PanelEx1.Size = New System.Drawing.Size(1292, 699)
        Me.PanelEx1.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx1.Style.GradientAngle = 90
        Me.PanelEx1.TabIndex = 0
        '
        'PanelEx3
        '
        Me.PanelEx3.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.PanelEx3.Controls.Add(Me.PanelEx6)
        Me.PanelEx3.Controls.Add(Me.StatusBar)
        Me.PanelEx3.Controls.Add(Me.PanelSOC)
        Me.PanelEx3.DisabledBackColor = System.Drawing.Color.Empty
        Me.PanelEx3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelEx3.Location = New System.Drawing.Point(430, 0)
        Me.PanelEx3.Name = "PanelEx3"
        Me.PanelEx3.Size = New System.Drawing.Size(862, 699)
        Me.PanelEx3.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx3.Style.GradientAngle = 90
        Me.PanelEx3.TabIndex = 44
        '
        'PanelEx6
        '
        Me.PanelEx6.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx6.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.PanelEx6.Controls.Add(Me.dgvWD)
        Me.PanelEx6.Controls.Add(Me.dgvSOC)
        Me.PanelEx6.DisabledBackColor = System.Drawing.Color.Empty
        Me.PanelEx6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelEx6.Location = New System.Drawing.Point(0, 44)
        Me.PanelEx6.Name = "PanelEx6"
        Me.PanelEx6.Size = New System.Drawing.Size(862, 625)
        Me.PanelEx6.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx6.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx6.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.PanelEx6.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx6.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx6.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx6.Style.GradientAngle = 90
        Me.PanelEx6.TabIndex = 58
        '
        'dgvWD
        '
        Me.dgvWD.AllowUserToAddRows = False
        Me.dgvWD.AllowUserToDeleteRows = False
        Me.dgvWD.AllowUserToOrderColumns = True
        Me.dgvWD.AutoGenerateColumns = False
        Me.dgvWD.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgvWD.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvWD.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvWD.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvWD.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.IDDataGridViewTextBoxColumn1, Me.DiaryDateDataGridViewTextBoxColumn, Me.WorkDoneDataGridViewTextBoxColumn, Me.RemarksDataGridViewTextBoxColumn1, Me.SelectRecord, Me.TourFrom, Me.TourTo, Me.TourPurpose})
        Me.dgvWD.DataSource = Me.WeeklyDiaryBindingSource
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvWD.DefaultCellStyle = DataGridViewCellStyle8
        Me.dgvWD.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvWD.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke
        Me.dgvWD.EnableHeadersVisualStyles = False
        Me.dgvWD.GridColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.dgvWD.Location = New System.Drawing.Point(0, 0)
        Me.dgvWD.MultiSelect = False
        Me.dgvWD.Name = "dgvWD"
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle9.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle9.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvWD.RowHeadersDefaultCellStyle = DataGridViewCellStyle9
        Me.dgvWD.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvWD.RowTemplate.Height = 40
        Me.dgvWD.SelectAllSignVisible = False
        Me.dgvWD.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgvWD.Size = New System.Drawing.Size(862, 625)
        Me.dgvWD.TabIndex = 44
        Me.dgvWD.TabStop = False
        '
        'WeeklyDiaryBindingSource
        '
        Me.WeeklyDiaryBindingSource.DataMember = "WeeklyDiary"
        Me.WeeklyDiaryBindingSource.DataSource = Me.WeeklyDiaryDataSet1
        '
        'WeeklyDiaryDataSet1
        '
        Me.WeeklyDiaryDataSet1.DataSetName = "WeeklyDiaryDataSet"
        Me.WeeklyDiaryDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'dgvSOC
        '
        Me.dgvSOC.AllowUserToAddRows = False
        Me.dgvSOC.AllowUserToDeleteRows = False
        Me.dgvSOC.AutoGenerateColumns = False
        Me.dgvSOC.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgvSOC.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter
        DataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle10.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle10.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvSOC.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle10
        Me.dgvSOC.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSOC.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.SOCNumberDataGridViewTextBoxColumn, Me.SelectRow, Me.DateOfInspectionDataGridViewTextBoxColumn, Me.PoliceStationDataGridViewTextBoxColumn, Me.CrimeNumberDataGridViewTextBoxColumn, Me.PlaceOfOccurrenceDataGridViewTextBoxColumn, Me.InvestigatingOfficer})
        Me.dgvSOC.DataSource = Me.SOCRegisterBindingSource
        DataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft
        DataGridViewCellStyle14.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle14.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle14.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle14.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvSOC.DefaultCellStyle = DataGridViewCellStyle14
        Me.dgvSOC.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvSOC.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke
        Me.dgvSOC.EnableHeadersVisualStyles = False
        Me.dgvSOC.GridColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.dgvSOC.Location = New System.Drawing.Point(0, 0)
        Me.dgvSOC.MultiSelect = False
        Me.dgvSOC.Name = "dgvSOC"
        DataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle15.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle15.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle15.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvSOC.RowHeadersDefaultCellStyle = DataGridViewCellStyle15
        DataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvSOC.RowsDefaultCellStyle = DataGridViewCellStyle16
        Me.dgvSOC.RowTemplate.Height = 40
        Me.dgvSOC.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvSOC.SelectAllSignVisible = False
        Me.dgvSOC.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgvSOC.ShowCellErrors = False
        Me.dgvSOC.Size = New System.Drawing.Size(862, 625)
        Me.dgvSOC.TabIndex = 43
        Me.dgvSOC.TabStop = False
        '
        'SOCNumberDataGridViewTextBoxColumn
        '
        Me.SOCNumberDataGridViewTextBoxColumn.DataPropertyName = "SOCNumber"
        Me.SOCNumberDataGridViewTextBoxColumn.HeaderText = "SOC"
        Me.SOCNumberDataGridViewTextBoxColumn.Name = "SOCNumberDataGridViewTextBoxColumn"
        Me.SOCNumberDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.SOCNumberDataGridViewTextBoxColumn.Width = 80
        '
        'SelectRow
        '
        Me.SelectRow.HeaderText = "Select"
        Me.SelectRow.Name = "SelectRow"
        Me.SelectRow.Width = 65
        '
        'DateOfInspectionDataGridViewTextBoxColumn
        '
        Me.DateOfInspectionDataGridViewTextBoxColumn.DataPropertyName = "DateOfInspection"
        DataGridViewCellStyle11.Format = "dd/MM/yyyy"
        Me.DateOfInspectionDataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle11
        Me.DateOfInspectionDataGridViewTextBoxColumn.HeaderText = "DI"
        Me.DateOfInspectionDataGridViewTextBoxColumn.Name = "DateOfInspectionDataGridViewTextBoxColumn"
        Me.DateOfInspectionDataGridViewTextBoxColumn.ReadOnly = True
        Me.DateOfInspectionDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.DateOfInspectionDataGridViewTextBoxColumn.Width = 90
        '
        'PoliceStationDataGridViewTextBoxColumn
        '
        Me.PoliceStationDataGridViewTextBoxColumn.DataPropertyName = "PoliceStation"
        DataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.PoliceStationDataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle12
        Me.PoliceStationDataGridViewTextBoxColumn.HeaderText = "Police Station"
        Me.PoliceStationDataGridViewTextBoxColumn.Name = "PoliceStationDataGridViewTextBoxColumn"
        Me.PoliceStationDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.PoliceStationDataGridViewTextBoxColumn.Width = 145
        '
        'CrimeNumberDataGridViewTextBoxColumn
        '
        Me.CrimeNumberDataGridViewTextBoxColumn.DataPropertyName = "CrimeNumber"
        Me.CrimeNumberDataGridViewTextBoxColumn.HeaderText = "Cr.No."
        Me.CrimeNumberDataGridViewTextBoxColumn.Name = "CrimeNumberDataGridViewTextBoxColumn"
        Me.CrimeNumberDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.CrimeNumberDataGridViewTextBoxColumn.Width = 90
        '
        'PlaceOfOccurrenceDataGridViewTextBoxColumn
        '
        Me.PlaceOfOccurrenceDataGridViewTextBoxColumn.DataPropertyName = "PlaceOfOccurrence"
        DataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.PlaceOfOccurrenceDataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle13
        Me.PlaceOfOccurrenceDataGridViewTextBoxColumn.HeaderText = "Place of Occurrence"
        Me.PlaceOfOccurrenceDataGridViewTextBoxColumn.Name = "PlaceOfOccurrenceDataGridViewTextBoxColumn"
        Me.PlaceOfOccurrenceDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.PlaceOfOccurrenceDataGridViewTextBoxColumn.Width = 200
        '
        'InvestigatingOfficer
        '
        Me.InvestigatingOfficer.DataPropertyName = "InvestigatingOfficer"
        Me.InvestigatingOfficer.HeaderText = "Inspecting Officer"
        Me.InvestigatingOfficer.Name = "InvestigatingOfficer"
        Me.InvestigatingOfficer.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.InvestigatingOfficer.Width = 195
        '
        'SOCRegisterBindingSource
        '
        Me.SOCRegisterBindingSource.DataMember = "SOCRegister"
        Me.SOCRegisterBindingSource.DataSource = Me.FingerPrintDataSet
        '
        'FingerPrintDataSet
        '
        Me.FingerPrintDataSet.DataSetName = "FingerPrintDataSet"
        Me.FingerPrintDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'StatusBar
        '
        Me.StatusBar.AccessibleDescription = "DotNetBar Bar (StatusBar)"
        Me.StatusBar.AccessibleName = "DotNetBar Bar"
        Me.StatusBar.AccessibleRole = System.Windows.Forms.AccessibleRole.StatusBar
        Me.StatusBar.BarType = DevComponents.DotNetBar.eBarType.StatusBar
        Me.StatusBar.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.StatusBar.DockedBorderStyle = DevComponents.DotNetBar.eBorderType.DoubleLine
        Me.StatusBar.DockSide = DevComponents.DotNetBar.eDockSide.Document
        Me.StatusBar.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.StatusBar.GrabHandleStyle = DevComponents.DotNetBar.eGrabHandleStyle.ResizeHandle
        Me.StatusBar.IsMaximized = False
        Me.StatusBar.Items.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.lblTickedRecords, Me.btnSelectAll, Me.btnDeselectAll, Me.lblOfficerName, Me.lblPEN, Me.lblBasicPay, Me.lblDA})
        Me.StatusBar.ItemSpacing = 5
        Me.StatusBar.Location = New System.Drawing.Point(0, 669)
        Me.StatusBar.MaximumSize = New System.Drawing.Size(0, 30)
        Me.StatusBar.Name = "StatusBar"
        Me.StatusBar.Size = New System.Drawing.Size(862, 30)
        Me.StatusBar.Stretch = True
        Me.StatusBar.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.StatusBar.TabIndex = 42
        Me.StatusBar.TabStop = False
        '
        'lblTickedRecords
        '
        Me.lblTickedRecords.BorderType = DevComponents.DotNetBar.eBorderType.DoubleLine
        Me.lblTickedRecords.Name = "lblTickedRecords"
        Me.lblTickedRecords.Text = "Selected Records"
        Me.lblTickedRecords.Width = 140
        '
        'btnSelectAll
        '
        Me.btnSelectAll.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnSelectAll.Icon = CType(resources.GetObject("btnSelectAll.Icon"), System.Drawing.Icon)
        Me.btnSelectAll.Name = "btnSelectAll"
        Me.btnSelectAll.Text = "Select All"
        '
        'btnDeselectAll
        '
        Me.btnDeselectAll.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnDeselectAll.Icon = CType(resources.GetObject("btnDeselectAll.Icon"), System.Drawing.Icon)
        Me.btnDeselectAll.Name = "btnDeselectAll"
        Me.btnDeselectAll.Text = "Deselect All"
        '
        'lblOfficerName
        '
        Me.lblOfficerName.BorderType = DevComponents.DotNetBar.eBorderType.DoubleLine
        Me.lblOfficerName.Name = "lblOfficerName"
        Me.lblOfficerName.Text = "Officer Name"
        Me.lblOfficerName.Width = 170
        '
        'lblPEN
        '
        Me.lblPEN.BorderType = DevComponents.DotNetBar.eBorderType.DoubleLine
        Me.lblPEN.Name = "lblPEN"
        Me.lblPEN.Text = "PEN No: "
        Me.lblPEN.Width = 130
        '
        'lblBasicPay
        '
        Me.lblBasicPay.BorderType = DevComponents.DotNetBar.eBorderType.DoubleLine
        Me.lblBasicPay.Font = New System.Drawing.Font("Rupee Foradian", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBasicPay.Name = "lblBasicPay"
        Me.lblBasicPay.Text = "Basic Pay: "
        Me.lblBasicPay.Width = 140
        '
        'lblDA
        '
        Me.lblDA.BorderType = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.lblDA.Font = New System.Drawing.Font("Rupee Foradian", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDA.Name = "lblDA"
        Me.lblDA.Text = "DA:"
        Me.lblDA.Width = 100
        '
        'PanelSOC
        '
        Me.PanelSOC.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelSOC.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.PanelSOC.DisabledBackColor = System.Drawing.Color.Empty
        Me.PanelSOC.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelSOC.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PanelSOC.Location = New System.Drawing.Point(0, 0)
        Me.PanelSOC.Name = "PanelSOC"
        Me.PanelSOC.Size = New System.Drawing.Size(862, 44)
        Me.PanelSOC.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelSOC.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelSOC.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelSOC.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelSOC.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelSOC.Style.GradientAngle = 90
        Me.PanelSOC.TabIndex = 40
        '
        'PanelEx2
        '
        Me.PanelEx2.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.PanelEx2.Controls.Add(Me.pnlBackup)
        Me.PanelEx2.Controls.Add(Me.GroupPanel3)
        Me.PanelEx2.Controls.Add(Me.GroupPanel2)
        Me.PanelEx2.Controls.Add(Me.GroupPanel1)
        Me.PanelEx2.Controls.Add(Me.GroupPanel5)
        Me.PanelEx2.Controls.Add(Me.pnlStatus)
        Me.PanelEx2.DisabledBackColor = System.Drawing.Color.Empty
        Me.PanelEx2.Dock = System.Windows.Forms.DockStyle.Left
        Me.PanelEx2.Location = New System.Drawing.Point(0, 0)
        Me.PanelEx2.Name = "PanelEx2"
        Me.PanelEx2.Size = New System.Drawing.Size(430, 699)
        Me.PanelEx2.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx2.Style.GradientAngle = 90
        Me.PanelEx2.TabIndex = 43
        '
        'pnlBackup
        '
        Me.pnlBackup.CanvasColor = System.Drawing.SystemColors.Control
        Me.pnlBackup.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.pnlBackup.Controls.Add(Me.cprgBackup)
        Me.pnlBackup.Controls.Add(Me.btnUploadAllFiles)
        Me.pnlBackup.Controls.Add(Me.lblBackup)
        Me.pnlBackup.DisabledBackColor = System.Drawing.Color.Empty
        Me.pnlBackup.Location = New System.Drawing.Point(8, 536)
        Me.pnlBackup.Name = "pnlBackup"
        Me.pnlBackup.Size = New System.Drawing.Size(412, 66)
        Me.pnlBackup.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.pnlBackup.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.pnlBackup.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.pnlBackup.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.pnlBackup.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.pnlBackup.Style.GradientAngle = 90
        Me.pnlBackup.TabIndex = 51
        '
        'cprgBackup
        '
        '
        '
        '
        Me.cprgBackup.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.cprgBackup.FocusCuesEnabled = False
        Me.cprgBackup.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cprgBackup.Location = New System.Drawing.Point(279, 3)
        Me.cprgBackup.Name = "cprgBackup"
        Me.cprgBackup.ProgressBarType = DevComponents.DotNetBar.eCircularProgressType.Donut
        Me.cprgBackup.ProgressColor = System.Drawing.Color.Red
        Me.cprgBackup.ProgressTextVisible = True
        Me.cprgBackup.Size = New System.Drawing.Size(133, 60)
        Me.cprgBackup.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP
        Me.cprgBackup.TabIndex = 49
        Me.cprgBackup.TabStop = False
        '
        'btnUploadAllFiles
        '
        Me.btnUploadAllFiles.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnUploadAllFiles.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnUploadAllFiles.Image = CType(resources.GetObject("btnUploadAllFiles.Image"), System.Drawing.Image)
        Me.btnUploadAllFiles.Location = New System.Drawing.Point(281, 6)
        Me.btnUploadAllFiles.Name = "btnUploadAllFiles"
        Me.btnUploadAllFiles.Size = New System.Drawing.Size(122, 55)
        Me.btnUploadAllFiles.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnUploadAllFiles.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnUploadSelectedMonthTAFiles})
        Me.btnUploadAllFiles.TabIndex = 14
        Me.btnUploadAllFiles.Text = "Backup"
        '
        'btnUploadSelectedMonthTAFiles
        '
        Me.btnUploadSelectedMonthTAFiles.GlobalItem = False
        Me.btnUploadSelectedMonthTAFiles.Name = "btnUploadSelectedMonthTAFiles"
        Me.btnUploadSelectedMonthTAFiles.Text = "Backup selected month's TA Files"
        '
        'lblBackup
        '
        Me.lblBackup.AutoSize = True
        '
        '
        '
        Me.lblBackup.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblBackup.Location = New System.Drawing.Point(11, 24)
        Me.lblBackup.Name = "lblBackup"
        Me.lblBackup.Size = New System.Drawing.Size(262, 18)
        Me.lblBackup.TabIndex = 46
        Me.lblBackup.Text = "Backup TA Bill and Tour Note to Google Drive"
        '
        'GroupPanel3
        '
        Me.GroupPanel3.BackColor = System.Drawing.Color.Transparent
        Me.GroupPanel3.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanel3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanel3.Controls.Add(Me.cprgBlankForms)
        Me.GroupPanel3.Controls.Add(Me.btnGenerateBlankTR56A)
        Me.GroupPanel3.Controls.Add(Me.btnGenerateBlankTourNote)
        Me.GroupPanel3.Controls.Add(Me.btnGenerateBlankTR47)
        Me.GroupPanel3.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel3.Location = New System.Drawing.Point(8, 431)
        Me.GroupPanel3.Name = "GroupPanel3"
        Me.GroupPanel3.Size = New System.Drawing.Size(412, 93)
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
        Me.GroupPanel3.TabIndex = 50
        Me.GroupPanel3.Text = "Blank Forms"
        '
        'cprgBlankForms
        '
        '
        '
        '
        Me.cprgBlankForms.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.cprgBlankForms.FocusCuesEnabled = False
        Me.cprgBlankForms.Location = New System.Drawing.Point(3, 3)
        Me.cprgBlankForms.Name = "cprgBlankForms"
        Me.cprgBlankForms.ProgressColor = System.Drawing.Color.Red
        Me.cprgBlankForms.ProgressTextVisible = True
        Me.cprgBlankForms.Size = New System.Drawing.Size(400, 64)
        Me.cprgBlankForms.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP
        Me.cprgBlankForms.TabIndex = 48
        Me.cprgBlankForms.TabStop = False
        '
        'btnGenerateBlankTR56A
        '
        Me.btnGenerateBlankTR56A.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnGenerateBlankTR56A.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnGenerateBlankTR56A.Location = New System.Drawing.Point(281, 7)
        Me.btnGenerateBlankTR56A.Name = "btnGenerateBlankTR56A"
        Me.btnGenerateBlankTR56A.Size = New System.Drawing.Size(122, 55)
        Me.btnGenerateBlankTR56A.TabIndex = 13
        Me.btnGenerateBlankTR56A.Text = "Blank TR 56A"
        '
        'btnGenerateBlankTourNote
        '
        Me.btnGenerateBlankTourNote.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnGenerateBlankTourNote.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnGenerateBlankTourNote.Location = New System.Drawing.Point(5, 7)
        Me.btnGenerateBlankTourNote.Name = "btnGenerateBlankTourNote"
        Me.btnGenerateBlankTourNote.Size = New System.Drawing.Size(122, 55)
        Me.btnGenerateBlankTourNote.TabIndex = 11
        Me.btnGenerateBlankTourNote.Text = "Blank Tour Note"
        '
        'btnGenerateBlankTR47
        '
        Me.btnGenerateBlankTR47.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnGenerateBlankTR47.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnGenerateBlankTR47.Location = New System.Drawing.Point(143, 7)
        Me.btnGenerateBlankTR47.Name = "btnGenerateBlankTR47"
        Me.btnGenerateBlankTR47.Size = New System.Drawing.Size(122, 55)
        Me.btnGenerateBlankTR47.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnGenerateBlankTR47Outer})
        Me.btnGenerateBlankTR47.TabIndex = 12
        Me.btnGenerateBlankTR47.Text = "Blank TR 47"
        '
        'btnGenerateBlankTR47Outer
        '
        Me.btnGenerateBlankTR47Outer.GlobalItem = False
        Me.btnGenerateBlankTR47Outer.Name = "btnGenerateBlankTR47Outer"
        Me.btnGenerateBlankTR47Outer.Text = "Blank TR 47 Outer"
        '
        'GroupPanel2
        '
        Me.GroupPanel2.BackColor = System.Drawing.Color.Transparent
        Me.GroupPanel2.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanel2.Controls.Add(Me.cprgGenerateFiles)
        Me.GroupPanel2.Controls.Add(Me.PanelEx4)
        Me.GroupPanel2.Controls.Add(Me.btnOpenTABillFolder)
        Me.GroupPanel2.Controls.Add(Me.btnGenerateTourNote)
        Me.GroupPanel2.Controls.Add(Me.btnGenerateTABill)
        Me.GroupPanel2.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel2.Location = New System.Drawing.Point(8, 252)
        Me.GroupPanel2.Name = "GroupPanel2"
        Me.GroupPanel2.Size = New System.Drawing.Size(412, 169)
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
        Me.GroupPanel2.TabIndex = 49
        Me.GroupPanel2.Text = "Step 3 : Generate Tour Note"
        '
        'cprgGenerateFiles
        '
        '
        '
        '
        Me.cprgGenerateFiles.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.cprgGenerateFiles.FocusCuesEnabled = False
        Me.cprgGenerateFiles.Location = New System.Drawing.Point(3, 75)
        Me.cprgGenerateFiles.Name = "cprgGenerateFiles"
        Me.cprgGenerateFiles.ProgressColor = System.Drawing.Color.Red
        Me.cprgGenerateFiles.ProgressTextVisible = True
        Me.cprgGenerateFiles.Size = New System.Drawing.Size(400, 64)
        Me.cprgGenerateFiles.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP
        Me.cprgGenerateFiles.TabIndex = 47
        Me.cprgGenerateFiles.TabStop = False
        '
        'PanelEx4
        '
        Me.PanelEx4.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.PanelEx4.Controls.Add(Me.LabelX7)
        Me.PanelEx4.Controls.Add(Me.chkThreeRows)
        Me.PanelEx4.Controls.Add(Me.chkSingleRow)
        Me.PanelEx4.DisabledBackColor = System.Drawing.Color.Empty
        Me.PanelEx4.Location = New System.Drawing.Point(5, 3)
        Me.PanelEx4.Name = "PanelEx4"
        Me.PanelEx4.Size = New System.Drawing.Size(398, 64)
        Me.PanelEx4.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx4.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx4.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx4.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx4.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx4.Style.GradientAngle = 90
        Me.PanelEx4.TabIndex = 47
        '
        'LabelX7
        '
        Me.LabelX7.AutoSize = True
        '
        '
        '
        Me.LabelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX7.Location = New System.Drawing.Point(11, 21)
        Me.LabelX7.Name = "LabelX7"
        Me.LabelX7.Size = New System.Drawing.Size(104, 18)
        Me.LabelX7.TabIndex = 44
        Me.LabelX7.Text = "Tour Note Format"
        '
        'chkThreeRows
        '
        Me.chkThreeRows.AutoSize = True
        '
        '
        '
        Me.chkThreeRows.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.chkThreeRows.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton
        Me.chkThreeRows.Location = New System.Drawing.Point(128, 34)
        Me.chkThreeRows.Name = "chkThreeRows"
        Me.chkThreeRows.Size = New System.Drawing.Size(152, 18)
        Me.chkThreeRows.TabIndex = 9
        Me.chkThreeRows.TabStop = False
        Me.chkThreeRows.Text = "Split Journey with Halt"
        '
        'chkSingleRow
        '
        Me.chkSingleRow.AutoSize = True
        '
        '
        '
        Me.chkSingleRow.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.chkSingleRow.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton
        Me.chkSingleRow.Checked = True
        Me.chkSingleRow.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkSingleRow.CheckValue = "Y"
        Me.chkSingleRow.Location = New System.Drawing.Point(128, 6)
        Me.chkSingleRow.Name = "chkSingleRow"
        Me.chkSingleRow.Size = New System.Drawing.Size(84, 18)
        Me.chkSingleRow.TabIndex = 8
        Me.chkSingleRow.TabStop = False
        Me.chkSingleRow.Text = "Single Line"
        '
        'btnOpenTABillFolder
        '
        Me.btnOpenTABillFolder.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnOpenTABillFolder.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnOpenTABillFolder.Location = New System.Drawing.Point(281, 78)
        Me.btnOpenTABillFolder.Name = "btnOpenTABillFolder"
        Me.btnOpenTABillFolder.Size = New System.Drawing.Size(122, 55)
        Me.btnOpenTABillFolder.TabIndex = 10
        Me.btnOpenTABillFolder.Text = "Open TA Folder"
        '
        'btnGenerateTourNote
        '
        Me.btnGenerateTourNote.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnGenerateTourNote.BackColor = System.Drawing.Color.Transparent
        Me.btnGenerateTourNote.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnGenerateTourNote.Location = New System.Drawing.Point(5, 78)
        Me.btnGenerateTourNote.Name = "btnGenerateTourNote"
        Me.btnGenerateTourNote.Size = New System.Drawing.Size(122, 55)
        Me.btnGenerateTourNote.TabIndex = 8
        Me.btnGenerateTourNote.Text = "Generate Tour Note"
        Me.btnGenerateTourNote.TextColor = System.Drawing.Color.Red
        '
        'btnGenerateTABill
        '
        Me.btnGenerateTABill.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnGenerateTABill.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnGenerateTABill.Location = New System.Drawing.Point(143, 78)
        Me.btnGenerateTABill.Name = "btnGenerateTABill"
        Me.btnGenerateTABill.Size = New System.Drawing.Size(122, 55)
        Me.btnGenerateTABill.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnGenerateTABillOuter})
        Me.btnGenerateTABill.TabIndex = 9
        Me.btnGenerateTABill.Text = "Generate TA Bill"
        Me.btnGenerateTABill.TextColor = System.Drawing.Color.Red
        '
        'btnGenerateTABillOuter
        '
        Me.btnGenerateTABillOuter.GlobalItem = False
        Me.btnGenerateTABillOuter.Name = "btnGenerateTABillOuter"
        Me.btnGenerateTABillOuter.Text = "Generate TA Bill Outer"
        '
        'GroupPanel1
        '
        Me.GroupPanel1.BackColor = System.Drawing.Color.Transparent
        Me.GroupPanel1.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanel1.Controls.Add(Me.LabelX3)
        Me.GroupPanel1.Controls.Add(Me.LabelX4)
        Me.GroupPanel1.Controls.Add(Me.cmbSOCOfficer)
        Me.GroupPanel1.Controls.Add(Me.txtYear)
        Me.GroupPanel1.Controls.Add(Me.cmbMonth)
        Me.GroupPanel1.Controls.Add(Me.LabelX1)
        Me.GroupPanel1.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel1.Location = New System.Drawing.Point(8, 135)
        Me.GroupPanel1.Name = "GroupPanel1"
        Me.GroupPanel1.Size = New System.Drawing.Size(412, 106)
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
        Me.GroupPanel1.TabIndex = 48
        Me.GroupPanel1.Text = "Step 2 : Generate Records"
        '
        'LabelX3
        '
        Me.LabelX3.AutoSize = True
        '
        '
        '
        Me.LabelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX3.Location = New System.Drawing.Point(5, 14)
        Me.LabelX3.Name = "LabelX3"
        Me.LabelX3.Size = New System.Drawing.Size(40, 18)
        Me.LabelX3.TabIndex = 34
        Me.LabelX3.Text = "Month"
        '
        'LabelX4
        '
        Me.LabelX4.AutoSize = True
        '
        '
        '
        Me.LabelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX4.Location = New System.Drawing.Point(292, 14)
        Me.LabelX4.Name = "LabelX4"
        Me.LabelX4.Size = New System.Drawing.Size(28, 18)
        Me.LabelX4.TabIndex = 35
        Me.LabelX4.Text = "Year"
        '
        'cmbSOCOfficer
        '
        Me.cmbSOCOfficer.DisplayMember = "Text"
        Me.cmbSOCOfficer.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbSOCOfficer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSOCOfficer.FocusHighlightColor = System.Drawing.Color.LightCoral
        Me.cmbSOCOfficer.FocusHighlightEnabled = True
        Me.cmbSOCOfficer.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbSOCOfficer.ForeColor = System.Drawing.Color.Black
        Me.cmbSOCOfficer.FormattingEnabled = True
        Me.cmbSOCOfficer.ItemHeight = 23
        Me.cmbSOCOfficer.Location = New System.Drawing.Point(52, 46)
        Me.cmbSOCOfficer.MaxDropDownItems = 15
        Me.cmbSOCOfficer.MaxLength = 255
        Me.cmbSOCOfficer.Name = "cmbSOCOfficer"
        Me.cmbSOCOfficer.Size = New System.Drawing.Size(351, 29)
        Me.cmbSOCOfficer.TabIndex = 7
        Me.cmbSOCOfficer.WatermarkText = "Select Officer Name"
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
        Me.txtYear.Location = New System.Drawing.Point(326, 9)
        Me.txtYear.MaxValue = 2099
        Me.txtYear.MinValue = 2000
        Me.txtYear.Name = "txtYear"
        Me.txtYear.ShowUpDown = True
        Me.txtYear.Size = New System.Drawing.Size(77, 29)
        Me.txtYear.TabIndex = 6
        Me.txtYear.Value = 2000
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
        Me.cmbMonth.Location = New System.Drawing.Point(52, 9)
        Me.cmbMonth.MaxDropDownItems = 15
        Me.cmbMonth.MaxLength = 255
        Me.cmbMonth.Name = "cmbMonth"
        Me.cmbMonth.Size = New System.Drawing.Size(127, 29)
        Me.cmbMonth.TabIndex = 5
        Me.cmbMonth.WatermarkText = "Month"
        '
        'LabelX1
        '
        Me.LabelX1.AutoSize = True
        '
        '
        '
        Me.LabelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX1.Location = New System.Drawing.Point(5, 51)
        Me.LabelX1.Name = "LabelX1"
        Me.LabelX1.Size = New System.Drawing.Size(41, 18)
        Me.LabelX1.TabIndex = 0
        Me.LabelX1.Text = "Officer"
        '
        'GroupPanel5
        '
        Me.GroupPanel5.BackColor = System.Drawing.Color.Transparent
        Me.GroupPanel5.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanel5.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanel5.Controls.Add(Me.txtDVNumber)
        Me.GroupPanel5.Controls.Add(Me.LabelX6)
        Me.GroupPanel5.Controls.Add(Me.LabelX5)
        Me.GroupPanel5.Controls.Add(Me.LabelX2)
        Me.GroupPanel5.Controls.Add(Me.chkUsePS)
        Me.GroupPanel5.Controls.Add(Me.chkUsePO)
        Me.GroupPanel5.Controls.Add(Me.txtStartingLocation)
        Me.GroupPanel5.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel5.Location = New System.Drawing.Point(8, 7)
        Me.GroupPanel5.Name = "GroupPanel5"
        Me.GroupPanel5.Size = New System.Drawing.Size(412, 118)
        '
        '
        '
        Me.GroupPanel5.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.GroupPanel5.Style.BackColorGradientAngle = 90
        Me.GroupPanel5.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.GroupPanel5.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel5.Style.BorderBottomWidth = 1
        Me.GroupPanel5.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.GroupPanel5.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel5.Style.BorderLeftWidth = 1
        Me.GroupPanel5.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel5.Style.BorderRightWidth = 1
        Me.GroupPanel5.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel5.Style.BorderTopWidth = 1
        Me.GroupPanel5.Style.CornerDiameter = 4
        Me.GroupPanel5.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.GroupPanel5.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.GroupPanel5.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.GroupPanel5.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.GroupPanel5.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.GroupPanel5.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.GroupPanel5.TabIndex = 47
        Me.GroupPanel5.Text = "Step 1 : Places Visited"
        '
        'txtDVNumber
        '
        Me.txtDVNumber.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtDVNumber.Border.Class = "TextBoxBorder"
        Me.txtDVNumber.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtDVNumber.ButtonCustom.Image = CType(resources.GetObject("txtDVNumber.ButtonCustom.Image"), System.Drawing.Image)
        Me.txtDVNumber.DisabledBackColor = System.Drawing.Color.White
        Me.txtDVNumber.FocusHighlightEnabled = True
        Me.txtDVNumber.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDVNumber.ForeColor = System.Drawing.Color.Black
        Me.txtDVNumber.Location = New System.Drawing.Point(78, 62)
        Me.txtDVNumber.MaxLength = 45
        Me.txtDVNumber.Name = "txtDVNumber"
        Me.txtDVNumber.Size = New System.Drawing.Size(325, 29)
        Me.txtDVNumber.TabIndex = 4
        Me.txtDVNumber.WatermarkText = "Department Vehicle Number"
        '
        'LabelX6
        '
        Me.LabelX6.AutoSize = True
        '
        '
        '
        Me.LabelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX6.Location = New System.Drawing.Point(5, 67)
        Me.LabelX6.Name = "LabelX6"
        Me.LabelX6.Size = New System.Drawing.Size(67, 18)
        Me.LabelX6.TabIndex = 42
        Me.LabelX6.Text = "Vehicle No."
        '
        'LabelX5
        '
        Me.LabelX5.AutoSize = True
        '
        '
        '
        Me.LabelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX5.Location = New System.Drawing.Point(5, 40)
        Me.LabelX5.Name = "LabelX5"
        Me.LabelX5.Size = New System.Drawing.Size(17, 18)
        Me.LabelX5.TabIndex = 41
        Me.LabelX5.Text = "To"
        '
        'LabelX2
        '
        Me.LabelX2.AutoSize = True
        '
        '
        '
        Me.LabelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX2.Location = New System.Drawing.Point(5, 10)
        Me.LabelX2.Name = "LabelX2"
        Me.LabelX2.Size = New System.Drawing.Size(32, 18)
        Me.LabelX2.TabIndex = 38
        Me.LabelX2.Text = "From"
        '
        'chkUsePS
        '
        Me.chkUsePS.AutoSize = True
        '
        '
        '
        Me.chkUsePS.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.chkUsePS.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton
        Me.chkUsePS.Location = New System.Drawing.Point(279, 40)
        Me.chkUsePS.Name = "chkUsePS"
        Me.chkUsePS.Size = New System.Drawing.Size(124, 18)
        Me.chkUsePS.TabIndex = 3
        Me.chkUsePS.TabStop = False
        Me.chkUsePS.Text = "Use Station Name"
        '
        'chkUsePO
        '
        Me.chkUsePO.AutoSize = True
        '
        '
        '
        Me.chkUsePO.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.chkUsePO.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton
        Me.chkUsePO.Checked = True
        Me.chkUsePO.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkUsePO.CheckValue = "Y"
        Me.chkUsePO.Location = New System.Drawing.Point(78, 40)
        Me.chkUsePO.Name = "chkUsePO"
        Me.chkUsePO.Size = New System.Drawing.Size(158, 18)
        Me.chkUsePO.TabIndex = 2
        Me.chkUsePO.TabStop = False
        Me.chkUsePO.Text = "Use Place of Occurrence"
        '
        'txtStartingLocation
        '
        Me.txtStartingLocation.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtStartingLocation.Border.Class = "TextBoxBorder"
        Me.txtStartingLocation.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtStartingLocation.ButtonCustom.Image = CType(resources.GetObject("txtStartingLocation.ButtonCustom.Image"), System.Drawing.Image)
        Me.txtStartingLocation.DisabledBackColor = System.Drawing.Color.White
        Me.txtStartingLocation.FocusHighlightEnabled = True
        Me.txtStartingLocation.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStartingLocation.ForeColor = System.Drawing.Color.Black
        Me.txtStartingLocation.Location = New System.Drawing.Point(78, 5)
        Me.txtStartingLocation.MaxLength = 45
        Me.txtStartingLocation.Name = "txtStartingLocation"
        Me.txtStartingLocation.Size = New System.Drawing.Size(325, 29)
        Me.txtStartingLocation.TabIndex = 1
        Me.txtStartingLocation.WatermarkText = "From"
        '
        'pnlStatus
        '
        Me.pnlStatus.CanvasColor = System.Drawing.SystemColors.Control
        Me.pnlStatus.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.pnlStatus.Controls.Add(Me.lblSavedTABill)
        Me.pnlStatus.Controls.Add(Me.lblSavedTourNote)
        Me.pnlStatus.DisabledBackColor = System.Drawing.Color.Empty
        Me.pnlStatus.Location = New System.Drawing.Point(8, 614)
        Me.pnlStatus.Name = "pnlStatus"
        Me.pnlStatus.Size = New System.Drawing.Size(412, 57)
        Me.pnlStatus.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.pnlStatus.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.pnlStatus.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.pnlStatus.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.pnlStatus.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.pnlStatus.Style.GradientAngle = 90
        Me.pnlStatus.TabIndex = 52
        '
        'lblSavedTABill
        '
        Me.lblSavedTABill.AutoSize = True
        '
        '
        '
        Me.lblSavedTABill.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblSavedTABill.Location = New System.Drawing.Point(11, 31)
        Me.lblSavedTABill.Name = "lblSavedTABill"
        Me.lblSavedTABill.Size = New System.Drawing.Size(76, 18)
        Me.lblSavedTABill.TabIndex = 47
        Me.lblSavedTABill.Text = "Saved TA Bill"
        '
        'lblSavedTourNote
        '
        Me.lblSavedTourNote.AutoSize = True
        '
        '
        '
        Me.lblSavedTourNote.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblSavedTourNote.Location = New System.Drawing.Point(11, 7)
        Me.lblSavedTourNote.Name = "lblSavedTourNote"
        Me.lblSavedTourNote.Size = New System.Drawing.Size(97, 18)
        Me.lblSavedTourNote.TabIndex = 46
        Me.lblSavedTourNote.Text = "Saved Tour Note"
        '
        'SocRegisterTableAdapter1
        '
        Me.SocRegisterTableAdapter1.ClearBeforeFill = True
        '
        'PoliceStationListTableAdapter1
        '
        Me.PoliceStationListTableAdapter1.ClearBeforeFill = True
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        '
        'bgwBlankForms
        '
        Me.bgwBlankForms.WorkerReportsProgress = True
        Me.bgwBlankForms.WorkerSupportsCancellation = True
        '
        'bgwSingleTN
        '
        Me.bgwSingleTN.WorkerReportsProgress = True
        Me.bgwSingleTN.WorkerSupportsCancellation = True
        '
        'bgwThreeTN
        '
        Me.bgwThreeTN.WorkerReportsProgress = True
        Me.bgwThreeTN.WorkerSupportsCancellation = True
        '
        'bgwTR56
        '
        Me.bgwTR56.WorkerReportsProgress = True
        Me.bgwTR56.WorkerSupportsCancellation = True
        '
        'bgwTR56ThreeLine
        '
        Me.bgwTR56ThreeLine.WorkerReportsProgress = True
        Me.bgwTR56ThreeLine.WorkerSupportsCancellation = True
        '
        'bgwTR47
        '
        Me.bgwTR47.WorkerReportsProgress = True
        Me.bgwTR47.WorkerSupportsCancellation = True
        '
        'bgwTR47ThreeLine
        '
        Me.bgwTR47ThreeLine.WorkerReportsProgress = True
        Me.bgwTR47ThreeLine.WorkerSupportsCancellation = True
        '
        'bgwUploadFile
        '
        Me.bgwUploadFile.WorkerReportsProgress = True
        Me.bgwUploadFile.WorkerSupportsCancellation = True
        '
        'bgwUploadAllFiles
        '
        Me.bgwUploadAllFiles.WorkerReportsProgress = True
        Me.bgwUploadAllFiles.WorkerSupportsCancellation = True
        '
        'CommonSettingsTableAdapter1
        '
        Me.CommonSettingsTableAdapter1.ClearBeforeFill = True
        '
        'WeeklyDiaryTableAdapter1
        '
        Me.WeeklyDiaryTableAdapter1.ClearBeforeFill = True
        '
        'IDDataGridViewTextBoxColumn1
        '
        Me.IDDataGridViewTextBoxColumn1.DataPropertyName = "ID"
        Me.IDDataGridViewTextBoxColumn1.HeaderText = "ID"
        Me.IDDataGridViewTextBoxColumn1.Name = "IDDataGridViewTextBoxColumn1"
        Me.IDDataGridViewTextBoxColumn1.Visible = False
        Me.IDDataGridViewTextBoxColumn1.Width = 50
        '
        'DiaryDateDataGridViewTextBoxColumn
        '
        Me.DiaryDateDataGridViewTextBoxColumn.DataPropertyName = "DiaryDate"
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter
        DataGridViewCellStyle2.Format = "dd/MM/yyyy"
        Me.DiaryDateDataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle2
        Me.DiaryDateDataGridViewTextBoxColumn.HeaderText = "Date"
        Me.DiaryDateDataGridViewTextBoxColumn.Name = "DiaryDateDataGridViewTextBoxColumn"
        Me.DiaryDateDataGridViewTextBoxColumn.ReadOnly = True
        Me.DiaryDateDataGridViewTextBoxColumn.Width = 80
        '
        'WorkDoneDataGridViewTextBoxColumn
        '
        Me.WorkDoneDataGridViewTextBoxColumn.DataPropertyName = "WorkDone"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.WorkDoneDataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle3
        Me.WorkDoneDataGridViewTextBoxColumn.HeaderText = "Work Done"
        Me.WorkDoneDataGridViewTextBoxColumn.Name = "WorkDoneDataGridViewTextBoxColumn"
        Me.WorkDoneDataGridViewTextBoxColumn.Width = 200
        '
        'RemarksDataGridViewTextBoxColumn1
        '
        Me.RemarksDataGridViewTextBoxColumn1.DataPropertyName = "Remarks"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.RemarksDataGridViewTextBoxColumn1.DefaultCellStyle = DataGridViewCellStyle4
        Me.RemarksDataGridViewTextBoxColumn1.HeaderText = "Remarks"
        Me.RemarksDataGridViewTextBoxColumn1.Name = "RemarksDataGridViewTextBoxColumn1"
        Me.RemarksDataGridViewTextBoxColumn1.Visible = False
        Me.RemarksDataGridViewTextBoxColumn1.Width = 200
        '
        'SelectRecord
        '
        Me.SelectRecord.HeaderText = "Select"
        Me.SelectRecord.Name = "SelectRecord"
        Me.SelectRecord.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.SelectRecord.Width = 65
        '
        'TourFrom
        '
        Me.TourFrom.DataPropertyName = "TourFrom"
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft
        Me.TourFrom.DefaultCellStyle = DataGridViewCellStyle5
        Me.TourFrom.HeaderText = "Tour From"
        Me.TourFrom.Name = "TourFrom"
        Me.TourFrom.Width = 110
        '
        'TourTo
        '
        Me.TourTo.DataPropertyName = "TourTo"
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft
        Me.TourTo.DefaultCellStyle = DataGridViewCellStyle6
        Me.TourTo.HeaderText = "Tour To"
        Me.TourTo.Name = "TourTo"
        Me.TourTo.Width = 140
        '
        'TourPurpose
        '
        Me.TourPurpose.DataPropertyName = "TourPurpose"
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft
        Me.TourPurpose.DefaultCellStyle = DataGridViewCellStyle7
        Me.TourPurpose.HeaderText = "Tour Purpose"
        Me.TourPurpose.Name = "TourPurpose"
        Me.TourPurpose.Width = 270
        '
        'FrmTourNote
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(1292, 699)
        Me.Controls.Add(Me.PanelEx1)
        Me.DoubleBuffered = True
        Me.EnableGlass = False
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FrmTourNote"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Tour Note/TA Bill Generator"
        Me.TitleText = "<b>Tour Note/TA Bill Generator</b>"
        Me.PanelEx1.ResumeLayout(False)
        Me.PanelEx3.ResumeLayout(False)
        Me.PanelEx6.ResumeLayout(False)
        CType(Me.dgvWD, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.WeeklyDiaryBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.WeeklyDiaryDataSet1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvSOC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SOCRegisterBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FingerPrintDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelEx2.ResumeLayout(False)
        Me.pnlBackup.ResumeLayout(False)
        Me.pnlBackup.PerformLayout()
        Me.GroupPanel3.ResumeLayout(False)
        Me.GroupPanel2.ResumeLayout(False)
        Me.PanelEx4.ResumeLayout(False)
        Me.PanelEx4.PerformLayout()
        Me.GroupPanel1.ResumeLayout(False)
        Me.GroupPanel1.PerformLayout()
        CType(Me.txtYear, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupPanel5.ResumeLayout(False)
        Me.GroupPanel5.PerformLayout()
        Me.pnlStatus.ResumeLayout(False)
        Me.pnlStatus.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PanelEx1 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents LabelX1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents btnGenerateTourNote As DevComponents.DotNetBar.ButtonX
    Friend WithEvents LabelX4 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX3 As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtYear As DevComponents.Editors.IntegerInput
    Friend WithEvents cmbMonth As DevComponents.DotNetBar.Controls.ComboBoxEx
    Friend WithEvents cmbSOCOfficer As DevComponents.DotNetBar.Controls.ComboBoxEx
    Friend WithEvents FingerPrintDataSet As FingerprintInformationSystem.FingerPrintDataSet
    Friend WithEvents SocRegisterTableAdapter1 As FingerprintInformationSystem.FingerPrintDataSetTableAdapters.SOCRegisterTableAdapter
    Friend WithEvents LabelX2 As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtStartingLocation As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents chkUsePO As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents chkUsePS As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents LabelX5 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX7 As DevComponents.DotNetBar.LabelX
    Friend WithEvents chkSingleRow As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents chkThreeRows As DevComponents.DotNetBar.Controls.CheckBoxX
    ' Friend WithEvents btnGenerateBlankTourNote As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents PoliceStationListTableAdapter1 As FingerprintInformationSystem.FingerPrintDataSetTableAdapters.PoliceStationListTableAdapter
    Friend WithEvents PanelEx2 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents PanelEx3 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents SOCRegisterBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents PanelSOC As DevComponents.DotNetBar.PanelEx
    Friend WithEvents btnGenerateBlankTourNote As DevComponents.DotNetBar.ButtonX
    Friend WithEvents StatusBar As DevComponents.DotNetBar.Bar
    Friend WithEvents lblTickedRecords As DevComponents.DotNetBar.LabelItem
    Friend WithEvents dgvSOC As DevComponents.DotNetBar.Controls.DataGridViewX
    Friend WithEvents btnSelectAll As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnDeselectAll As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents lblOfficerName As DevComponents.DotNetBar.LabelItem
    Friend WithEvents lblPEN As DevComponents.DotNetBar.LabelItem
    Friend WithEvents lblBasicPay As DevComponents.DotNetBar.LabelItem
    Friend WithEvents btnGenerateTABill As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnOpenTABillFolder As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnGenerateBlankTR56A As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnGenerateBlankTR47 As DevComponents.DotNetBar.ButtonX
    Friend WithEvents PanelEx4 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents lblSavedTourNote As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblSavedTABill As DevComponents.DotNetBar.LabelX
    Friend WithEvents pnlStatus As DevComponents.DotNetBar.PanelEx
    Friend WithEvents lblDA As DevComponents.DotNetBar.LabelItem
    Friend WithEvents btnGenerateTABillOuter As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents GroupPanel3 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents GroupPanel2 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents GroupPanel1 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents GroupPanel5 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents cprgGenerateFiles As DevComponents.DotNetBar.Controls.CircularProgress
    Friend WithEvents bgwBlankForms As System.ComponentModel.BackgroundWorker
    Friend WithEvents bgwSingleTN As System.ComponentModel.BackgroundWorker
    Friend WithEvents bgwThreeTN As System.ComponentModel.BackgroundWorker
    Friend WithEvents cprgBlankForms As DevComponents.DotNetBar.Controls.CircularProgress
    Friend WithEvents bgwTR56 As System.ComponentModel.BackgroundWorker
    Friend WithEvents bgwTR56ThreeLine As System.ComponentModel.BackgroundWorker
    Friend WithEvents bgwTR47 As System.ComponentModel.BackgroundWorker
    Friend WithEvents bgwTR47ThreeLine As System.ComponentModel.BackgroundWorker
    Friend WithEvents btnGenerateBlankTR47Outer As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents pnlBackup As DevComponents.DotNetBar.PanelEx
    Friend WithEvents btnUploadAllFiles As DevComponents.DotNetBar.ButtonX
    Friend WithEvents lblBackup As DevComponents.DotNetBar.LabelX
    Friend WithEvents cprgBackup As DevComponents.DotNetBar.Controls.CircularProgress
    Friend WithEvents bgwUploadFile As System.ComponentModel.BackgroundWorker
    Friend WithEvents btnUploadSelectedMonthTAFiles As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents bgwUploadAllFiles As System.ComponentModel.BackgroundWorker
    Friend WithEvents txtDVNumber As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX6 As DevComponents.DotNetBar.LabelX
    Friend WithEvents CommonSettingsTableAdapter1 As FingerprintInformationSystem.FingerPrintDataSetTableAdapters.CommonSettingsTableAdapter
    Friend WithEvents PanelEx6 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents WeeklyDiaryTableAdapter1 As FingerprintInformationSystem.WeeklyDiaryDataSetTableAdapters.WeeklyDiaryTableAdapter
    Friend WithEvents WeeklyDiaryDataSet1 As FingerprintInformationSystem.WeeklyDiaryDataSet
    Friend WithEvents WeeklyDiaryBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents dgvWD As DevComponents.DotNetBar.Controls.DataGridViewX
    Friend WithEvents SOCNumberDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SelectRow As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents DateOfInspectionDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PoliceStationDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CrimeNumberDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PlaceOfOccurrenceDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents InvestigatingOfficer As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IDDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DiaryDateDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents WorkDoneDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RemarksDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SelectRecord As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents TourFrom As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TourTo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TourPurpose As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
