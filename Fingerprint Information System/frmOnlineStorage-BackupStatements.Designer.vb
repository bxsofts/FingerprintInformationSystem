<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBackupStatements
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBackupStatements))
        Me.PanelEx1 = New DevComponents.DotNetBar.PanelEx()
        Me.btnDownload = New DevComponents.DotNetBar.ButtonX()
        Me.PanelEx2 = New DevComponents.DotNetBar.PanelEx()
        Me.Line2 = New DevComponents.DotNetBar.Controls.Line()
        Me.Line1 = New DevComponents.DotNetBar.Controls.Line()
        Me.lblAnnualPerf = New DevComponents.DotNetBar.LabelX()
        Me.chkAnnualPerf = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.LabelX2 = New DevComponents.DotNetBar.LabelX()
        Me.txtAnnualYear = New DevComponents.Editors.IntegerInput()
        Me.cmbMonth = New DevComponents.DotNetBar.Controls.ComboBoxEx()
        Me.lblSOC = New DevComponents.DotNetBar.LabelX()
        Me.chkSOC = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.lblQuarterlyPerf = New DevComponents.DotNetBar.LabelX()
        Me.chkGrave = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.lblGrave = New DevComponents.DotNetBar.LabelX()
        Me.chkID = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.lblID = New DevComponents.DotNetBar.LabelX()
        Me.chkMonthlyPerf = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.lblMonthPerf = New DevComponents.DotNetBar.LabelX()
        Me.txtMonthlyYear = New DevComponents.Editors.IntegerInput()
        Me.chkQuarterlyPerf = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.LabelX3 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX6 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX4 = New DevComponents.DotNetBar.LabelX()
        Me.txtQuarter = New DevComponents.Editors.IntegerInput()
        Me.LabelX5 = New DevComponents.DotNetBar.LabelX()
        Me.txtQuarterYear = New DevComponents.Editors.IntegerInput()
        Me.CircularProgress1 = New DevComponents.DotNetBar.Controls.CircularProgress()
        Me.btnBackup = New DevComponents.DotNetBar.ButtonX()
        Me.bgwUploadFile = New System.ComponentModel.BackgroundWorker()
        Me.bgwDownloadStatements = New System.ComponentModel.BackgroundWorker()
        Me.PanelEx1.SuspendLayout()
        Me.PanelEx2.SuspendLayout()
        CType(Me.txtAnnualYear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMonthlyYear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtQuarter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtQuarterYear, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelEx1
        '
        Me.PanelEx1.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.PanelEx1.Controls.Add(Me.PanelEx2)
        Me.PanelEx1.Controls.Add(Me.CircularProgress1)
        Me.PanelEx1.Controls.Add(Me.btnBackup)
        Me.PanelEx1.Controls.Add(Me.btnDownload)
        Me.PanelEx1.DisabledBackColor = System.Drawing.Color.Empty
        Me.PanelEx1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelEx1.Location = New System.Drawing.Point(0, 0)
        Me.PanelEx1.Name = "PanelEx1"
        Me.PanelEx1.Size = New System.Drawing.Size(487, 343)
        Me.PanelEx1.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx1.Style.GradientAngle = 90
        Me.PanelEx1.TabIndex = 0
        '
        'btnDownload
        '
        Me.btnDownload.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnDownload.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnDownload.Location = New System.Drawing.Point(363, 114)
        Me.btnDownload.Name = "btnDownload"
        Me.btnDownload.Size = New System.Drawing.Size(101, 56)
        Me.btnDownload.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnDownload.TabIndex = 7
        Me.btnDownload.Text = "Download"
        '
        'PanelEx2
        '
        Me.PanelEx2.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.PanelEx2.Controls.Add(Me.Line2)
        Me.PanelEx2.Controls.Add(Me.Line1)
        Me.PanelEx2.Controls.Add(Me.lblAnnualPerf)
        Me.PanelEx2.Controls.Add(Me.chkAnnualPerf)
        Me.PanelEx2.Controls.Add(Me.LabelX2)
        Me.PanelEx2.Controls.Add(Me.txtAnnualYear)
        Me.PanelEx2.Controls.Add(Me.cmbMonth)
        Me.PanelEx2.Controls.Add(Me.lblSOC)
        Me.PanelEx2.Controls.Add(Me.chkSOC)
        Me.PanelEx2.Controls.Add(Me.lblQuarterlyPerf)
        Me.PanelEx2.Controls.Add(Me.chkGrave)
        Me.PanelEx2.Controls.Add(Me.lblGrave)
        Me.PanelEx2.Controls.Add(Me.chkID)
        Me.PanelEx2.Controls.Add(Me.lblID)
        Me.PanelEx2.Controls.Add(Me.chkMonthlyPerf)
        Me.PanelEx2.Controls.Add(Me.lblMonthPerf)
        Me.PanelEx2.Controls.Add(Me.txtMonthlyYear)
        Me.PanelEx2.Controls.Add(Me.chkQuarterlyPerf)
        Me.PanelEx2.Controls.Add(Me.LabelX3)
        Me.PanelEx2.Controls.Add(Me.LabelX6)
        Me.PanelEx2.Controls.Add(Me.LabelX4)
        Me.PanelEx2.Controls.Add(Me.txtQuarter)
        Me.PanelEx2.Controls.Add(Me.LabelX5)
        Me.PanelEx2.Controls.Add(Me.txtQuarterYear)
        Me.PanelEx2.DisabledBackColor = System.Drawing.Color.Empty
        Me.PanelEx2.Dock = System.Windows.Forms.DockStyle.Left
        Me.PanelEx2.Location = New System.Drawing.Point(0, 0)
        Me.PanelEx2.Name = "PanelEx2"
        Me.PanelEx2.Size = New System.Drawing.Size(342, 343)
        Me.PanelEx2.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx2.Style.GradientAngle = 90
        Me.PanelEx2.TabIndex = 62
        '
        'Line2
        '
        Me.Line2.Location = New System.Drawing.Point(12, 252)
        Me.Line2.Name = "Line2"
        Me.Line2.Size = New System.Drawing.Size(313, 18)
        Me.Line2.TabIndex = 67
        Me.Line2.Text = "Line2"
        '
        'Line1
        '
        Me.Line1.Location = New System.Drawing.Point(12, 168)
        Me.Line1.Name = "Line1"
        Me.Line1.Size = New System.Drawing.Size(313, 18)
        Me.Line1.TabIndex = 66
        Me.Line1.Text = "Line1"
        '
        'lblAnnualPerf
        '
        Me.lblAnnualPerf.AutoSize = True
        '
        '
        '
        Me.lblAnnualPerf.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblAnnualPerf.Location = New System.Drawing.Point(190, 313)
        Me.lblAnnualPerf.Name = "lblAnnualPerf"
        Me.lblAnnualPerf.Size = New System.Drawing.Size(47, 18)
        Me.lblAnnualPerf.TabIndex = 65
        Me.lblAnnualPerf.Text = "LabelX9"
        '
        'chkAnnualPerf
        '
        Me.chkAnnualPerf.AutoSize = True
        '
        '
        '
        Me.chkAnnualPerf.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.chkAnnualPerf.Location = New System.Drawing.Point(67, 313)
        Me.chkAnnualPerf.Name = "chkAnnualPerf"
        Me.chkAnnualPerf.Size = New System.Drawing.Size(97, 18)
        Me.chkAnnualPerf.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.chkAnnualPerf.TabIndex = 64
        Me.chkAnnualPerf.TabStop = False
        Me.chkAnnualPerf.Text = "Annual Work"
        '
        'LabelX2
        '
        Me.LabelX2.AutoSize = True
        '
        '
        '
        Me.LabelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX2.Location = New System.Drawing.Point(12, 279)
        Me.LabelX2.Name = "LabelX2"
        Me.LabelX2.Size = New System.Drawing.Size(28, 18)
        Me.LabelX2.TabIndex = 63
        Me.LabelX2.Text = "Year"
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
        Me.txtAnnualYear.Location = New System.Drawing.Point(67, 274)
        Me.txtAnnualYear.MaxValue = 2099
        Me.txtAnnualYear.MinValue = 1900
        Me.txtAnnualYear.MouseWheelValueChangeEnabled = False
        Me.txtAnnualYear.Name = "txtAnnualYear"
        Me.txtAnnualYear.ShowUpDown = True
        Me.txtAnnualYear.Size = New System.Drawing.Size(64, 29)
        Me.txtAnnualYear.TabIndex = 5
        Me.txtAnnualYear.Value = 1900
        Me.txtAnnualYear.WatermarkText = "Year"
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
        Me.cmbMonth.Location = New System.Drawing.Point(67, 9)
        Me.cmbMonth.MaxDropDownItems = 15
        Me.cmbMonth.MaxLength = 255
        Me.cmbMonth.Name = "cmbMonth"
        Me.cmbMonth.Size = New System.Drawing.Size(110, 29)
        Me.cmbMonth.TabIndex = 1
        Me.cmbMonth.WatermarkText = "Month"
        '
        'lblSOC
        '
        Me.lblSOC.AutoSize = True
        '
        '
        '
        Me.lblSOC.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblSOC.Location = New System.Drawing.Point(190, 53)
        Me.lblSOC.Name = "lblSOC"
        Me.lblSOC.Size = New System.Drawing.Size(47, 18)
        Me.lblSOC.TabIndex = 61
        Me.lblSOC.Text = "LabelX1"
        '
        'chkSOC
        '
        Me.chkSOC.AutoSize = True
        '
        '
        '
        Me.chkSOC.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.chkSOC.Location = New System.Drawing.Point(67, 53)
        Me.chkSOC.Name = "chkSOC"
        Me.chkSOC.Size = New System.Drawing.Size(108, 18)
        Me.chkSOC.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.chkSOC.TabIndex = 1
        Me.chkSOC.TabStop = False
        Me.chkSOC.Text = "SOC Statement"
        '
        'lblQuarterlyPerf
        '
        Me.lblQuarterlyPerf.AutoSize = True
        '
        '
        '
        Me.lblQuarterlyPerf.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblQuarterlyPerf.Location = New System.Drawing.Point(190, 228)
        Me.lblQuarterlyPerf.Name = "lblQuarterlyPerf"
        Me.lblQuarterlyPerf.Size = New System.Drawing.Size(47, 18)
        Me.lblQuarterlyPerf.TabIndex = 60
        Me.lblQuarterlyPerf.Text = "LabelX9"
        '
        'chkGrave
        '
        Me.chkGrave.AutoSize = True
        '
        '
        '
        Me.chkGrave.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.chkGrave.Location = New System.Drawing.Point(67, 84)
        Me.chkGrave.Name = "chkGrave"
        Me.chkGrave.Size = New System.Drawing.Size(92, 18)
        Me.chkGrave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.chkGrave.TabIndex = 2
        Me.chkGrave.TabStop = False
        Me.chkGrave.Text = "Grave Crime"
        '
        'lblGrave
        '
        Me.lblGrave.AutoSize = True
        '
        '
        '
        Me.lblGrave.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblGrave.Location = New System.Drawing.Point(190, 84)
        Me.lblGrave.Name = "lblGrave"
        Me.lblGrave.Size = New System.Drawing.Size(47, 18)
        Me.lblGrave.TabIndex = 59
        Me.lblGrave.Text = "LabelX8"
        '
        'chkID
        '
        Me.chkID.AutoSize = True
        '
        '
        '
        Me.chkID.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.chkID.Location = New System.Drawing.Point(67, 115)
        Me.chkID.Name = "chkID"
        Me.chkID.Size = New System.Drawing.Size(98, 18)
        Me.chkID.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.chkID.TabIndex = 3
        Me.chkID.TabStop = False
        Me.chkID.Text = "Identification"
        '
        'lblID
        '
        Me.lblID.AutoSize = True
        '
        '
        '
        Me.lblID.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblID.Location = New System.Drawing.Point(190, 115)
        Me.lblID.Name = "lblID"
        Me.lblID.Size = New System.Drawing.Size(47, 18)
        Me.lblID.TabIndex = 58
        Me.lblID.Text = "LabelX7"
        '
        'chkMonthlyPerf
        '
        Me.chkMonthlyPerf.AutoSize = True
        '
        '
        '
        Me.chkMonthlyPerf.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.chkMonthlyPerf.Location = New System.Drawing.Point(67, 146)
        Me.chkMonthlyPerf.Name = "chkMonthlyPerf"
        Me.chkMonthlyPerf.Size = New System.Drawing.Size(87, 18)
        Me.chkMonthlyPerf.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.chkMonthlyPerf.TabIndex = 4
        Me.chkMonthlyPerf.TabStop = False
        Me.chkMonthlyPerf.Text = "Work Done"
        '
        'lblMonthPerf
        '
        Me.lblMonthPerf.AutoSize = True
        '
        '
        '
        Me.lblMonthPerf.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblMonthPerf.Location = New System.Drawing.Point(190, 146)
        Me.lblMonthPerf.Name = "lblMonthPerf"
        Me.lblMonthPerf.Size = New System.Drawing.Size(47, 18)
        Me.lblMonthPerf.TabIndex = 57
        Me.lblMonthPerf.Text = "LabelX2"
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
        Me.txtMonthlyYear.Location = New System.Drawing.Point(224, 9)
        Me.txtMonthlyYear.MaxValue = 2099
        Me.txtMonthlyYear.MinValue = 0
        Me.txtMonthlyYear.Name = "txtMonthlyYear"
        Me.txtMonthlyYear.ShowUpDown = True
        Me.txtMonthlyYear.Size = New System.Drawing.Size(64, 29)
        Me.txtMonthlyYear.TabIndex = 2
        Me.txtMonthlyYear.Value = 1900
        Me.txtMonthlyYear.WatermarkText = "Year"
        '
        'chkQuarterlyPerf
        '
        Me.chkQuarterlyPerf.AutoSize = True
        '
        '
        '
        Me.chkQuarterlyPerf.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.chkQuarterlyPerf.Location = New System.Drawing.Point(67, 228)
        Me.chkQuarterlyPerf.Name = "chkQuarterlyPerf"
        Me.chkQuarterlyPerf.Size = New System.Drawing.Size(110, 18)
        Me.chkQuarterlyPerf.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.chkQuarterlyPerf.TabIndex = 55
        Me.chkQuarterlyPerf.TabStop = False
        Me.chkQuarterlyPerf.Text = "Quarterly Work"
        '
        'LabelX3
        '
        Me.LabelX3.AutoSize = True
        '
        '
        '
        Me.LabelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX3.Location = New System.Drawing.Point(12, 14)
        Me.LabelX3.Name = "LabelX3"
        Me.LabelX3.Size = New System.Drawing.Size(40, 18)
        Me.LabelX3.TabIndex = 33
        Me.LabelX3.Text = "Month"
        '
        'LabelX6
        '
        Me.LabelX6.AutoSize = True
        '
        '
        '
        Me.LabelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX6.Location = New System.Drawing.Point(12, 195)
        Me.LabelX6.Name = "LabelX6"
        Me.LabelX6.Size = New System.Drawing.Size(46, 18)
        Me.LabelX6.TabIndex = 53
        Me.LabelX6.Text = "Quarter"
        '
        'LabelX4
        '
        Me.LabelX4.AutoSize = True
        '
        '
        '
        Me.LabelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX4.Location = New System.Drawing.Point(190, 14)
        Me.LabelX4.Name = "LabelX4"
        Me.LabelX4.Size = New System.Drawing.Size(28, 18)
        Me.LabelX4.TabIndex = 34
        Me.LabelX4.Text = "Year"
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
        Me.txtQuarter.Location = New System.Drawing.Point(67, 190)
        Me.txtQuarter.MaxValue = 4
        Me.txtQuarter.MinValue = 1
        Me.txtQuarter.MouseWheelValueChangeEnabled = False
        Me.txtQuarter.Name = "txtQuarter"
        Me.txtQuarter.ShowUpDown = True
        Me.txtQuarter.Size = New System.Drawing.Size(44, 29)
        Me.txtQuarter.TabIndex = 3
        Me.txtQuarter.Value = 4
        Me.txtQuarter.WatermarkText = "Quarter"
        '
        'LabelX5
        '
        Me.LabelX5.AutoSize = True
        '
        '
        '
        Me.LabelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX5.Location = New System.Drawing.Point(127, 195)
        Me.LabelX5.Name = "LabelX5"
        Me.LabelX5.Size = New System.Drawing.Size(28, 18)
        Me.LabelX5.TabIndex = 54
        Me.LabelX5.Text = "Year"
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
        Me.txtQuarterYear.Location = New System.Drawing.Point(161, 190)
        Me.txtQuarterYear.MaxValue = 2099
        Me.txtQuarterYear.MinValue = 1900
        Me.txtQuarterYear.MouseWheelValueChangeEnabled = False
        Me.txtQuarterYear.Name = "txtQuarterYear"
        Me.txtQuarterYear.ShowUpDown = True
        Me.txtQuarterYear.Size = New System.Drawing.Size(64, 29)
        Me.txtQuarterYear.TabIndex = 4
        Me.txtQuarterYear.Value = 1900
        Me.txtQuarterYear.WatermarkText = "Year"
        '
        'CircularProgress1
        '
        '
        '
        '
        Me.CircularProgress1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.CircularProgress1.Dock = System.Windows.Forms.DockStyle.Right
        Me.CircularProgress1.FocusCuesEnabled = False
        Me.CircularProgress1.Location = New System.Drawing.Point(359, 0)
        Me.CircularProgress1.Name = "CircularProgress1"
        Me.CircularProgress1.ProgressColor = System.Drawing.Color.Red
        Me.CircularProgress1.ProgressTextVisible = True
        Me.CircularProgress1.Size = New System.Drawing.Size(128, 343)
        Me.CircularProgress1.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP
        Me.CircularProgress1.TabIndex = 49
        Me.CircularProgress1.TabStop = False
        '
        'btnBackup
        '
        Me.btnBackup.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnBackup.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnBackup.Location = New System.Drawing.Point(363, 29)
        Me.btnBackup.Name = "btnBackup"
        Me.btnBackup.Size = New System.Drawing.Size(101, 56)
        Me.btnBackup.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnBackup.TabIndex = 6
        Me.btnBackup.Text = "Backup"
        '
        'bgwUploadFile
        '
        Me.bgwUploadFile.WorkerReportsProgress = True
        Me.bgwUploadFile.WorkerSupportsCancellation = True
        '
        'bgwDownloadStatements
        '
        Me.bgwDownloadStatements.WorkerReportsProgress = True
        Me.bgwDownloadStatements.WorkerSupportsCancellation = True
        '
        'frmBackupStatements
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(487, 343)
        Me.Controls.Add(Me.PanelEx1)
        Me.DoubleBuffered = True
        Me.EnableGlass = False
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmBackupStatements"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Backup Statements"
        Me.TitleText = "<b>Backup Statements</b>"
        Me.PanelEx1.ResumeLayout(False)
        Me.PanelEx2.ResumeLayout(False)
        Me.PanelEx2.PerformLayout()
        CType(Me.txtAnnualYear, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMonthlyYear, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtQuarter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtQuarterYear, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PanelEx1 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents chkMonthlyPerf As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents chkID As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents chkGrave As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents chkSOC As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents LabelX4 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX3 As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtMonthlyYear As DevComponents.Editors.IntegerInput
    Friend WithEvents cmbMonth As DevComponents.DotNetBar.Controls.ComboBoxEx
    Friend WithEvents btnBackup As DevComponents.DotNetBar.ButtonX
    Friend WithEvents CircularProgress1 As DevComponents.DotNetBar.Controls.CircularProgress
    Friend WithEvents bgwUploadFile As System.ComponentModel.BackgroundWorker
    Friend WithEvents LabelX6 As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtQuarter As DevComponents.Editors.IntegerInput
    Friend WithEvents txtQuarterYear As DevComponents.Editors.IntegerInput
    Friend WithEvents LabelX5 As DevComponents.DotNetBar.LabelX
    Friend WithEvents chkQuarterlyPerf As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents lblSOC As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblQuarterlyPerf As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblGrave As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblID As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblMonthPerf As DevComponents.DotNetBar.LabelX
    Friend WithEvents PanelEx2 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents lblAnnualPerf As DevComponents.DotNetBar.LabelX
    Friend WithEvents chkAnnualPerf As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents LabelX2 As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtAnnualYear As DevComponents.Editors.IntegerInput
    Friend WithEvents Line2 As DevComponents.DotNetBar.Controls.Line
    Friend WithEvents Line1 As DevComponents.DotNetBar.Controls.Line
    Friend WithEvents btnDownload As DevComponents.DotNetBar.ButtonX
    Friend WithEvents bgwDownloadStatements As System.ComponentModel.BackgroundWorker
End Class
