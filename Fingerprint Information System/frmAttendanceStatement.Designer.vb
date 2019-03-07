<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAttendanceStmt
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAttendanceStmt))
        Me.PanelEx2 = New DevComponents.DotNetBar.PanelEx()
        Me.GroupPanel6 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.CircularProgress1 = New DevComponents.DotNetBar.Controls.CircularProgress()
        Me.btnOpenFolder = New DevComponents.DotNetBar.ButtonX()
        Me.btnGenerateCL = New DevComponents.DotNetBar.ButtonX()
        Me.LabelX1 = New DevComponents.DotNetBar.LabelX()
        Me.dtTo = New DevComponents.Editors.DateTimeAdv.DateTimeInput()
        Me.btnGenerateAttendance = New DevComponents.DotNetBar.ButtonX()
        Me.dtFrom = New DevComponents.Editors.DateTimeAdv.DateTimeInput()
        Me.LabelX2 = New DevComponents.DotNetBar.LabelX()
        Me.GroupPanel5 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.chkStatementOnly = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.chkCoB = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.GroupPanel4 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.chkStaff = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.chkTI = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.bgwAttendance = New System.ComponentModel.BackgroundWorker()
        Me.PanelEx2.SuspendLayout()
        Me.GroupPanel6.SuspendLayout()
        CType(Me.dtTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupPanel5.SuspendLayout()
        Me.GroupPanel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'PanelEx2
        '
        Me.PanelEx2.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.PanelEx2.Controls.Add(Me.GroupPanel6)
        Me.PanelEx2.Controls.Add(Me.GroupPanel5)
        Me.PanelEx2.Controls.Add(Me.GroupPanel4)
        Me.PanelEx2.DisabledBackColor = System.Drawing.Color.Empty
        Me.PanelEx2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelEx2.Location = New System.Drawing.Point(0, 0)
        Me.PanelEx2.Name = "PanelEx2"
        Me.PanelEx2.Size = New System.Drawing.Size(514, 358)
        Me.PanelEx2.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx2.Style.GradientAngle = 90
        Me.PanelEx2.TabIndex = 27
        '
        'GroupPanel6
        '
        Me.GroupPanel6.BackColor = System.Drawing.Color.Transparent
        Me.GroupPanel6.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanel6.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanel6.Controls.Add(Me.CircularProgress1)
        Me.GroupPanel6.Controls.Add(Me.btnOpenFolder)
        Me.GroupPanel6.Controls.Add(Me.btnGenerateCL)
        Me.GroupPanel6.Controls.Add(Me.LabelX1)
        Me.GroupPanel6.Controls.Add(Me.dtTo)
        Me.GroupPanel6.Controls.Add(Me.btnGenerateAttendance)
        Me.GroupPanel6.Controls.Add(Me.dtFrom)
        Me.GroupPanel6.Controls.Add(Me.LabelX2)
        Me.GroupPanel6.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel6.Location = New System.Drawing.Point(12, 192)
        Me.GroupPanel6.Name = "GroupPanel6"
        Me.GroupPanel6.Size = New System.Drawing.Size(492, 163)
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
        Me.GroupPanel6.TabIndex = 25
        Me.GroupPanel6.Text = "Period"
        '
        'CircularProgress1
        '
        '
        '
        '
        Me.CircularProgress1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.CircularProgress1.Dock = System.Windows.Forms.DockStyle.Right
        Me.CircularProgress1.FocusCuesEnabled = False
        Me.CircularProgress1.Location = New System.Drawing.Point(343, 0)
        Me.CircularProgress1.Name = "CircularProgress1"
        Me.CircularProgress1.ProgressColor = System.Drawing.Color.Red
        Me.CircularProgress1.ProgressTextVisible = True
        Me.CircularProgress1.Size = New System.Drawing.Size(143, 139)
        Me.CircularProgress1.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP
        Me.CircularProgress1.TabIndex = 26
        Me.CircularProgress1.TabStop = False
        '
        'btnOpenFolder
        '
        Me.btnOpenFolder.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnOpenFolder.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnOpenFolder.Location = New System.Drawing.Point(354, 95)
        Me.btnOpenFolder.Name = "btnOpenFolder"
        Me.btnOpenFolder.Size = New System.Drawing.Size(115, 36)
        Me.btnOpenFolder.TabIndex = 9
        Me.btnOpenFolder.Text = "Open Folder"
        '
        'btnGenerateCL
        '
        Me.btnGenerateCL.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnGenerateCL.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnGenerateCL.Location = New System.Drawing.Point(354, 51)
        Me.btnGenerateCL.Name = "btnGenerateCL"
        Me.btnGenerateCL.Size = New System.Drawing.Size(115, 36)
        Me.btnGenerateCL.TabIndex = 8
        Me.btnGenerateCL.Text = "Covering Letter"
        '
        'LabelX1
        '
        Me.LabelX1.AutoSize = True
        '
        '
        '
        Me.LabelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX1.Location = New System.Drawing.Point(24, 39)
        Me.LabelX1.Name = "LabelX1"
        Me.LabelX1.Size = New System.Drawing.Size(32, 18)
        Me.LabelX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.LabelX1.TabIndex = 12
        Me.LabelX1.Text = "From"
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
        Me.dtTo.Location = New System.Drawing.Point(76, 76)
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
        Me.dtTo.Size = New System.Drawing.Size(113, 29)
        Me.dtTo.TabIndex = 6
        Me.dtTo.Value = New Date(1900, 1, 1, 0, 0, 0, 0)
        Me.dtTo.WatermarkText = "To"
        '
        'btnGenerateAttendance
        '
        Me.btnGenerateAttendance.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnGenerateAttendance.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnGenerateAttendance.Location = New System.Drawing.Point(354, 7)
        Me.btnGenerateAttendance.Name = "btnGenerateAttendance"
        Me.btnGenerateAttendance.Size = New System.Drawing.Size(115, 36)
        Me.btnGenerateAttendance.TabIndex = 7
        Me.btnGenerateAttendance.Text = "Attendance"
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
        Me.dtFrom.Location = New System.Drawing.Point(76, 33)
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
        Me.dtFrom.Size = New System.Drawing.Size(113, 29)
        Me.dtFrom.TabIndex = 5
        Me.dtFrom.Value = New Date(1900, 1, 1, 0, 0, 0, 0)
        Me.dtFrom.WatermarkText = "From"
        '
        'LabelX2
        '
        Me.LabelX2.AutoSize = True
        '
        '
        '
        Me.LabelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX2.Location = New System.Drawing.Point(24, 82)
        Me.LabelX2.Name = "LabelX2"
        Me.LabelX2.Size = New System.Drawing.Size(17, 18)
        Me.LabelX2.TabIndex = 13
        Me.LabelX2.Text = "To"
        '
        'GroupPanel5
        '
        Me.GroupPanel5.BackColor = System.Drawing.Color.Transparent
        Me.GroupPanel5.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanel5.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanel5.Controls.Add(Me.chkStatementOnly)
        Me.GroupPanel5.Controls.Add(Me.chkCoB)
        Me.GroupPanel5.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel5.Location = New System.Drawing.Point(12, 102)
        Me.GroupPanel5.Name = "GroupPanel5"
        Me.GroupPanel5.Size = New System.Drawing.Size(492, 79)
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
        Me.GroupPanel5.TabIndex = 24
        Me.GroupPanel5.Text = "Statement Format"
        '
        'chkStatementOnly
        '
        Me.chkStatementOnly.AutoSize = True
        '
        '
        '
        Me.chkStatementOnly.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.chkStatementOnly.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton
        Me.chkStatementOnly.Location = New System.Drawing.Point(187, 22)
        Me.chkStatementOnly.Name = "chkStatementOnly"
        Me.chkStatementOnly.Size = New System.Drawing.Size(110, 18)
        Me.chkStatementOnly.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.chkStatementOnly.TabIndex = 4
        Me.chkStatementOnly.Text = "Statement Only"
        '
        'chkCoB
        '
        Me.chkCoB.AutoSize = True
        '
        '
        '
        Me.chkCoB.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.chkCoB.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton
        Me.chkCoB.Checked = True
        Me.chkCoB.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkCoB.CheckValue = "Y"
        Me.chkCoB.Location = New System.Drawing.Point(24, 22)
        Me.chkCoB.Name = "chkCoB"
        Me.chkCoB.Size = New System.Drawing.Size(97, 18)
        Me.chkCoB.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.chkCoB.TabIndex = 3
        Me.chkCoB.Text = "CoB Message"
        '
        'GroupPanel4
        '
        Me.GroupPanel4.BackColor = System.Drawing.Color.Transparent
        Me.GroupPanel4.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanel4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanel4.Controls.Add(Me.chkStaff)
        Me.GroupPanel4.Controls.Add(Me.chkTI)
        Me.GroupPanel4.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel4.Location = New System.Drawing.Point(12, 12)
        Me.GroupPanel4.Name = "GroupPanel4"
        Me.GroupPanel4.Size = New System.Drawing.Size(492, 79)
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
        Me.GroupPanel4.TabIndex = 23
        Me.GroupPanel4.Text = "Attendance of"
        '
        'chkStaff
        '
        Me.chkStaff.AutoSize = True
        '
        '
        '
        Me.chkStaff.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.chkStaff.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton
        Me.chkStaff.Checked = True
        Me.chkStaff.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkStaff.CheckValue = "Y"
        Me.chkStaff.Location = New System.Drawing.Point(24, 18)
        Me.chkStaff.Name = "chkStaff"
        Me.chkStaff.Size = New System.Drawing.Size(50, 18)
        Me.chkStaff.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.chkStaff.TabIndex = 1
        Me.chkStaff.Text = "Staff"
        '
        'chkTI
        '
        Me.chkTI.AutoSize = True
        '
        '
        '
        Me.chkTI.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.chkTI.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton
        Me.chkTI.Location = New System.Drawing.Point(187, 18)
        Me.chkTI.Name = "chkTI"
        Me.chkTI.Size = New System.Drawing.Size(113, 18)
        Me.chkTI.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.chkTI.TabIndex = 2
        Me.chkTI.Text = "Tester Inspector"
        '
        'bgwAttendance
        '
        Me.bgwAttendance.WorkerReportsProgress = True
        Me.bgwAttendance.WorkerSupportsCancellation = True
        '
        'frmAttendanceStmt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(514, 358)
        Me.Controls.Add(Me.PanelEx2)
        Me.DoubleBuffered = True
        Me.EnableGlass = False
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAttendanceStmt"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Attendance Statement"
        Me.TitleText = "<b>Attendance Statement</b>"
        Me.PanelEx2.ResumeLayout(False)
        Me.GroupPanel6.ResumeLayout(False)
        Me.GroupPanel6.PerformLayout()
        CType(Me.dtTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtFrom, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupPanel5.ResumeLayout(False)
        Me.GroupPanel5.PerformLayout()
        Me.GroupPanel4.ResumeLayout(False)
        Me.GroupPanel4.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PanelEx2 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents LabelX2 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents btnGenerateAttendance As DevComponents.DotNetBar.ButtonX
    Friend WithEvents dtFrom As DevComponents.Editors.DateTimeAdv.DateTimeInput
    Friend WithEvents dtTo As DevComponents.Editors.DateTimeAdv.DateTimeInput
    Friend WithEvents chkCoB As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents chkTI As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents chkStaff As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents chkStatementOnly As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents GroupPanel6 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents GroupPanel5 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents GroupPanel4 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents bgwAttendance As System.ComponentModel.BackgroundWorker
    Friend WithEvents CircularProgress1 As DevComponents.DotNetBar.Controls.CircularProgress
    Friend WithEvents btnGenerateCL As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnOpenFolder As DevComponents.DotNetBar.ButtonX
End Class
