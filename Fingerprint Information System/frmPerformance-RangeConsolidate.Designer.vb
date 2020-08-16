<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPerformance_RangeConsolidate
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPerformance_RangeConsolidate))
        Me.PanelEx1 = New DevComponents.DotNetBar.PanelEx()
        Me.txtAnnualYear = New DevComponents.Editors.IntegerInput()
        Me.LabelX1 = New DevComponents.DotNetBar.LabelX()
        Me.lblStmt = New DevComponents.DotNetBar.LabelX()
        Me.lbl1 = New DevComponents.DotNetBar.LabelX()
        Me.lbl5 = New DevComponents.DotNetBar.LabelX()
        Me.lbl2 = New DevComponents.DotNetBar.LabelX()
        Me.lbl3 = New DevComponents.DotNetBar.LabelX()
        Me.lbl4 = New DevComponents.DotNetBar.LabelX()
        Me.CircularProgress1 = New DevComponents.DotNetBar.Controls.CircularProgress()
        Me.lblDistrict1 = New DevComponents.DotNetBar.LabelX()
        Me.lblDistrict5 = New DevComponents.DotNetBar.LabelX()
        Me.lblDistrict2 = New DevComponents.DotNetBar.LabelX()
        Me.lblDistrict3 = New DevComponents.DotNetBar.LabelX()
        Me.lblDistrict4 = New DevComponents.DotNetBar.LabelX()
        Me.btnConsolidateQuarter = New DevComponents.DotNetBar.ButtonX()
        Me.btnConsolidateMonth = New DevComponents.DotNetBar.ButtonX()
        Me.LabelX6 = New DevComponents.DotNetBar.LabelX()
        Me.txtQuarter = New DevComponents.Editors.IntegerInput()
        Me.txtQuarterYear = New DevComponents.Editors.IntegerInput()
        Me.LabelX5 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX4 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX3 = New DevComponents.DotNetBar.LabelX()
        Me.txtMonthlyYear = New DevComponents.Editors.IntegerInput()
        Me.cmbMonth = New DevComponents.DotNetBar.Controls.ComboBoxEx()
        Me.btnDownloadStatements = New DevComponents.DotNetBar.ButtonX()
        Me.btnConsolidateAnnual = New DevComponents.DotNetBar.ButtonX()
        Me.btnOpenFolder = New DevComponents.DotNetBar.ButtonX()
        Me.bgwDownloadMonthFiles = New System.ComponentModel.BackgroundWorker()
        Me.bgwDownloadQuarterFiles = New System.ComponentModel.BackgroundWorker()
        Me.bgwDownloadAnnualFiles = New System.ComponentModel.BackgroundWorker()
        Me.bgwDownloadStatements = New System.ComponentModel.BackgroundWorker()
        Me.lblID1 = New DevComponents.DotNetBar.LabelX()
        Me.lblID5 = New DevComponents.DotNetBar.LabelX()
        Me.lblID2 = New DevComponents.DotNetBar.LabelX()
        Me.lblID3 = New DevComponents.DotNetBar.LabelX()
        Me.lblID4 = New DevComponents.DotNetBar.LabelX()
        Me.lblSOC1 = New DevComponents.DotNetBar.LabelX()
        Me.lblSOC5 = New DevComponents.DotNetBar.LabelX()
        Me.lblSOC2 = New DevComponents.DotNetBar.LabelX()
        Me.lblSOC3 = New DevComponents.DotNetBar.LabelX()
        Me.lblSOC4 = New DevComponents.DotNetBar.LabelX()
        Me.lblGrave = New DevComponents.DotNetBar.LabelX()
        Me.lblID = New DevComponents.DotNetBar.LabelX()
        Me.lblSOC = New DevComponents.DotNetBar.LabelX()
        Me.LabelX2 = New DevComponents.DotNetBar.LabelX()
        Me.PanelEx1.SuspendLayout()
        CType(Me.txtAnnualYear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtQuarter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtQuarterYear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMonthlyYear, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelEx1
        '
        Me.PanelEx1.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.PanelEx1.Controls.Add(Me.LabelX2)
        Me.PanelEx1.Controls.Add(Me.lblSOC)
        Me.PanelEx1.Controls.Add(Me.lblID)
        Me.PanelEx1.Controls.Add(Me.lblGrave)
        Me.PanelEx1.Controls.Add(Me.lblSOC1)
        Me.PanelEx1.Controls.Add(Me.lblSOC5)
        Me.PanelEx1.Controls.Add(Me.lblSOC2)
        Me.PanelEx1.Controls.Add(Me.lblSOC3)
        Me.PanelEx1.Controls.Add(Me.lblSOC4)
        Me.PanelEx1.Controls.Add(Me.lblID1)
        Me.PanelEx1.Controls.Add(Me.lblID5)
        Me.PanelEx1.Controls.Add(Me.lblID2)
        Me.PanelEx1.Controls.Add(Me.lblID3)
        Me.PanelEx1.Controls.Add(Me.lblID4)
        Me.PanelEx1.Controls.Add(Me.txtAnnualYear)
        Me.PanelEx1.Controls.Add(Me.LabelX1)
        Me.PanelEx1.Controls.Add(Me.lblStmt)
        Me.PanelEx1.Controls.Add(Me.lbl1)
        Me.PanelEx1.Controls.Add(Me.lbl5)
        Me.PanelEx1.Controls.Add(Me.lbl2)
        Me.PanelEx1.Controls.Add(Me.lbl3)
        Me.PanelEx1.Controls.Add(Me.lbl4)
        Me.PanelEx1.Controls.Add(Me.CircularProgress1)
        Me.PanelEx1.Controls.Add(Me.lblDistrict1)
        Me.PanelEx1.Controls.Add(Me.lblDistrict5)
        Me.PanelEx1.Controls.Add(Me.lblDistrict2)
        Me.PanelEx1.Controls.Add(Me.lblDistrict3)
        Me.PanelEx1.Controls.Add(Me.lblDistrict4)
        Me.PanelEx1.Controls.Add(Me.btnConsolidateQuarter)
        Me.PanelEx1.Controls.Add(Me.btnConsolidateMonth)
        Me.PanelEx1.Controls.Add(Me.LabelX6)
        Me.PanelEx1.Controls.Add(Me.txtQuarter)
        Me.PanelEx1.Controls.Add(Me.txtQuarterYear)
        Me.PanelEx1.Controls.Add(Me.LabelX5)
        Me.PanelEx1.Controls.Add(Me.LabelX4)
        Me.PanelEx1.Controls.Add(Me.LabelX3)
        Me.PanelEx1.Controls.Add(Me.txtMonthlyYear)
        Me.PanelEx1.Controls.Add(Me.cmbMonth)
        Me.PanelEx1.Controls.Add(Me.btnDownloadStatements)
        Me.PanelEx1.Controls.Add(Me.btnConsolidateAnnual)
        Me.PanelEx1.Controls.Add(Me.btnOpenFolder)
        Me.PanelEx1.DisabledBackColor = System.Drawing.Color.Empty
        Me.PanelEx1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelEx1.Location = New System.Drawing.Point(0, 0)
        Me.PanelEx1.Name = "PanelEx1"
        Me.PanelEx1.Size = New System.Drawing.Size(674, 316)
        Me.PanelEx1.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx1.Style.GradientAngle = 90
        Me.PanelEx1.TabIndex = 0
        '
        'txtAnnualYear
        '
        '
        '
        '
        Me.txtAnnualYear.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.txtAnnualYear.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtAnnualYear.ButtonCustom.Image = CType(resources.GetObject("txtAnnualYear.ButtonCustom.Image"), System.Drawing.Image)
        Me.txtAnnualYear.FocusHighlightEnabled = True
        Me.txtAnnualYear.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAnnualYear.InputHorizontalAlignment = DevComponents.Editors.eHorizontalAlignment.Left
        Me.txtAnnualYear.Location = New System.Drawing.Point(230, 96)
        Me.txtAnnualYear.MaxValue = 2099
        Me.txtAnnualYear.MinValue = 1900
        Me.txtAnnualYear.MouseWheelValueChangeEnabled = False
        Me.txtAnnualYear.Name = "txtAnnualYear"
        Me.txtAnnualYear.ShowUpDown = True
        Me.txtAnnualYear.Size = New System.Drawing.Size(64, 29)
        Me.txtAnnualYear.TabIndex = 74
        Me.txtAnnualYear.Value = 1900
        Me.txtAnnualYear.WatermarkText = "Year"
        '
        'LabelX1
        '
        Me.LabelX1.AutoSize = True
        '
        '
        '
        Me.LabelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX1.Location = New System.Drawing.Point(45, 101)
        Me.LabelX1.Name = "LabelX1"
        Me.LabelX1.Size = New System.Drawing.Size(180, 18)
        Me.LabelX1.TabIndex = 76
        Me.LabelX1.Text = "Annual Work Done for the Year"
        '
        'lblStmt
        '
        Me.lblStmt.AutoSize = True
        '
        '
        '
        Me.lblStmt.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblStmt.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStmt.ForeColor = System.Drawing.Color.Red
        Me.lblStmt.Location = New System.Drawing.Point(15, 137)
        Me.lblStmt.Name = "lblStmt"
        Me.lblStmt.Size = New System.Drawing.Size(51, 20)
        Me.lblStmt.TabIndex = 73
        Me.lblStmt.Text = "LabelX1"
        '
        'lbl1
        '
        Me.lbl1.AutoSize = True
        '
        '
        '
        Me.lbl1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lbl1.Location = New System.Drawing.Point(179, 189)
        Me.lbl1.Name = "lbl1"
        Me.lbl1.Size = New System.Drawing.Size(47, 18)
        Me.lbl1.TabIndex = 72
        Me.lbl1.Text = "LabelX1"
        '
        'lbl5
        '
        Me.lbl5.AutoSize = True
        '
        '
        '
        Me.lbl5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lbl5.Location = New System.Drawing.Point(179, 285)
        Me.lbl5.Name = "lbl5"
        Me.lbl5.Size = New System.Drawing.Size(47, 18)
        Me.lbl5.TabIndex = 71
        Me.lbl5.Text = "LabelX9"
        '
        'lbl2
        '
        Me.lbl2.AutoSize = True
        '
        '
        '
        Me.lbl2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lbl2.Location = New System.Drawing.Point(179, 213)
        Me.lbl2.Name = "lbl2"
        Me.lbl2.Size = New System.Drawing.Size(47, 18)
        Me.lbl2.TabIndex = 70
        Me.lbl2.Text = "LabelX8"
        '
        'lbl3
        '
        Me.lbl3.AutoSize = True
        '
        '
        '
        Me.lbl3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lbl3.Location = New System.Drawing.Point(179, 237)
        Me.lbl3.Name = "lbl3"
        Me.lbl3.Size = New System.Drawing.Size(47, 18)
        Me.lbl3.TabIndex = 69
        Me.lbl3.Text = "LabelX7"
        '
        'lbl4
        '
        Me.lbl4.AutoSize = True
        '
        '
        '
        Me.lbl4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lbl4.Location = New System.Drawing.Point(179, 261)
        Me.lbl4.Name = "lbl4"
        Me.lbl4.Size = New System.Drawing.Size(47, 18)
        Me.lbl4.TabIndex = 68
        Me.lbl4.Text = "LabelX2"
        '
        'CircularProgress1
        '
        '
        '
        '
        Me.CircularProgress1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.CircularProgress1.FocusCuesEnabled = False
        Me.CircularProgress1.Location = New System.Drawing.Point(310, 18)
        Me.CircularProgress1.Name = "CircularProgress1"
        Me.CircularProgress1.ProgressColor = System.Drawing.Color.Red
        Me.CircularProgress1.ProgressTextVisible = True
        Me.CircularProgress1.Size = New System.Drawing.Size(229, 107)
        Me.CircularProgress1.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP
        Me.CircularProgress1.TabIndex = 67
        Me.CircularProgress1.TabStop = False
        '
        'lblDistrict1
        '
        Me.lblDistrict1.AutoSize = True
        '
        '
        '
        Me.lblDistrict1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblDistrict1.Location = New System.Drawing.Point(15, 189)
        Me.lblDistrict1.Name = "lblDistrict1"
        Me.lblDistrict1.Size = New System.Drawing.Size(47, 18)
        Me.lblDistrict1.TabIndex = 66
        Me.lblDistrict1.Text = "LabelX1"
        '
        'lblDistrict5
        '
        Me.lblDistrict5.AutoSize = True
        '
        '
        '
        Me.lblDistrict5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblDistrict5.Location = New System.Drawing.Point(15, 285)
        Me.lblDistrict5.Name = "lblDistrict5"
        Me.lblDistrict5.Size = New System.Drawing.Size(47, 18)
        Me.lblDistrict5.TabIndex = 65
        Me.lblDistrict5.Text = "LabelX9"
        '
        'lblDistrict2
        '
        Me.lblDistrict2.AutoSize = True
        '
        '
        '
        Me.lblDistrict2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblDistrict2.Location = New System.Drawing.Point(15, 213)
        Me.lblDistrict2.Name = "lblDistrict2"
        Me.lblDistrict2.Size = New System.Drawing.Size(47, 18)
        Me.lblDistrict2.TabIndex = 64
        Me.lblDistrict2.Text = "LabelX8"
        '
        'lblDistrict3
        '
        Me.lblDistrict3.AutoSize = True
        '
        '
        '
        Me.lblDistrict3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblDistrict3.Location = New System.Drawing.Point(15, 237)
        Me.lblDistrict3.Name = "lblDistrict3"
        Me.lblDistrict3.Size = New System.Drawing.Size(47, 18)
        Me.lblDistrict3.TabIndex = 63
        Me.lblDistrict3.Text = "LabelX7"
        '
        'lblDistrict4
        '
        Me.lblDistrict4.AutoSize = True
        '
        '
        '
        Me.lblDistrict4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblDistrict4.Location = New System.Drawing.Point(15, 261)
        Me.lblDistrict4.Name = "lblDistrict4"
        Me.lblDistrict4.Size = New System.Drawing.Size(47, 18)
        Me.lblDistrict4.TabIndex = 62
        Me.lblDistrict4.Text = "LabelX2"
        '
        'btnConsolidateQuarter
        '
        Me.btnConsolidateQuarter.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnConsolidateQuarter.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnConsolidateQuarter.Location = New System.Drawing.Point(326, 57)
        Me.btnConsolidateQuarter.Name = "btnConsolidateQuarter"
        Me.btnConsolidateQuarter.Size = New System.Drawing.Size(75, 29)
        Me.btnConsolidateQuarter.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnConsolidateQuarter.TabIndex = 6
        Me.btnConsolidateQuarter.Text = "Consolidate"
        '
        'btnConsolidateMonth
        '
        Me.btnConsolidateMonth.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnConsolidateMonth.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnConsolidateMonth.Location = New System.Drawing.Point(326, 18)
        Me.btnConsolidateMonth.Name = "btnConsolidateMonth"
        Me.btnConsolidateMonth.Size = New System.Drawing.Size(75, 29)
        Me.btnConsolidateMonth.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnConsolidateMonth.TabIndex = 3
        Me.btnConsolidateMonth.Text = "Consolidate"
        '
        'LabelX6
        '
        Me.LabelX6.AutoSize = True
        '
        '
        '
        Me.LabelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX6.Location = New System.Drawing.Point(15, 62)
        Me.LabelX6.Name = "LabelX6"
        Me.LabelX6.Size = New System.Drawing.Size(46, 18)
        Me.LabelX6.TabIndex = 57
        Me.LabelX6.Text = "Quarter"
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
        Me.txtQuarter.Location = New System.Drawing.Point(73, 57)
        Me.txtQuarter.MaxValue = 4
        Me.txtQuarter.MinValue = 1
        Me.txtQuarter.MouseWheelValueChangeEnabled = False
        Me.txtQuarter.Name = "txtQuarter"
        Me.txtQuarter.ShowUpDown = True
        Me.txtQuarter.Size = New System.Drawing.Size(110, 29)
        Me.txtQuarter.TabIndex = 4
        Me.txtQuarter.Value = 4
        Me.txtQuarter.WatermarkText = "Quarter"
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
        Me.txtQuarterYear.Location = New System.Drawing.Point(230, 57)
        Me.txtQuarterYear.MaxValue = 2099
        Me.txtQuarterYear.MinValue = 1900
        Me.txtQuarterYear.MouseWheelValueChangeEnabled = False
        Me.txtQuarterYear.Name = "txtQuarterYear"
        Me.txtQuarterYear.ShowUpDown = True
        Me.txtQuarterYear.Size = New System.Drawing.Size(64, 29)
        Me.txtQuarterYear.TabIndex = 5
        Me.txtQuarterYear.Value = 1900
        Me.txtQuarterYear.WatermarkText = "Year"
        '
        'LabelX5
        '
        Me.LabelX5.AutoSize = True
        '
        '
        '
        Me.LabelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX5.Location = New System.Drawing.Point(196, 62)
        Me.LabelX5.Name = "LabelX5"
        Me.LabelX5.Size = New System.Drawing.Size(28, 18)
        Me.LabelX5.TabIndex = 58
        Me.LabelX5.Text = "Year"
        '
        'LabelX4
        '
        Me.LabelX4.AutoSize = True
        '
        '
        '
        Me.LabelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX4.Location = New System.Drawing.Point(196, 23)
        Me.LabelX4.Name = "LabelX4"
        Me.LabelX4.Size = New System.Drawing.Size(28, 18)
        Me.LabelX4.TabIndex = 38
        Me.LabelX4.Text = "Year"
        '
        'LabelX3
        '
        Me.LabelX3.AutoSize = True
        '
        '
        '
        Me.LabelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX3.Location = New System.Drawing.Point(15, 23)
        Me.LabelX3.Name = "LabelX3"
        Me.LabelX3.Size = New System.Drawing.Size(40, 18)
        Me.LabelX3.TabIndex = 37
        Me.LabelX3.Text = "Month"
        '
        'txtMonthlyYear
        '
        '
        '
        '
        Me.txtMonthlyYear.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.txtMonthlyYear.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtMonthlyYear.ButtonCustom.Image = CType(resources.GetObject("txtMonthlyYear.ButtonCustom.Image"), System.Drawing.Image)
        Me.txtMonthlyYear.FocusHighlightEnabled = True
        Me.txtMonthlyYear.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMonthlyYear.InputHorizontalAlignment = DevComponents.Editors.eHorizontalAlignment.Left
        Me.txtMonthlyYear.Location = New System.Drawing.Point(230, 18)
        Me.txtMonthlyYear.MaxValue = 2099
        Me.txtMonthlyYear.MinValue = 0
        Me.txtMonthlyYear.Name = "txtMonthlyYear"
        Me.txtMonthlyYear.ShowUpDown = True
        Me.txtMonthlyYear.Size = New System.Drawing.Size(64, 29)
        Me.txtMonthlyYear.TabIndex = 2
        Me.txtMonthlyYear.Value = 1900
        Me.txtMonthlyYear.WatermarkText = "Year"
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
        Me.cmbMonth.Location = New System.Drawing.Point(73, 18)
        Me.cmbMonth.MaxDropDownItems = 15
        Me.cmbMonth.MaxLength = 255
        Me.cmbMonth.Name = "cmbMonth"
        Me.cmbMonth.Size = New System.Drawing.Size(110, 29)
        Me.cmbMonth.TabIndex = 1
        Me.cmbMonth.WatermarkText = "Month"
        '
        'btnDownloadStatements
        '
        Me.btnDownloadStatements.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnDownloadStatements.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnDownloadStatements.Location = New System.Drawing.Point(424, 18)
        Me.btnDownloadStatements.Name = "btnDownloadStatements"
        Me.btnDownloadStatements.Size = New System.Drawing.Size(103, 68)
        Me.btnDownloadStatements.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnDownloadStatements.TabIndex = 8
        Me.btnDownloadStatements.Text = "Download Monthly Statements"
        '
        'btnConsolidateAnnual
        '
        Me.btnConsolidateAnnual.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnConsolidateAnnual.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnConsolidateAnnual.Location = New System.Drawing.Point(326, 96)
        Me.btnConsolidateAnnual.Name = "btnConsolidateAnnual"
        Me.btnConsolidateAnnual.Size = New System.Drawing.Size(75, 29)
        Me.btnConsolidateAnnual.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnConsolidateAnnual.TabIndex = 75
        Me.btnConsolidateAnnual.Text = "Consolidate"
        '
        'btnOpenFolder
        '
        Me.btnOpenFolder.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnOpenFolder.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnOpenFolder.Location = New System.Drawing.Point(424, 96)
        Me.btnOpenFolder.Name = "btnOpenFolder"
        Me.btnOpenFolder.Size = New System.Drawing.Size(103, 29)
        Me.btnOpenFolder.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnOpenFolder.TabIndex = 7
        Me.btnOpenFolder.Text = "Open Folder"
        '
        'bgwDownloadMonthFiles
        '
        Me.bgwDownloadMonthFiles.WorkerReportsProgress = True
        Me.bgwDownloadMonthFiles.WorkerSupportsCancellation = True
        '
        'bgwDownloadQuarterFiles
        '
        Me.bgwDownloadQuarterFiles.WorkerReportsProgress = True
        Me.bgwDownloadQuarterFiles.WorkerSupportsCancellation = True
        '
        'bgwDownloadAnnualFiles
        '
        Me.bgwDownloadAnnualFiles.WorkerReportsProgress = True
        Me.bgwDownloadAnnualFiles.WorkerSupportsCancellation = True
        '
        'bgwDownloadStatements
        '
        Me.bgwDownloadStatements.WorkerReportsProgress = True
        Me.bgwDownloadStatements.WorkerSupportsCancellation = True
        '
        'lblID1
        '
        Me.lblID1.AutoSize = True
        '
        '
        '
        Me.lblID1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblID1.Location = New System.Drawing.Point(354, 189)
        Me.lblID1.Name = "lblID1"
        Me.lblID1.Size = New System.Drawing.Size(47, 18)
        Me.lblID1.TabIndex = 81
        Me.lblID1.Text = "LabelX1"
        '
        'lblID5
        '
        Me.lblID5.AutoSize = True
        '
        '
        '
        Me.lblID5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblID5.Location = New System.Drawing.Point(354, 285)
        Me.lblID5.Name = "lblID5"
        Me.lblID5.Size = New System.Drawing.Size(47, 18)
        Me.lblID5.TabIndex = 80
        Me.lblID5.Text = "LabelX9"
        '
        'lblID2
        '
        Me.lblID2.AutoSize = True
        '
        '
        '
        Me.lblID2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblID2.Location = New System.Drawing.Point(354, 213)
        Me.lblID2.Name = "lblID2"
        Me.lblID2.Size = New System.Drawing.Size(47, 18)
        Me.lblID2.TabIndex = 79
        Me.lblID2.Text = "LabelX8"
        '
        'lblID3
        '
        Me.lblID3.AutoSize = True
        '
        '
        '
        Me.lblID3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblID3.Location = New System.Drawing.Point(354, 237)
        Me.lblID3.Name = "lblID3"
        Me.lblID3.Size = New System.Drawing.Size(47, 18)
        Me.lblID3.TabIndex = 78
        Me.lblID3.Text = "LabelX7"
        '
        'lblID4
        '
        Me.lblID4.AutoSize = True
        '
        '
        '
        Me.lblID4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblID4.Location = New System.Drawing.Point(354, 261)
        Me.lblID4.Name = "lblID4"
        Me.lblID4.Size = New System.Drawing.Size(47, 18)
        Me.lblID4.TabIndex = 77
        Me.lblID4.Text = "LabelX2"
        '
        'lblSOC1
        '
        Me.lblSOC1.AutoSize = True
        '
        '
        '
        Me.lblSOC1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblSOC1.Location = New System.Drawing.Point(542, 189)
        Me.lblSOC1.Name = "lblSOC1"
        Me.lblSOC1.Size = New System.Drawing.Size(47, 18)
        Me.lblSOC1.TabIndex = 86
        Me.lblSOC1.Text = "LabelX1"
        '
        'lblSOC5
        '
        Me.lblSOC5.AutoSize = True
        '
        '
        '
        Me.lblSOC5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblSOC5.Location = New System.Drawing.Point(542, 285)
        Me.lblSOC5.Name = "lblSOC5"
        Me.lblSOC5.Size = New System.Drawing.Size(47, 18)
        Me.lblSOC5.TabIndex = 85
        Me.lblSOC5.Text = "LabelX9"
        '
        'lblSOC2
        '
        Me.lblSOC2.AutoSize = True
        '
        '
        '
        Me.lblSOC2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblSOC2.Location = New System.Drawing.Point(542, 213)
        Me.lblSOC2.Name = "lblSOC2"
        Me.lblSOC2.Size = New System.Drawing.Size(47, 18)
        Me.lblSOC2.TabIndex = 84
        Me.lblSOC2.Text = "LabelX8"
        '
        'lblSOC3
        '
        Me.lblSOC3.AutoSize = True
        '
        '
        '
        Me.lblSOC3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblSOC3.Location = New System.Drawing.Point(542, 237)
        Me.lblSOC3.Name = "lblSOC3"
        Me.lblSOC3.Size = New System.Drawing.Size(47, 18)
        Me.lblSOC3.TabIndex = 83
        Me.lblSOC3.Text = "LabelX7"
        '
        'lblSOC4
        '
        Me.lblSOC4.AutoSize = True
        '
        '
        '
        Me.lblSOC4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblSOC4.Location = New System.Drawing.Point(542, 261)
        Me.lblSOC4.Name = "lblSOC4"
        Me.lblSOC4.Size = New System.Drawing.Size(47, 18)
        Me.lblSOC4.TabIndex = 82
        Me.lblSOC4.Text = "LabelX2"
        '
        'lblGrave
        '
        Me.lblGrave.AutoSize = True
        '
        '
        '
        Me.lblGrave.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblGrave.Font = New System.Drawing.Font("Segoe UI", 9.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGrave.ForeColor = System.Drawing.Color.Red
        Me.lblGrave.Location = New System.Drawing.Point(179, 165)
        Me.lblGrave.Name = "lblGrave"
        Me.lblGrave.Size = New System.Drawing.Size(35, 18)
        Me.lblGrave.TabIndex = 87
        Me.lblGrave.Text = "Grave"
        '
        'lblID
        '
        Me.lblID.AutoSize = True
        '
        '
        '
        Me.lblID.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblID.Font = New System.Drawing.Font("Segoe UI", 9.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblID.ForeColor = System.Drawing.Color.Red
        Me.lblID.Location = New System.Drawing.Point(354, 165)
        Me.lblID.Name = "lblID"
        Me.lblID.Size = New System.Drawing.Size(78, 18)
        Me.lblID.TabIndex = 88
        Me.lblID.Text = "Identification"
        '
        'lblSOC
        '
        Me.lblSOC.AutoSize = True
        '
        '
        '
        Me.lblSOC.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblSOC.Font = New System.Drawing.Font("Segoe UI", 9.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSOC.ForeColor = System.Drawing.Color.Red
        Me.lblSOC.Location = New System.Drawing.Point(542, 165)
        Me.lblSOC.Name = "lblSOC"
        Me.lblSOC.Size = New System.Drawing.Size(26, 18)
        Me.lblSOC.TabIndex = 89
        Me.lblSOC.Text = "SOC"
        '
        'LabelX2
        '
        Me.LabelX2.AutoSize = True
        '
        '
        '
        Me.LabelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX2.Font = New System.Drawing.Font("Segoe UI", 9.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX2.ForeColor = System.Drawing.Color.Red
        Me.LabelX2.Location = New System.Drawing.Point(15, 165)
        Me.LabelX2.Name = "LabelX2"
        Me.LabelX2.Size = New System.Drawing.Size(43, 18)
        Me.LabelX2.TabIndex = 90
        Me.LabelX2.Text = "District"
        '
        'frmPerformance_RangeConsolidate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(674, 316)
        Me.Controls.Add(Me.PanelEx1)
        Me.DoubleBuffered = True
        Me.EnableGlass = False
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPerformance_RangeConsolidate"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Consolidate Work Done"
        Me.TitleText = "<b>Consolidate Work Done</b>"
        Me.PanelEx1.ResumeLayout(False)
        Me.PanelEx1.PerformLayout()
        CType(Me.txtAnnualYear, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtQuarter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtQuarterYear, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMonthlyYear, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PanelEx1 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents LabelX4 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX3 As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtMonthlyYear As DevComponents.Editors.IntegerInput
    Friend WithEvents cmbMonth As DevComponents.DotNetBar.Controls.ComboBoxEx
    Friend WithEvents btnConsolidateMonth As DevComponents.DotNetBar.ButtonX
    Friend WithEvents LabelX6 As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtQuarter As DevComponents.Editors.IntegerInput
    Friend WithEvents txtQuarterYear As DevComponents.Editors.IntegerInput
    Friend WithEvents LabelX5 As DevComponents.DotNetBar.LabelX
    Friend WithEvents btnConsolidateQuarter As DevComponents.DotNetBar.ButtonX
    Friend WithEvents lblDistrict5 As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblDistrict2 As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblDistrict3 As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblDistrict4 As DevComponents.DotNetBar.LabelX
    Friend WithEvents CircularProgress1 As DevComponents.DotNetBar.Controls.CircularProgress
    Friend WithEvents lbl1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents lbl5 As DevComponents.DotNetBar.LabelX
    Friend WithEvents lbl2 As DevComponents.DotNetBar.LabelX
    Friend WithEvents lbl3 As DevComponents.DotNetBar.LabelX
    Friend WithEvents lbl4 As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblStmt As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblDistrict1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents bgwDownloadMonthFiles As System.ComponentModel.BackgroundWorker
    Friend WithEvents bgwDownloadQuarterFiles As System.ComponentModel.BackgroundWorker
    Friend WithEvents btnOpenFolder As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnConsolidateAnnual As DevComponents.DotNetBar.ButtonX
    Friend WithEvents txtAnnualYear As DevComponents.Editors.IntegerInput
    Friend WithEvents LabelX1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents btnDownloadStatements As DevComponents.DotNetBar.ButtonX
    Friend WithEvents bgwDownloadAnnualFiles As System.ComponentModel.BackgroundWorker
    Friend WithEvents bgwDownloadStatements As System.ComponentModel.BackgroundWorker
    Friend WithEvents lblSOC1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblSOC5 As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblSOC2 As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblSOC3 As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblSOC4 As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblID1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblID5 As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblID2 As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblID3 As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblID4 As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblSOC As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblID As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblGrave As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX2 As DevComponents.DotNetBar.LabelX
End Class
