﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmIdentificationStatement
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmIdentificationStatement))
        Me.FingerPrintDataSet = New FingerprintInformationSystem.FingerPrintDataSet()
        Me.PanelEx1 = New DevComponents.DotNetBar.PanelEx()
        Me.LabelX6 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX4 = New DevComponents.DotNetBar.LabelX()
        Me.cmbMonth = New DevComponents.DotNetBar.Controls.ComboBoxEx()
        Me.CircularProgress1 = New DevComponents.DotNetBar.Controls.CircularProgress()
        Me.btnGeneratebyPeriod = New DevComponents.DotNetBar.ButtonX()
        Me.LabelX1 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX3 = New DevComponents.DotNetBar.LabelX()
        Me.dtFrom = New DevComponents.Editors.DateTimeAdv.DateTimeInput()
        Me.dtTo = New DevComponents.Editors.DateTimeAdv.DateTimeInput()
        Me.LabelX2 = New DevComponents.DotNetBar.LabelX()
        Me.txtYear = New DevComponents.Editors.IntegerInput()
        Me.btnGenerateByMonth = New DevComponents.DotNetBar.ButtonX()
        Me.btnOpenFolder = New DevComponents.DotNetBar.ButtonX()
        Me.bgwIDList = New System.ComponentModel.BackgroundWorker()
        Me.JoinedIDRTableAdapter1 = New FingerprintInformationSystem.FingerPrintDataSetTableAdapters.JoinedIDRTableAdapter()
        CType(Me.FingerPrintDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelEx1.SuspendLayout()
        CType(Me.dtFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtYear, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'FingerPrintDataSet
        '
        Me.FingerPrintDataSet.DataSetName = "FingerPrintDataSet"
        Me.FingerPrintDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'PanelEx1
        '
        Me.PanelEx1.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.PanelEx1.Controls.Add(Me.LabelX6)
        Me.PanelEx1.Controls.Add(Me.LabelX4)
        Me.PanelEx1.Controls.Add(Me.cmbMonth)
        Me.PanelEx1.Controls.Add(Me.CircularProgress1)
        Me.PanelEx1.Controls.Add(Me.btnGeneratebyPeriod)
        Me.PanelEx1.Controls.Add(Me.LabelX1)
        Me.PanelEx1.Controls.Add(Me.LabelX3)
        Me.PanelEx1.Controls.Add(Me.dtFrom)
        Me.PanelEx1.Controls.Add(Me.dtTo)
        Me.PanelEx1.Controls.Add(Me.LabelX2)
        Me.PanelEx1.Controls.Add(Me.txtYear)
        Me.PanelEx1.Controls.Add(Me.btnGenerateByMonth)
        Me.PanelEx1.Controls.Add(Me.btnOpenFolder)
        Me.PanelEx1.DisabledBackColor = System.Drawing.Color.Empty
        Me.PanelEx1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelEx1.Location = New System.Drawing.Point(0, 0)
        Me.PanelEx1.Name = "PanelEx1"
        Me.PanelEx1.Size = New System.Drawing.Size(512, 126)
        Me.PanelEx1.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx1.Style.GradientAngle = 90
        Me.PanelEx1.TabIndex = 32
        '
        'LabelX6
        '
        Me.LabelX6.AutoSize = True
        '
        '
        '
        Me.LabelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX6.Location = New System.Drawing.Point(161, 91)
        Me.LabelX6.Name = "LabelX6"
        Me.LabelX6.Size = New System.Drawing.Size(213, 18)
        Me.LabelX6.TabIndex = 59
        Me.LabelX6.Text = "Open Identification Statement Folder"
        '
        'LabelX4
        '
        Me.LabelX4.AutoSize = True
        '
        '
        '
        Me.LabelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX4.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX4.Location = New System.Drawing.Point(12, 17)
        Me.LabelX4.Name = "LabelX4"
        Me.LabelX4.Size = New System.Drawing.Size(44, 20)
        Me.LabelX4.TabIndex = 56
        Me.LabelX4.Text = "Month"
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
        Me.cmbMonth.Location = New System.Drawing.Point(90, 12)
        Me.cmbMonth.MaxDropDownItems = 15
        Me.cmbMonth.MaxLength = 255
        Me.cmbMonth.Name = "cmbMonth"
        Me.cmbMonth.Size = New System.Drawing.Size(118, 29)
        Me.cmbMonth.TabIndex = 0
        Me.cmbMonth.WatermarkText = "Month"
        '
        'CircularProgress1
        '
        '
        '
        '
        Me.CircularProgress1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.CircularProgress1.Dock = System.Windows.Forms.DockStyle.Right
        Me.CircularProgress1.FocusCuesEnabled = False
        Me.CircularProgress1.Location = New System.Drawing.Point(392, 0)
        Me.CircularProgress1.Name = "CircularProgress1"
        Me.CircularProgress1.ProgressColor = System.Drawing.Color.Red
        Me.CircularProgress1.ProgressTextVisible = True
        Me.CircularProgress1.Size = New System.Drawing.Size(120, 126)
        Me.CircularProgress1.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP
        Me.CircularProgress1.TabIndex = 54
        Me.CircularProgress1.TabStop = False
        '
        'btnGeneratebyPeriod
        '
        Me.btnGeneratebyPeriod.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnGeneratebyPeriod.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnGeneratebyPeriod.Location = New System.Drawing.Point(392, 49)
        Me.btnGeneratebyPeriod.Name = "btnGeneratebyPeriod"
        Me.btnGeneratebyPeriod.Size = New System.Drawing.Size(110, 29)
        Me.btnGeneratebyPeriod.TabIndex = 5
        Me.btnGeneratebyPeriod.Text = "Generate"
        '
        'LabelX1
        '
        Me.LabelX1.AutoSize = True
        '
        '
        '
        Me.LabelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX1.Location = New System.Drawing.Point(225, 54)
        Me.LabelX1.Name = "LabelX1"
        Me.LabelX1.Size = New System.Drawing.Size(17, 18)
        Me.LabelX1.TabIndex = 18
        Me.LabelX1.Text = "To"
        '
        'LabelX3
        '
        Me.LabelX3.AutoSize = True
        '
        '
        '
        Me.LabelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX3.Location = New System.Drawing.Point(12, 54)
        Me.LabelX3.Name = "LabelX3"
        Me.LabelX3.Size = New System.Drawing.Size(72, 18)
        Me.LabelX3.TabIndex = 17
        Me.LabelX3.Text = "Period From"
        '
        'dtFrom
        '
        Me.dtFrom.AllowEmptyState = False
        Me.dtFrom.AutoAdvance = True
        Me.dtFrom.AutoSelectDate = True
        Me.dtFrom.BackColor = System.Drawing.SystemColors.Window
        '
        '
        '
        Me.dtFrom.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.dtFrom.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.dtFrom.ButtonClear.Image = CType(resources.GetObject("dtFrom.ButtonClear.Image"), System.Drawing.Image)
        Me.dtFrom.ButtonDropDown.Visible = True
        Me.dtFrom.CustomFormat = "dd/MM/yyyy"
        Me.dtFrom.FocusHighlightEnabled = True
        Me.dtFrom.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtFrom.Format = DevComponents.Editors.eDateTimePickerFormat.Custom
        Me.dtFrom.IsPopupCalendarOpen = False
        Me.dtFrom.Location = New System.Drawing.Point(90, 49)
        Me.dtFrom.MaxDate = New Date(2100, 12, 31, 0, 0, 0, 0)
        Me.dtFrom.MinDate = New Date(1900, 1, 1, 0, 0, 0, 0)
        '
        '
        '
        '
        '
        '
        Me.dtFrom.MonthCalendar.BackgroundStyle.BackColor = System.Drawing.SystemColors.Window
        Me.dtFrom.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.dtFrom.MonthCalendar.ClearButtonVisible = True
        '
        '
        '
        Me.dtFrom.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2
        Me.dtFrom.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90
        Me.dtFrom.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.dtFrom.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.dtFrom.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder
        Me.dtFrom.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1
        Me.dtFrom.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.dtFrom.MonthCalendar.DaySize = New System.Drawing.Size(30, 15)
        Me.dtFrom.MonthCalendar.DisplayMonth = New Date(2008, 7, 1, 0, 0, 0, 0)
        '
        '
        '
        Me.dtFrom.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.dtFrom.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90
        Me.dtFrom.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.dtFrom.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.dtFrom.MonthCalendar.TodayButtonVisible = True
        Me.dtFrom.Name = "dtFrom"
        Me.dtFrom.Size = New System.Drawing.Size(118, 29)
        Me.dtFrom.TabIndex = 3
        Me.dtFrom.Value = New Date(1900, 1, 1, 0, 0, 0, 0)
        Me.dtFrom.WatermarkText = "From"
        '
        'dtTo
        '
        Me.dtTo.AllowEmptyState = False
        Me.dtTo.AutoAdvance = True
        Me.dtTo.AutoSelectDate = True
        Me.dtTo.BackColor = System.Drawing.SystemColors.Window
        '
        '
        '
        Me.dtTo.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.dtTo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.dtTo.ButtonClear.Image = CType(resources.GetObject("dtTo.ButtonClear.Image"), System.Drawing.Image)
        Me.dtTo.ButtonDropDown.Visible = True
        Me.dtTo.CustomFormat = "dd/MM/yyyy"
        Me.dtTo.FocusHighlightEnabled = True
        Me.dtTo.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtTo.Format = DevComponents.Editors.eDateTimePickerFormat.Custom
        Me.dtTo.IsPopupCalendarOpen = False
        Me.dtTo.Location = New System.Drawing.Point(259, 49)
        Me.dtTo.MaxDate = New Date(2100, 12, 31, 0, 0, 0, 0)
        Me.dtTo.MinDate = New Date(1900, 1, 1, 0, 0, 0, 0)
        '
        '
        '
        '
        '
        '
        Me.dtTo.MonthCalendar.BackgroundStyle.BackColor = System.Drawing.SystemColors.Window
        Me.dtTo.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.dtTo.MonthCalendar.ClearButtonVisible = True
        '
        '
        '
        Me.dtTo.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2
        Me.dtTo.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90
        Me.dtTo.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.dtTo.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.dtTo.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder
        Me.dtTo.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1
        Me.dtTo.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.dtTo.MonthCalendar.DaySize = New System.Drawing.Size(30, 15)
        Me.dtTo.MonthCalendar.DisplayMonth = New Date(2008, 7, 1, 0, 0, 0, 0)
        '
        '
        '
        Me.dtTo.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.dtTo.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90
        Me.dtTo.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.dtTo.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.dtTo.MonthCalendar.TodayButtonVisible = True
        Me.dtTo.Name = "dtTo"
        Me.dtTo.Size = New System.Drawing.Size(115, 29)
        Me.dtTo.TabIndex = 4
        Me.dtTo.Value = New Date(1900, 1, 1, 0, 0, 0, 0)
        Me.dtTo.WatermarkText = "To"
        '
        'LabelX2
        '
        Me.LabelX2.AutoSize = True
        '
        '
        '
        Me.LabelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX2.Location = New System.Drawing.Point(225, 17)
        Me.LabelX2.Name = "LabelX2"
        Me.LabelX2.Size = New System.Drawing.Size(28, 18)
        Me.LabelX2.TabIndex = 14
        Me.LabelX2.Text = "Year"
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
        Me.txtYear.Location = New System.Drawing.Point(259, 12)
        Me.txtYear.MaxValue = 2099
        Me.txtYear.MinValue = 1900
        Me.txtYear.Name = "txtYear"
        Me.txtYear.ShowUpDown = True
        Me.txtYear.Size = New System.Drawing.Size(115, 29)
        Me.txtYear.TabIndex = 1
        Me.txtYear.Value = 1900
        Me.txtYear.WatermarkText = "Year"
        '
        'btnGenerateByMonth
        '
        Me.btnGenerateByMonth.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnGenerateByMonth.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnGenerateByMonth.Location = New System.Drawing.Point(392, 12)
        Me.btnGenerateByMonth.Name = "btnGenerateByMonth"
        Me.btnGenerateByMonth.Size = New System.Drawing.Size(110, 29)
        Me.btnGenerateByMonth.TabIndex = 2
        Me.btnGenerateByMonth.Text = "Generate"
        '
        'btnOpenFolder
        '
        Me.btnOpenFolder.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnOpenFolder.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnOpenFolder.Location = New System.Drawing.Point(392, 86)
        Me.btnOpenFolder.Name = "btnOpenFolder"
        Me.btnOpenFolder.Size = New System.Drawing.Size(110, 29)
        Me.btnOpenFolder.TabIndex = 60
        Me.btnOpenFolder.Text = "Open Folder"
        '
        'bgwIDList
        '
        Me.bgwIDList.WorkerReportsProgress = True
        Me.bgwIDList.WorkerSupportsCancellation = True
        '
        'JoinedIDRTableAdapter1
        '
        Me.JoinedIDRTableAdapter1.ClearBeforeFill = True
        '
        'frmIdentificationStatement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(512, 126)
        Me.Controls.Add(Me.PanelEx1)
        Me.DoubleBuffered = True
        Me.EnableGlass = False
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmIdentificationStatement"
        Me.ShowInTaskbar = False
        Me.Text = "Identification Statement"
        Me.TitleText = "<b>Identification Statement</b>"
        CType(Me.FingerPrintDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelEx1.ResumeLayout(False)
        Me.PanelEx1.PerformLayout()
        CType(Me.dtFrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtYear, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents FingerPrintDataSet As FingerprintInformationSystem.FingerPrintDataSet
    Friend WithEvents PanelEx1 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents CircularProgress1 As DevComponents.DotNetBar.Controls.CircularProgress
    Friend WithEvents btnGeneratebyPeriod As DevComponents.DotNetBar.ButtonX
    Friend WithEvents LabelX1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX3 As DevComponents.DotNetBar.LabelX
    Friend WithEvents dtFrom As DevComponents.Editors.DateTimeAdv.DateTimeInput
    Friend WithEvents dtTo As DevComponents.Editors.DateTimeAdv.DateTimeInput
    Friend WithEvents LabelX2 As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtYear As DevComponents.Editors.IntegerInput
    Friend WithEvents btnGenerateByMonth As DevComponents.DotNetBar.ButtonX
    Friend WithEvents bgwIDList As System.ComponentModel.BackgroundWorker
    Friend WithEvents LabelX4 As DevComponents.DotNetBar.LabelX
    Friend WithEvents cmbMonth As DevComponents.DotNetBar.Controls.ComboBoxEx
    Friend WithEvents btnOpenFolder As DevComponents.DotNetBar.ButtonX
    Friend WithEvents LabelX6 As DevComponents.DotNetBar.LabelX
    Friend WithEvents JoinedIDRTableAdapter1 As FingerprintInformationSystem.FingerPrintDataSetTableAdapters.JoinedIDRTableAdapter
End Class