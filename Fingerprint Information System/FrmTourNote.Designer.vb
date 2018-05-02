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
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmTourNote))
        Me.PanelEx1 = New DevComponents.DotNetBar.PanelEx()
        Me.PanelEx3 = New DevComponents.DotNetBar.PanelEx()
        Me.SOCDatagrid = New DevComponents.DotNetBar.Controls.DataGridViewX()
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
        Me.GroupPanel3 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.btnGenerateBlankTR56A = New DevComponents.DotNetBar.ButtonX()
        Me.btnGenerateBlankTourNote = New DevComponents.DotNetBar.ButtonX()
        Me.btnGenerateBlankTR47 = New DevComponents.DotNetBar.ButtonX()
        Me.GroupPanel2 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.PanelEx4 = New DevComponents.DotNetBar.PanelEx()
        Me.chkUseSavedTourNote = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.LabelX7 = New DevComponents.DotNetBar.LabelX()
        Me.chkThreeRows = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.chkSingleRow = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.btnOpenTABillFolder = New DevComponents.DotNetBar.ButtonX()
        Me.btnShowTourNote = New DevComponents.DotNetBar.ButtonX()
        Me.btnTABill = New DevComponents.DotNetBar.ButtonX()
        Me.btnShowTABillOuter = New DevComponents.DotNetBar.ButtonItem()
        Me.GroupPanel1 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.LabelX3 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX4 = New DevComponents.DotNetBar.LabelX()
        Me.cmbSOCOfficer = New DevComponents.DotNetBar.Controls.ComboBoxEx()
        Me.txtYear = New DevComponents.Editors.IntegerInput()
        Me.cmbMonth = New DevComponents.DotNetBar.Controls.ComboBoxEx()
        Me.LabelX1 = New DevComponents.DotNetBar.LabelX()
        Me.GroupPanel5 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.LabelX5 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX2 = New DevComponents.DotNetBar.LabelX()
        Me.chkUsePS = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.chkUsePO = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.txtStartingLocation = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.PanelEx5 = New DevComponents.DotNetBar.PanelEx()
        Me.lblSavedTABill = New DevComponents.DotNetBar.LabelX()
        Me.lblSavedTourNote = New DevComponents.DotNetBar.LabelX()
        Me.SocRegisterTableAdapter1 = New FingerprintInformationSystem.FingerPrintDataSetTableAdapters.SOCRegisterTableAdapter()
        Me.PoliceStationListTableAdapter1 = New FingerprintInformationSystem.FingerPrintDataSetTableAdapters.PoliceStationListTableAdapter()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.PanelEx1.SuspendLayout()
        Me.PanelEx3.SuspendLayout()
        CType(Me.SOCDatagrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SOCRegisterBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FingerPrintDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelEx2.SuspendLayout()
        Me.GroupPanel3.SuspendLayout()
        Me.GroupPanel2.SuspendLayout()
        Me.PanelEx4.SuspendLayout()
        Me.GroupPanel1.SuspendLayout()
        CType(Me.txtYear, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupPanel5.SuspendLayout()
        Me.PanelEx5.SuspendLayout()
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
        Me.PanelEx1.Size = New System.Drawing.Size(1292, 733)
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
        Me.PanelEx3.Controls.Add(Me.SOCDatagrid)
        Me.PanelEx3.Controls.Add(Me.StatusBar)
        Me.PanelEx3.Controls.Add(Me.PanelSOC)
        Me.PanelEx3.DisabledBackColor = System.Drawing.Color.Empty
        Me.PanelEx3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelEx3.Location = New System.Drawing.Point(404, 0)
        Me.PanelEx3.Name = "PanelEx3"
        Me.PanelEx3.Size = New System.Drawing.Size(888, 733)
        Me.PanelEx3.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx3.Style.GradientAngle = 90
        Me.PanelEx3.TabIndex = 44
        '
        'SOCDatagrid
        '
        Me.SOCDatagrid.AllowUserToAddRows = False
        Me.SOCDatagrid.AllowUserToDeleteRows = False
        Me.SOCDatagrid.AutoGenerateColumns = False
        Me.SOCDatagrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.SOCDatagrid.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.SOCDatagrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.SOCDatagrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.SOCNumberDataGridViewTextBoxColumn, Me.SelectRow, Me.DateOfInspectionDataGridViewTextBoxColumn, Me.PoliceStationDataGridViewTextBoxColumn, Me.CrimeNumberDataGridViewTextBoxColumn, Me.PlaceOfOccurrenceDataGridViewTextBoxColumn, Me.InvestigatingOfficer})
        Me.SOCDatagrid.DataSource = Me.SOCRegisterBindingSource
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.SOCDatagrid.DefaultCellStyle = DataGridViewCellStyle5
        Me.SOCDatagrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SOCDatagrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke
        Me.SOCDatagrid.GridColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.SOCDatagrid.Location = New System.Drawing.Point(0, 44)
        Me.SOCDatagrid.MultiSelect = False
        Me.SOCDatagrid.Name = "SOCDatagrid"
        Me.SOCDatagrid.RowHeadersWidth = 50
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.SOCDatagrid.RowsDefaultCellStyle = DataGridViewCellStyle6
        Me.SOCDatagrid.RowTemplate.Height = 40
        Me.SOCDatagrid.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.SOCDatagrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.SOCDatagrid.ShowCellErrors = False
        Me.SOCDatagrid.Size = New System.Drawing.Size(888, 659)
        Me.SOCDatagrid.TabIndex = 43
        Me.SOCDatagrid.TabStop = False
        '
        'SOCNumberDataGridViewTextBoxColumn
        '
        Me.SOCNumberDataGridViewTextBoxColumn.DataPropertyName = "SOCNumber"
        Me.SOCNumberDataGridViewTextBoxColumn.HeaderText = "SOC No."
        Me.SOCNumberDataGridViewTextBoxColumn.Name = "SOCNumberDataGridViewTextBoxColumn"
        Me.SOCNumberDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.SOCNumberDataGridViewTextBoxColumn.Width = 80
        '
        'SelectRow
        '
        Me.SelectRow.HeaderText = "Select"
        Me.SelectRow.Name = "SelectRow"
        Me.SelectRow.Width = 80
        '
        'DateOfInspectionDataGridViewTextBoxColumn
        '
        Me.DateOfInspectionDataGridViewTextBoxColumn.DataPropertyName = "DateOfInspection"
        DataGridViewCellStyle2.Format = "dd/MM/yyyy"
        Me.DateOfInspectionDataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle2
        Me.DateOfInspectionDataGridViewTextBoxColumn.HeaderText = "DI"
        Me.DateOfInspectionDataGridViewTextBoxColumn.Name = "DateOfInspectionDataGridViewTextBoxColumn"
        Me.DateOfInspectionDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.DateOfInspectionDataGridViewTextBoxColumn.Width = 90
        '
        'PoliceStationDataGridViewTextBoxColumn
        '
        Me.PoliceStationDataGridViewTextBoxColumn.DataPropertyName = "PoliceStation"
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.PoliceStationDataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle3
        Me.PoliceStationDataGridViewTextBoxColumn.HeaderText = "Police Station"
        Me.PoliceStationDataGridViewTextBoxColumn.Name = "PoliceStationDataGridViewTextBoxColumn"
        Me.PoliceStationDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.PoliceStationDataGridViewTextBoxColumn.Width = 150
        '
        'CrimeNumberDataGridViewTextBoxColumn
        '
        Me.CrimeNumberDataGridViewTextBoxColumn.DataPropertyName = "CrimeNumber"
        Me.CrimeNumberDataGridViewTextBoxColumn.HeaderText = "Cr.No."
        Me.CrimeNumberDataGridViewTextBoxColumn.Name = "CrimeNumberDataGridViewTextBoxColumn"
        Me.CrimeNumberDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        '
        'PlaceOfOccurrenceDataGridViewTextBoxColumn
        '
        Me.PlaceOfOccurrenceDataGridViewTextBoxColumn.DataPropertyName = "PlaceOfOccurrence"
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.PlaceOfOccurrenceDataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle4
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
        Me.InvestigatingOfficer.Width = 210
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
        Me.StatusBar.Location = New System.Drawing.Point(0, 703)
        Me.StatusBar.MaximumSize = New System.Drawing.Size(0, 30)
        Me.StatusBar.Name = "StatusBar"
        Me.StatusBar.Size = New System.Drawing.Size(888, 30)
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
        Me.PanelSOC.Size = New System.Drawing.Size(888, 44)
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
        Me.PanelEx2.Controls.Add(Me.GroupPanel3)
        Me.PanelEx2.Controls.Add(Me.GroupPanel2)
        Me.PanelEx2.Controls.Add(Me.GroupPanel1)
        Me.PanelEx2.Controls.Add(Me.GroupPanel5)
        Me.PanelEx2.Controls.Add(Me.PanelEx5)
        Me.PanelEx2.DisabledBackColor = System.Drawing.Color.Empty
        Me.PanelEx2.Dock = System.Windows.Forms.DockStyle.Left
        Me.PanelEx2.Location = New System.Drawing.Point(0, 0)
        Me.PanelEx2.Name = "PanelEx2"
        Me.PanelEx2.Size = New System.Drawing.Size(404, 733)
        Me.PanelEx2.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx2.Style.GradientAngle = 90
        Me.PanelEx2.TabIndex = 43
        '
        'GroupPanel3
        '
        Me.GroupPanel3.BackColor = System.Drawing.Color.Transparent
        Me.GroupPanel3.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanel3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanel3.Controls.Add(Me.btnGenerateBlankTR56A)
        Me.GroupPanel3.Controls.Add(Me.btnGenerateBlankTourNote)
        Me.GroupPanel3.Controls.Add(Me.btnGenerateBlankTR47)
        Me.GroupPanel3.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel3.Location = New System.Drawing.Point(12, 457)
        Me.GroupPanel3.Name = "GroupPanel3"
        Me.GroupPanel3.Size = New System.Drawing.Size(379, 123)
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
        Me.GroupPanel3.TabIndex = 47
        Me.GroupPanel3.Text = "Blank Forms"
        '
        'btnGenerateBlankTR56A
        '
        Me.btnGenerateBlankTR56A.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnGenerateBlankTR56A.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnGenerateBlankTR56A.Location = New System.Drawing.Point(263, 13)
        Me.btnGenerateBlankTR56A.Name = "btnGenerateBlankTR56A"
        Me.btnGenerateBlankTR56A.Size = New System.Drawing.Size(107, 69)
        Me.btnGenerateBlankTR56A.TabIndex = 15
        Me.btnGenerateBlankTR56A.Text = "Blank TR 56A"
        '
        'btnGenerateBlankTourNote
        '
        Me.btnGenerateBlankTourNote.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnGenerateBlankTourNote.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnGenerateBlankTourNote.Location = New System.Drawing.Point(3, 13)
        Me.btnGenerateBlankTourNote.Name = "btnGenerateBlankTourNote"
        Me.btnGenerateBlankTourNote.Size = New System.Drawing.Size(107, 69)
        Me.btnGenerateBlankTourNote.TabIndex = 13
        Me.btnGenerateBlankTourNote.Text = "Blank Tour Note"
        '
        'btnGenerateBlankTR47
        '
        Me.btnGenerateBlankTR47.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnGenerateBlankTR47.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnGenerateBlankTR47.Location = New System.Drawing.Point(133, 13)
        Me.btnGenerateBlankTR47.Name = "btnGenerateBlankTR47"
        Me.btnGenerateBlankTR47.Size = New System.Drawing.Size(107, 69)
        Me.btnGenerateBlankTR47.TabIndex = 14
        Me.btnGenerateBlankTR47.Text = "Blank TR 47"
        '
        'GroupPanel2
        '
        Me.GroupPanel2.BackColor = System.Drawing.Color.Transparent
        Me.GroupPanel2.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanel2.Controls.Add(Me.PanelEx4)
        Me.GroupPanel2.Controls.Add(Me.btnOpenTABillFolder)
        Me.GroupPanel2.Controls.Add(Me.btnShowTourNote)
        Me.GroupPanel2.Controls.Add(Me.btnTABill)
        Me.GroupPanel2.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel2.Location = New System.Drawing.Point(12, 243)
        Me.GroupPanel2.Name = "GroupPanel2"
        Me.GroupPanel2.Size = New System.Drawing.Size(379, 208)
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
        Me.GroupPanel2.TabIndex = 47
        Me.GroupPanel2.Text = "Step 3 : Generate Tour Note"
        '
        'PanelEx4
        '
        Me.PanelEx4.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.PanelEx4.Controls.Add(Me.chkUseSavedTourNote)
        Me.PanelEx4.Controls.Add(Me.LabelX7)
        Me.PanelEx4.Controls.Add(Me.chkThreeRows)
        Me.PanelEx4.Controls.Add(Me.chkSingleRow)
        Me.PanelEx4.DisabledBackColor = System.Drawing.Color.Empty
        Me.PanelEx4.Location = New System.Drawing.Point(3, 3)
        Me.PanelEx4.Name = "PanelEx4"
        Me.PanelEx4.Size = New System.Drawing.Size(367, 93)
        Me.PanelEx4.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx4.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx4.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx4.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx4.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx4.Style.GradientAngle = 90
        Me.PanelEx4.TabIndex = 47
        '
        'chkUseSavedTourNote
        '
        Me.chkUseSavedTourNote.AutoSize = True
        '
        '
        '
        Me.chkUseSavedTourNote.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.chkUseSavedTourNote.Checked = True
        Me.chkUseSavedTourNote.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkUseSavedTourNote.CheckValue = "Y"
        Me.chkUseSavedTourNote.Location = New System.Drawing.Point(13, 65)
        Me.chkUseSavedTourNote.Name = "chkUseSavedTourNote"
        Me.chkUseSavedTourNote.Size = New System.Drawing.Size(243, 18)
        Me.chkUseSavedTourNote.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.chkUseSavedTourNote.TabIndex = 45
        Me.chkUseSavedTourNote.Text = "Generate TA Bill from Saved Tour Note"
        '
        'LabelX7
        '
        Me.LabelX7.AutoSize = True
        '
        '
        '
        Me.LabelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX7.Location = New System.Drawing.Point(13, 22)
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
        Me.chkThreeRows.Location = New System.Drawing.Point(128, 35)
        Me.chkThreeRows.Name = "chkThreeRows"
        Me.chkThreeRows.Size = New System.Drawing.Size(152, 18)
        Me.chkThreeRows.TabIndex = 9
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
        Me.chkSingleRow.Location = New System.Drawing.Point(128, 7)
        Me.chkSingleRow.Name = "chkSingleRow"
        Me.chkSingleRow.Size = New System.Drawing.Size(84, 18)
        Me.chkSingleRow.TabIndex = 8
        Me.chkSingleRow.Text = "Single Line"
        '
        'btnOpenTABillFolder
        '
        Me.btnOpenTABillFolder.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnOpenTABillFolder.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnOpenTABillFolder.Location = New System.Drawing.Point(263, 102)
        Me.btnOpenTABillFolder.Name = "btnOpenTABillFolder"
        Me.btnOpenTABillFolder.Size = New System.Drawing.Size(107, 69)
        Me.btnOpenTABillFolder.TabIndex = 46
        Me.btnOpenTABillFolder.Text = "Open TA Folder"
        '
        'btnShowTourNote
        '
        Me.btnShowTourNote.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnShowTourNote.BackColor = System.Drawing.Color.Transparent
        Me.btnShowTourNote.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnShowTourNote.Location = New System.Drawing.Point(3, 102)
        Me.btnShowTourNote.Name = "btnShowTourNote"
        Me.btnShowTourNote.Size = New System.Drawing.Size(107, 69)
        Me.btnShowTourNote.TabIndex = 10
        Me.btnShowTourNote.Text = " Show Tour Note"
        Me.btnShowTourNote.TextColor = System.Drawing.Color.Red
        '
        'btnTABill
        '
        Me.btnTABill.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnTABill.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnTABill.Location = New System.Drawing.Point(133, 102)
        Me.btnTABill.Name = "btnTABill"
        Me.btnTABill.Size = New System.Drawing.Size(107, 69)
        Me.btnTABill.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnShowTABillOuter})
        Me.btnTABill.TabIndex = 45
        Me.btnTABill.Text = " Show TA Bill"
        Me.btnTABill.TextColor = System.Drawing.Color.Red
        '
        'btnShowTABillOuter
        '
        Me.btnShowTABillOuter.GlobalItem = False
        Me.btnShowTABillOuter.Name = "btnShowTABillOuter"
        Me.btnShowTABillOuter.Text = "Show TA Bill Outer"
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
        Me.GroupPanel1.Location = New System.Drawing.Point(12, 131)
        Me.GroupPanel1.Name = "GroupPanel1"
        Me.GroupPanel1.Size = New System.Drawing.Size(379, 106)
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
        Me.GroupPanel1.TabIndex = 47
        Me.GroupPanel1.Text = "Step 2 : Generate Records"
        '
        'LabelX3
        '
        Me.LabelX3.AutoSize = True
        '
        '
        '
        Me.LabelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX3.Location = New System.Drawing.Point(5, 15)
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
        Me.LabelX4.Location = New System.Drawing.Point(244, 15)
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
        Me.cmbSOCOfficer.Size = New System.Drawing.Size(307, 29)
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
        Me.txtYear.Location = New System.Drawing.Point(282, 9)
        Me.txtYear.MaxValue = 2099
        Me.txtYear.MinValue = 2000
        Me.txtYear.Name = "txtYear"
        Me.txtYear.ShowUpDown = True
        Me.txtYear.Size = New System.Drawing.Size(77, 29)
        Me.txtYear.TabIndex = 5
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
        Me.cmbMonth.TabIndex = 4
        Me.cmbMonth.WatermarkText = "Month"
        '
        'LabelX1
        '
        Me.LabelX1.AutoSize = True
        '
        '
        '
        Me.LabelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX1.Location = New System.Drawing.Point(5, 53)
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
        Me.GroupPanel5.Controls.Add(Me.LabelX5)
        Me.GroupPanel5.Controls.Add(Me.LabelX2)
        Me.GroupPanel5.Controls.Add(Me.chkUsePS)
        Me.GroupPanel5.Controls.Add(Me.chkUsePO)
        Me.GroupPanel5.Controls.Add(Me.txtStartingLocation)
        Me.GroupPanel5.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel5.Location = New System.Drawing.Point(12, 7)
        Me.GroupPanel5.Name = "GroupPanel5"
        Me.GroupPanel5.Size = New System.Drawing.Size(379, 118)
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
        'LabelX5
        '
        Me.LabelX5.AutoSize = True
        '
        '
        '
        Me.LabelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX5.Location = New System.Drawing.Point(5, 54)
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
        Me.chkUsePS.Location = New System.Drawing.Point(52, 69)
        Me.chkUsePS.Name = "chkUsePS"
        Me.chkUsePS.Size = New System.Drawing.Size(124, 18)
        Me.chkUsePS.TabIndex = 3
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
        Me.chkUsePO.Location = New System.Drawing.Point(52, 40)
        Me.chkUsePO.Name = "chkUsePO"
        Me.chkUsePO.Size = New System.Drawing.Size(158, 18)
        Me.chkUsePO.TabIndex = 2
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
        Me.txtStartingLocation.Location = New System.Drawing.Point(52, 5)
        Me.txtStartingLocation.MaxLength = 45
        Me.txtStartingLocation.Name = "txtStartingLocation"
        Me.txtStartingLocation.Size = New System.Drawing.Size(307, 29)
        Me.txtStartingLocation.TabIndex = 1
        Me.txtStartingLocation.WatermarkText = "From"
        '
        'PanelEx5
        '
        Me.PanelEx5.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx5.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.PanelEx5.Controls.Add(Me.lblSavedTABill)
        Me.PanelEx5.Controls.Add(Me.lblSavedTourNote)
        Me.PanelEx5.DisabledBackColor = System.Drawing.Color.Empty
        Me.PanelEx5.Location = New System.Drawing.Point(12, 585)
        Me.PanelEx5.Name = "PanelEx5"
        Me.PanelEx5.Size = New System.Drawing.Size(379, 80)
        Me.PanelEx5.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx5.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx5.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx5.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx5.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx5.Style.GradientAngle = 90
        Me.PanelEx5.TabIndex = 45
        '
        'lblSavedTABill
        '
        Me.lblSavedTABill.AutoSize = True
        '
        '
        '
        Me.lblSavedTABill.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblSavedTABill.Location = New System.Drawing.Point(19, 35)
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
        Me.lblSavedTourNote.Location = New System.Drawing.Point(19, 11)
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
        'FrmTourNote
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1292, 733)
        Me.Controls.Add(Me.PanelEx1)
        Me.DoubleBuffered = True
        Me.EnableGlass = False
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FrmTourNote"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Tour Note/TA Bill Generator"
        Me.TitleText = "<b>Tour Note/TA Bill Generator</b>"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.PanelEx1.ResumeLayout(False)
        Me.PanelEx3.ResumeLayout(False)
        CType(Me.SOCDatagrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SOCRegisterBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FingerPrintDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelEx2.ResumeLayout(False)
        Me.GroupPanel3.ResumeLayout(False)
        Me.GroupPanel2.ResumeLayout(False)
        Me.PanelEx4.ResumeLayout(False)
        Me.PanelEx4.PerformLayout()
        Me.GroupPanel1.ResumeLayout(False)
        Me.GroupPanel1.PerformLayout()
        CType(Me.txtYear, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupPanel5.ResumeLayout(False)
        Me.GroupPanel5.PerformLayout()
        Me.PanelEx5.ResumeLayout(False)
        Me.PanelEx5.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PanelEx1 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents LabelX1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents btnShowTourNote As DevComponents.DotNetBar.ButtonX
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
    Friend WithEvents SOCDatagrid As DevComponents.DotNetBar.Controls.DataGridViewX
    Friend WithEvents btnSelectAll As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnDeselectAll As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents lblOfficerName As DevComponents.DotNetBar.LabelItem
    Friend WithEvents lblPEN As DevComponents.DotNetBar.LabelItem
    Friend WithEvents lblBasicPay As DevComponents.DotNetBar.LabelItem
    Friend WithEvents btnTABill As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnOpenTABillFolder As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnGenerateBlankTR56A As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnGenerateBlankTR47 As DevComponents.DotNetBar.ButtonX
    Friend WithEvents PanelEx4 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents SOCNumberDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SelectRow As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents DateOfInspectionDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PoliceStationDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CrimeNumberDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PlaceOfOccurrenceDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents InvestigatingOfficer As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents chkUseSavedTourNote As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents lblSavedTourNote As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblSavedTABill As DevComponents.DotNetBar.LabelX
    Friend WithEvents PanelEx5 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents lblDA As DevComponents.DotNetBar.LabelItem
    Friend WithEvents btnShowTABillOuter As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents GroupPanel3 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents GroupPanel2 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents GroupPanel1 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents GroupPanel5 As DevComponents.DotNetBar.Controls.GroupPanel
End Class
