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
        Me.txtYear = New DevComponents.Editors.IntegerInput()
        Me.cmbMonth = New DevComponents.DotNetBar.Controls.ComboBoxEx()
        Me.bgwDownloadMonthFiles = New System.ComponentModel.BackgroundWorker()
        Me.bgwDownloadQuarterFiles = New System.ComponentModel.BackgroundWorker()
        Me.btnOpenFolder = New DevComponents.DotNetBar.ButtonX()
        Me.PanelEx1.SuspendLayout()
        CType(Me.txtQuarter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtQuarterYear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtYear, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelEx1
        '
        Me.PanelEx1.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.PanelEx1.Controls.Add(Me.btnOpenFolder)
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
        Me.PanelEx1.Controls.Add(Me.txtYear)
        Me.PanelEx1.Controls.Add(Me.cmbMonth)
        Me.PanelEx1.DisabledBackColor = System.Drawing.Color.Empty
        Me.PanelEx1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelEx1.Location = New System.Drawing.Point(0, 0)
        Me.PanelEx1.Name = "PanelEx1"
        Me.PanelEx1.Size = New System.Drawing.Size(449, 250)
        Me.PanelEx1.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx1.Style.GradientAngle = 90
        Me.PanelEx1.TabIndex = 0
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
        Me.lblStmt.Location = New System.Drawing.Point(15, 102)
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
        Me.lbl1.Location = New System.Drawing.Point(179, 126)
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
        Me.lbl5.Location = New System.Drawing.Point(179, 222)
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
        Me.lbl2.Location = New System.Drawing.Point(179, 150)
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
        Me.lbl3.Location = New System.Drawing.Point(179, 174)
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
        Me.lbl4.Location = New System.Drawing.Point(179, 198)
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
        Me.CircularProgress1.Location = New System.Drawing.Point(326, 18)
        Me.CircularProgress1.Name = "CircularProgress1"
        Me.CircularProgress1.ProgressColor = System.Drawing.Color.Red
        Me.CircularProgress1.ProgressTextVisible = True
        Me.CircularProgress1.Size = New System.Drawing.Size(75, 78)
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
        Me.lblDistrict1.Location = New System.Drawing.Point(15, 126)
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
        Me.lblDistrict5.Location = New System.Drawing.Point(15, 222)
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
        Me.lblDistrict2.Location = New System.Drawing.Point(15, 150)
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
        Me.lblDistrict3.Location = New System.Drawing.Point(15, 174)
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
        Me.lblDistrict4.Location = New System.Drawing.Point(15, 198)
        Me.lblDistrict4.Name = "lblDistrict4"
        Me.lblDistrict4.Size = New System.Drawing.Size(47, 18)
        Me.lblDistrict4.TabIndex = 62
        Me.lblDistrict4.Text = "LabelX2"
        '
        'btnConsolidateQuarter
        '
        Me.btnConsolidateQuarter.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnConsolidateQuarter.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnConsolidateQuarter.Location = New System.Drawing.Point(326, 58)
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
        Me.LabelX6.Location = New System.Drawing.Point(15, 63)
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
        Me.txtQuarter.Location = New System.Drawing.Point(73, 58)
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
        Me.txtQuarterYear.Location = New System.Drawing.Point(230, 58)
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
        Me.LabelX5.Location = New System.Drawing.Point(196, 63)
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
        Me.txtYear.Location = New System.Drawing.Point(230, 18)
        Me.txtYear.MaxValue = 2099
        Me.txtYear.MinValue = 0
        Me.txtYear.Name = "txtYear"
        Me.txtYear.ShowUpDown = True
        Me.txtYear.Size = New System.Drawing.Size(64, 29)
        Me.txtYear.TabIndex = 2
        Me.txtYear.Value = 1900
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
        Me.cmbMonth.Location = New System.Drawing.Point(73, 18)
        Me.cmbMonth.MaxDropDownItems = 15
        Me.cmbMonth.MaxLength = 255
        Me.cmbMonth.Name = "cmbMonth"
        Me.cmbMonth.Size = New System.Drawing.Size(110, 29)
        Me.cmbMonth.TabIndex = 1
        Me.cmbMonth.WatermarkText = "Month"
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
        'btnOpenFolder
        '
        Me.btnOpenFolder.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnOpenFolder.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnOpenFolder.Location = New System.Drawing.Point(326, 98)
        Me.btnOpenFolder.Name = "btnOpenFolder"
        Me.btnOpenFolder.Size = New System.Drawing.Size(75, 29)
        Me.btnOpenFolder.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnOpenFolder.TabIndex = 7
        Me.btnOpenFolder.Text = "Open Folder"
        '
        'frmPerformance_RangeConsolidate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(449, 250)
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
        CType(Me.txtQuarter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtQuarterYear, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtYear, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PanelEx1 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents LabelX4 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX3 As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtYear As DevComponents.Editors.IntegerInput
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
End Class
