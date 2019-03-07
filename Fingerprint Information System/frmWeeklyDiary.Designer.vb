<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmWeeklyDiary
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmWeeklyDiary))
        Me.LabelX3 = New DevComponents.DotNetBar.LabelX()
        Me.btnGenerateWeeklyDiary = New DevComponents.DotNetBar.ButtonX()
        Me.PanelEx1 = New DevComponents.DotNetBar.PanelEx()
        Me.cprgBackup = New DevComponents.DotNetBar.Controls.CircularProgress()
        Me.btnBackupAllFiles = New DevComponents.DotNetBar.ButtonX()
        Me.btnBackupSingleFile = New DevComponents.DotNetBar.ButtonItem()
        Me.lblBackup = New DevComponents.DotNetBar.LabelX()
        Me.CircularProgress1 = New DevComponents.DotNetBar.Controls.CircularProgress()
        Me.lblSelectedDate = New DevComponents.DotNetBar.LabelX()
        Me.MonthCalendarAdv1 = New DevComponents.Editors.DateTimeAdv.MonthCalendarAdv()
        Me.btnCoveringLetter = New DevComponents.DotNetBar.ButtonX()
        Me.btnOpenWeeklyDiaryFolder = New DevComponents.DotNetBar.ButtonX()
        Me.FingerPrintDataSet1 = New FingerprintInformationSystem.FingerPrintDataSet()
        Me.SocRegisterTableAdapter1 = New FingerprintInformationSystem.FingerPrintDataSetTableAdapters.SOCRegisterTableAdapter()
        Me.bgwWeeklyDiary = New System.ComponentModel.BackgroundWorker()
        Me.bgwUploadFile = New System.ComponentModel.BackgroundWorker()
        Me.bgwUploadAllFiles = New System.ComponentModel.BackgroundWorker()
        Me.PanelEx1.SuspendLayout()
        CType(Me.FingerPrintDataSet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelX3
        '
        Me.LabelX3.AutoSize = True
        '
        '
        '
        Me.LabelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX3.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX3.Location = New System.Drawing.Point(18, 31)
        Me.LabelX3.Name = "LabelX3"
        Me.LabelX3.Size = New System.Drawing.Size(107, 20)
        Me.LabelX3.TabIndex = 22
        Me.LabelX3.Text = "Week starting on"
        '
        'btnGenerateWeeklyDiary
        '
        Me.btnGenerateWeeklyDiary.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnGenerateWeeklyDiary.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnGenerateWeeklyDiary.Image = CType(resources.GetObject("btnGenerateWeeklyDiary.Image"), System.Drawing.Image)
        Me.btnGenerateWeeklyDiary.Location = New System.Drawing.Point(328, 9)
        Me.btnGenerateWeeklyDiary.Name = "btnGenerateWeeklyDiary"
        Me.btnGenerateWeeklyDiary.Size = New System.Drawing.Size(143, 44)
        Me.btnGenerateWeeklyDiary.TabIndex = 1
        Me.btnGenerateWeeklyDiary.Text = "Generate"
        '
        'PanelEx1
        '
        Me.PanelEx1.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.PanelEx1.Controls.Add(Me.cprgBackup)
        Me.PanelEx1.Controls.Add(Me.btnBackupAllFiles)
        Me.PanelEx1.Controls.Add(Me.lblBackup)
        Me.PanelEx1.Controls.Add(Me.CircularProgress1)
        Me.PanelEx1.Controls.Add(Me.lblSelectedDate)
        Me.PanelEx1.Controls.Add(Me.MonthCalendarAdv1)
        Me.PanelEx1.Controls.Add(Me.btnCoveringLetter)
        Me.PanelEx1.Controls.Add(Me.btnOpenWeeklyDiaryFolder)
        Me.PanelEx1.Controls.Add(Me.LabelX3)
        Me.PanelEx1.Controls.Add(Me.btnGenerateWeeklyDiary)
        Me.PanelEx1.DisabledBackColor = System.Drawing.Color.Empty
        Me.PanelEx1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelEx1.Location = New System.Drawing.Point(0, 0)
        Me.PanelEx1.Name = "PanelEx1"
        Me.PanelEx1.Size = New System.Drawing.Size(483, 212)
        Me.PanelEx1.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx1.Style.GradientAngle = 90
        Me.PanelEx1.TabIndex = 24
        '
        'cprgBackup
        '
        '
        '
        '
        Me.cprgBackup.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.cprgBackup.FocusCuesEnabled = False
        Me.cprgBackup.Location = New System.Drawing.Point(328, 159)
        Me.cprgBackup.Name = "cprgBackup"
        Me.cprgBackup.ProgressBarType = DevComponents.DotNetBar.eCircularProgressType.Donut
        Me.cprgBackup.ProgressColor = System.Drawing.Color.Red
        Me.cprgBackup.ProgressTextVisible = True
        Me.cprgBackup.Size = New System.Drawing.Size(143, 44)
        Me.cprgBackup.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP
        Me.cprgBackup.TabIndex = 49
        Me.cprgBackup.TabStop = False
        '
        'btnBackupAllFiles
        '
        Me.btnBackupAllFiles.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnBackupAllFiles.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnBackupAllFiles.Image = CType(resources.GetObject("btnBackupAllFiles.Image"), System.Drawing.Image)
        Me.btnBackupAllFiles.Location = New System.Drawing.Point(328, 159)
        Me.btnBackupAllFiles.Name = "btnBackupAllFiles"
        Me.btnBackupAllFiles.Size = New System.Drawing.Size(143, 44)
        Me.btnBackupAllFiles.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnBackupAllFiles.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnBackupSingleFile})
        Me.btnBackupAllFiles.TabIndex = 4
        Me.btnBackupAllFiles.Text = "Drive Backup"
        '
        'btnBackupSingleFile
        '
        Me.btnBackupSingleFile.GlobalItem = False
        Me.btnBackupSingleFile.Name = "btnBackupSingleFile"
        Me.btnBackupSingleFile.Text = "Backup Current Weekly Diary File"
        '
        'lblBackup
        '
        Me.lblBackup.AutoSize = True
        '
        '
        '
        Me.lblBackup.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblBackup.Location = New System.Drawing.Point(31, 172)
        Me.lblBackup.Name = "lblBackup"
        Me.lblBackup.Size = New System.Drawing.Size(244, 18)
        Me.lblBackup.TabIndex = 46
        Me.lblBackup.Text = "Backup Weekly Diary to your Google Drive"
        '
        'CircularProgress1
        '
        '
        '
        '
        Me.CircularProgress1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.CircularProgress1.FocusCuesEnabled = False
        Me.CircularProgress1.Location = New System.Drawing.Point(140, 9)
        Me.CircularProgress1.Name = "CircularProgress1"
        Me.CircularProgress1.ProgressColor = System.Drawing.Color.Red
        Me.CircularProgress1.ProgressTextVisible = True
        Me.CircularProgress1.Size = New System.Drawing.Size(170, 131)
        Me.CircularProgress1.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP
        Me.CircularProgress1.TabIndex = 37
        Me.CircularProgress1.TabStop = False
        '
        'lblSelectedDate
        '
        Me.lblSelectedDate.AutoSize = True
        '
        '
        '
        Me.lblSelectedDate.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblSelectedDate.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSelectedDate.ForeColor = System.Drawing.Color.Red
        Me.lblSelectedDate.Location = New System.Drawing.Point(18, 67)
        Me.lblSelectedDate.Name = "lblSelectedDate"
        Me.lblSelectedDate.Size = New System.Drawing.Size(38, 24)
        Me.lblSelectedDate.TabIndex = 36
        Me.lblSelectedDate.Text = "Date"
        '
        'MonthCalendarAdv1
        '
        Me.MonthCalendarAdv1.AutoSize = True
        '
        '
        '
        Me.MonthCalendarAdv1.BackgroundStyle.Class = "MonthCalendarAdv"
        Me.MonthCalendarAdv1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.MonthCalendarAdv1.Colors.Selection.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(128, Byte), Integer))
        '
        '
        '
        Me.MonthCalendarAdv1.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.MonthCalendarAdv1.ContainerControlProcessDialogKey = True
        Me.MonthCalendarAdv1.DisplayMonth = New Date(2018, 4, 1, 0, 0, 0, 0)
        Me.MonthCalendarAdv1.FirstDayOfWeek = System.DayOfWeek.Monday
        Me.MonthCalendarAdv1.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MonthCalendarAdv1.Location = New System.Drawing.Point(140, 9)
        Me.MonthCalendarAdv1.Name = "MonthCalendarAdv1"
        '
        '
        '
        Me.MonthCalendarAdv1.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2
        Me.MonthCalendarAdv1.NavigationBackgroundStyle.BackColorGradientAngle = 90
        Me.MonthCalendarAdv1.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.MonthCalendarAdv1.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.MonthCalendarAdv1.ShowTodayMarker = False
        Me.MonthCalendarAdv1.Size = New System.Drawing.Size(170, 131)
        Me.MonthCalendarAdv1.TabIndex = 35
        Me.MonthCalendarAdv1.TabStop = False
        '
        'btnCoveringLetter
        '
        Me.btnCoveringLetter.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnCoveringLetter.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnCoveringLetter.Image = CType(resources.GetObject("btnCoveringLetter.Image"), System.Drawing.Image)
        Me.btnCoveringLetter.Location = New System.Drawing.Point(328, 59)
        Me.btnCoveringLetter.Name = "btnCoveringLetter"
        Me.btnCoveringLetter.Size = New System.Drawing.Size(143, 44)
        Me.btnCoveringLetter.TabIndex = 2
        Me.btnCoveringLetter.Text = "Covering Letter"
        '
        'btnOpenWeeklyDiaryFolder
        '
        Me.btnOpenWeeklyDiaryFolder.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnOpenWeeklyDiaryFolder.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnOpenWeeklyDiaryFolder.Image = CType(resources.GetObject("btnOpenWeeklyDiaryFolder.Image"), System.Drawing.Image)
        Me.btnOpenWeeklyDiaryFolder.Location = New System.Drawing.Point(328, 109)
        Me.btnOpenWeeklyDiaryFolder.Name = "btnOpenWeeklyDiaryFolder"
        Me.btnOpenWeeklyDiaryFolder.Size = New System.Drawing.Size(143, 44)
        Me.btnOpenWeeklyDiaryFolder.TabIndex = 3
        Me.btnOpenWeeklyDiaryFolder.Text = "Open Folder"
        '
        'FingerPrintDataSet1
        '
        Me.FingerPrintDataSet1.DataSetName = "FingerPrintDataSet"
        Me.FingerPrintDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'SocRegisterTableAdapter1
        '
        Me.SocRegisterTableAdapter1.ClearBeforeFill = True
        '
        'bgwWeeklyDiary
        '
        Me.bgwWeeklyDiary.WorkerReportsProgress = True
        Me.bgwWeeklyDiary.WorkerSupportsCancellation = True
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
        'frmWeeklyDiary
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(483, 212)
        Me.Controls.Add(Me.PanelEx1)
        Me.DoubleBuffered = True
        Me.EnableGlass = False
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmWeeklyDiary"
        Me.ShowInTaskbar = False
        Me.Text = "Weekly Diary"
        Me.TitleText = "<b>Weekly Diary</b>"
        Me.PanelEx1.ResumeLayout(False)
        Me.PanelEx1.PerformLayout()
        CType(Me.FingerPrintDataSet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LabelX3 As DevComponents.DotNetBar.LabelX
    Friend WithEvents btnGenerateWeeklyDiary As DevComponents.DotNetBar.ButtonX
    Friend WithEvents PanelEx1 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents btnOpenWeeklyDiaryFolder As DevComponents.DotNetBar.ButtonX
    Friend WithEvents FingerPrintDataSet1 As FingerprintInformationSystem.FingerPrintDataSet
    Friend WithEvents SocRegisterTableAdapter1 As FingerprintInformationSystem.FingerPrintDataSetTableAdapters.SOCRegisterTableAdapter
    Friend WithEvents btnCoveringLetter As DevComponents.DotNetBar.ButtonX
    Friend WithEvents MonthCalendarAdv1 As DevComponents.Editors.DateTimeAdv.MonthCalendarAdv
    Friend WithEvents lblSelectedDate As DevComponents.DotNetBar.LabelX
    Friend WithEvents bgwWeeklyDiary As System.ComponentModel.BackgroundWorker
    Friend WithEvents CircularProgress1 As DevComponents.DotNetBar.Controls.CircularProgress
    Friend WithEvents btnBackupAllFiles As DevComponents.DotNetBar.ButtonX
    Friend WithEvents cprgBackup As DevComponents.DotNetBar.Controls.CircularProgress
    Friend WithEvents lblBackup As DevComponents.DotNetBar.LabelX
    Friend WithEvents bgwUploadFile As System.ComponentModel.BackgroundWorker
    Friend WithEvents btnBackupSingleFile As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents bgwUploadAllFiles As System.ComponentModel.BackgroundWorker
End Class
