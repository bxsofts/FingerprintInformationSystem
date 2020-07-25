<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChalanDetails
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmChalanDetails))
        Me.txtChalanNumber = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX5 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX4 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX1 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX2 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX3 = New DevComponents.DotNetBar.LabelX()
        Me.txtTreasury = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.txtAmount = New DevComponents.Editors.IntegerInput()
        Me.txtHeadOfAccount = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.dtChalanDate = New DevComponents.Editors.DateTimeAdv.DateTimeInput()
        Me.LabelX62 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX66 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX67 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX94 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX161 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX162 = New DevComponents.DotNetBar.LabelX()
        Me.btnClose = New DevComponents.DotNetBar.ButtonX()
        Me.btnClearFields = New DevComponents.DotNetBar.ButtonX()
        Me.btnAddToList = New DevComponents.DotNetBar.ButtonX()
        Me.FPARegisterAutoTextTableAdapter1 = New FingerprintInformationSystem.FingerPrintDataSetTableAdapters.FPRegisterAutoTextTableAdapter()
        Me.FingerPrintDataSet1 = New FingerprintInformationSystem.FingerPrintDataSet()
        CType(Me.txtAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtChalanDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FingerPrintDataSet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtChalanNumber
        '
        Me.txtChalanNumber.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.txtChalanNumber.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtChalanNumber.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtChalanNumber.Border.Class = "TextBoxBorder"
        Me.txtChalanNumber.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtChalanNumber.ButtonCustom.Image = CType(resources.GetObject("txtChalanNumber.ButtonCustom.Image"), System.Drawing.Image)
        Me.txtChalanNumber.ButtonCustom.Visible = True
        Me.txtChalanNumber.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtChalanNumber.DisabledBackColor = System.Drawing.Color.White
        Me.txtChalanNumber.FocusHighlightEnabled = True
        Me.txtChalanNumber.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtChalanNumber.ForeColor = System.Drawing.Color.Black
        Me.txtChalanNumber.Location = New System.Drawing.Point(75, 9)
        Me.txtChalanNumber.MaxLength = 255
        Me.txtChalanNumber.Name = "txtChalanNumber"
        Me.txtChalanNumber.Size = New System.Drawing.Size(227, 25)
        Me.txtChalanNumber.TabIndex = 232
        Me.txtChalanNumber.WatermarkBehavior = DevComponents.DotNetBar.eWatermarkBehavior.HideNonEmpty
        Me.txtChalanNumber.WatermarkText = "Chalan Number"
        '
        'LabelX5
        '
        Me.LabelX5.AutoSize = True
        '
        '
        '
        Me.LabelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX5.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX5.Location = New System.Drawing.Point(171, 139)
        Me.LabelX5.Name = "LabelX5"
        Me.LabelX5.Size = New System.Drawing.Size(7, 22)
        Me.LabelX5.TabIndex = 247
        Me.LabelX5.Text = "<font color=""#ED1C24"">*</font><b></b>"
        '
        'LabelX4
        '
        Me.LabelX4.AutoSize = True
        '
        '
        '
        Me.LabelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX4.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX4.Location = New System.Drawing.Point(308, 106)
        Me.LabelX4.Name = "LabelX4"
        Me.LabelX4.Size = New System.Drawing.Size(7, 22)
        Me.LabelX4.TabIndex = 246
        Me.LabelX4.Text = "<font color=""#ED1C24"">*</font><b></b>"
        '
        'LabelX1
        '
        Me.LabelX1.AutoSize = True
        '
        '
        '
        Me.LabelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX1.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX1.Location = New System.Drawing.Point(308, 77)
        Me.LabelX1.Name = "LabelX1"
        Me.LabelX1.Size = New System.Drawing.Size(7, 22)
        Me.LabelX1.TabIndex = 245
        Me.LabelX1.Text = "<font color=""#ED1C24"">*</font><b></b>"
        '
        'LabelX2
        '
        Me.LabelX2.AutoSize = True
        '
        '
        '
        Me.LabelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX2.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX2.Location = New System.Drawing.Point(308, 45)
        Me.LabelX2.Name = "LabelX2"
        Me.LabelX2.Size = New System.Drawing.Size(7, 22)
        Me.LabelX2.TabIndex = 244
        Me.LabelX2.Text = "<font color=""#ED1C24"">*</font><b></b>"
        '
        'LabelX3
        '
        Me.LabelX3.AutoSize = True
        '
        '
        '
        Me.LabelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX3.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX3.Location = New System.Drawing.Point(308, 14)
        Me.LabelX3.Name = "LabelX3"
        Me.LabelX3.Size = New System.Drawing.Size(7, 22)
        Me.LabelX3.TabIndex = 243
        Me.LabelX3.Text = "<font color=""#ED1C24"">*</font><b></b>"
        '
        'txtTreasury
        '
        Me.txtTreasury.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.txtTreasury.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtTreasury.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtTreasury.Border.Class = "TextBoxBorder"
        Me.txtTreasury.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtTreasury.ButtonCustom.Image = CType(resources.GetObject("txtTreasury.ButtonCustom.Image"), System.Drawing.Image)
        Me.txtTreasury.ButtonCustom.Visible = True
        Me.txtTreasury.DisabledBackColor = System.Drawing.Color.White
        Me.txtTreasury.FocusHighlightEnabled = True
        Me.txtTreasury.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTreasury.ForeColor = System.Drawing.Color.Black
        Me.txtTreasury.Location = New System.Drawing.Point(75, 102)
        Me.txtTreasury.MaxLength = 255
        Me.txtTreasury.Name = "txtTreasury"
        Me.txtTreasury.Size = New System.Drawing.Size(227, 25)
        Me.txtTreasury.TabIndex = 235
        Me.txtTreasury.WatermarkBehavior = DevComponents.DotNetBar.eWatermarkBehavior.HideNonEmpty
        Me.txtTreasury.WatermarkText = "Treasury"
        '
        'txtAmount
        '
        '
        '
        '
        Me.txtAmount.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.txtAmount.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtAmount.FocusHighlightEnabled = True
        Me.txtAmount.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmount.InputHorizontalAlignment = DevComponents.Editors.eHorizontalAlignment.Left
        Me.txtAmount.Location = New System.Drawing.Point(75, 133)
        Me.txtAmount.MaxValue = 9999
        Me.txtAmount.MinValue = 0
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.ShowUpDown = True
        Me.txtAmount.Size = New System.Drawing.Size(91, 25)
        Me.txtAmount.TabIndex = 236
        Me.txtAmount.WatermarkText = "Amount"
        '
        'txtHeadOfAccount
        '
        Me.txtHeadOfAccount.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.txtHeadOfAccount.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtHeadOfAccount.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtHeadOfAccount.Border.Class = "TextBoxBorder"
        Me.txtHeadOfAccount.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtHeadOfAccount.ButtonCustom.Image = CType(resources.GetObject("txtHeadOfAccount.ButtonCustom.Image"), System.Drawing.Image)
        Me.txtHeadOfAccount.ButtonCustom.Visible = True
        Me.txtHeadOfAccount.DisabledBackColor = System.Drawing.Color.White
        Me.txtHeadOfAccount.FocusHighlightEnabled = True
        Me.txtHeadOfAccount.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHeadOfAccount.ForeColor = System.Drawing.Color.Black
        Me.txtHeadOfAccount.Location = New System.Drawing.Point(75, 71)
        Me.txtHeadOfAccount.MaxLength = 255
        Me.txtHeadOfAccount.Name = "txtHeadOfAccount"
        Me.txtHeadOfAccount.Size = New System.Drawing.Size(227, 25)
        Me.txtHeadOfAccount.TabIndex = 234
        Me.txtHeadOfAccount.WatermarkBehavior = DevComponents.DotNetBar.eWatermarkBehavior.HideNonEmpty
        Me.txtHeadOfAccount.WatermarkText = "Head of Account"
        '
        'dtChalanDate
        '
        Me.dtChalanDate.AutoAdvance = True
        Me.dtChalanDate.AutoSelectDate = True
        Me.dtChalanDate.BackColor = System.Drawing.SystemColors.Window
        '
        '
        '
        Me.dtChalanDate.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.dtChalanDate.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.dtChalanDate.ButtonClear.Image = CType(resources.GetObject("dtChalanDate.ButtonClear.Image"), System.Drawing.Image)
        Me.dtChalanDate.ButtonClear.Visible = True
        Me.dtChalanDate.ButtonDropDown.Visible = True
        Me.dtChalanDate.CustomFormat = "dd/MM/yyyy"
        Me.dtChalanDate.FocusHighlightEnabled = True
        Me.dtChalanDate.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtChalanDate.Format = DevComponents.Editors.eDateTimePickerFormat.Custom
        Me.dtChalanDate.IsPopupCalendarOpen = False
        Me.dtChalanDate.Location = New System.Drawing.Point(75, 40)
        Me.dtChalanDate.MaxDate = New Date(2100, 12, 31, 0, 0, 0, 0)
        Me.dtChalanDate.MinDate = New Date(1900, 1, 1, 0, 0, 0, 0)
        '
        '
        '
        '
        '
        '
        Me.dtChalanDate.MonthCalendar.BackgroundStyle.BackColor = System.Drawing.SystemColors.Window
        Me.dtChalanDate.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.dtChalanDate.MonthCalendar.ClearButtonVisible = True
        '
        '
        '
        Me.dtChalanDate.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2
        Me.dtChalanDate.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90
        Me.dtChalanDate.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.dtChalanDate.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.dtChalanDate.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder
        Me.dtChalanDate.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1
        Me.dtChalanDate.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.dtChalanDate.MonthCalendar.DaySize = New System.Drawing.Size(30, 15)
        Me.dtChalanDate.MonthCalendar.DisplayMonth = New Date(2008, 7, 1, 0, 0, 0, 0)
        Me.dtChalanDate.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday
        '
        '
        '
        Me.dtChalanDate.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.dtChalanDate.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90
        Me.dtChalanDate.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.dtChalanDate.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.dtChalanDate.MonthCalendar.TodayButtonVisible = True
        Me.dtChalanDate.Name = "dtChalanDate"
        Me.dtChalanDate.Size = New System.Drawing.Size(227, 25)
        Me.dtChalanDate.TabIndex = 233
        Me.dtChalanDate.WatermarkText = "Chalan Date"
        '
        'LabelX62
        '
        Me.LabelX62.AutoSize = True
        '
        '
        '
        Me.LabelX62.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX62.Location = New System.Drawing.Point(-1, 12)
        Me.LabelX62.Name = "LabelX62"
        Me.LabelX62.Size = New System.Drawing.Size(64, 18)
        Me.LabelX62.TabIndex = 237
        Me.LabelX62.Text = "Chalan No."
        '
        'LabelX66
        '
        Me.LabelX66.AutoSize = True
        '
        '
        '
        Me.LabelX66.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX66.Location = New System.Drawing.Point(-1, 105)
        Me.LabelX66.Name = "LabelX66"
        Me.LabelX66.Size = New System.Drawing.Size(51, 18)
        Me.LabelX66.TabIndex = 238
        Me.LabelX66.Text = "Treasury"
        '
        'LabelX67
        '
        Me.LabelX67.AutoSize = True
        '
        '
        '
        Me.LabelX67.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX67.Location = New System.Drawing.Point(-1, 136)
        Me.LabelX67.Name = "LabelX67"
        Me.LabelX67.Size = New System.Drawing.Size(48, 18)
        Me.LabelX67.TabIndex = 239
        Me.LabelX67.Text = "Amount"
        '
        'LabelX94
        '
        Me.LabelX94.AutoSize = True
        '
        '
        '
        Me.LabelX94.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX94.Font = New System.Drawing.Font("Rupee Foradian", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX94.Location = New System.Drawing.Point(51, 138)
        Me.LabelX94.Name = "LabelX94"
        Me.LabelX94.Size = New System.Drawing.Size(9, 16)
        Me.LabelX94.TabIndex = 240
        Me.LabelX94.Text = "`"
        '
        'LabelX161
        '
        Me.LabelX161.AutoSize = True
        '
        '
        '
        Me.LabelX161.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX161.Location = New System.Drawing.Point(-1, 43)
        Me.LabelX161.Name = "LabelX161"
        Me.LabelX161.Size = New System.Drawing.Size(71, 18)
        Me.LabelX161.TabIndex = 241
        Me.LabelX161.Text = "Chalan Date"
        '
        'LabelX162
        '
        Me.LabelX162.AutoSize = True
        '
        '
        '
        Me.LabelX162.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX162.Location = New System.Drawing.Point(-1, 74)
        Me.LabelX162.Name = "LabelX162"
        Me.LabelX162.Size = New System.Drawing.Size(32, 18)
        Me.LabelX162.TabIndex = 242
        Me.LabelX162.Text = "Head"
        '
        'btnClose
        '
        Me.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnClose.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnClose.Location = New System.Drawing.Point(351, 95)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(105, 34)
        Me.btnClose.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnClose.TabIndex = 250
        Me.btnClose.Text = "Close"
        '
        'btnClearFields
        '
        Me.btnClearFields.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnClearFields.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnClearFields.Location = New System.Drawing.Point(351, 52)
        Me.btnClearFields.Name = "btnClearFields"
        Me.btnClearFields.Size = New System.Drawing.Size(105, 34)
        Me.btnClearFields.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnClearFields.TabIndex = 249
        Me.btnClearFields.Text = "Clear Fields"
        '
        'btnAddToList
        '
        Me.btnAddToList.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnAddToList.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnAddToList.Location = New System.Drawing.Point(351, 9)
        Me.btnAddToList.Name = "btnAddToList"
        Me.btnAddToList.Size = New System.Drawing.Size(105, 34)
        Me.btnAddToList.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnAddToList.TabIndex = 248
        Me.btnAddToList.Text = "Add to List"
        '
        'FPARegisterAutoTextTableAdapter1
        '
        Me.FPARegisterAutoTextTableAdapter1.ClearBeforeFill = True
        '
        'FingerPrintDataSet1
        '
        Me.FingerPrintDataSet1.DataSetName = "FingerPrintDataSet"
        Me.FingerPrintDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'frmChalanDetails
        '
        Me.AcceptButton = Me.btnAddToList
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(465, 158)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnClearFields)
        Me.Controls.Add(Me.btnAddToList)
        Me.Controls.Add(Me.txtChalanNumber)
        Me.Controls.Add(Me.LabelX5)
        Me.Controls.Add(Me.LabelX4)
        Me.Controls.Add(Me.LabelX1)
        Me.Controls.Add(Me.LabelX2)
        Me.Controls.Add(Me.LabelX3)
        Me.Controls.Add(Me.txtTreasury)
        Me.Controls.Add(Me.txtAmount)
        Me.Controls.Add(Me.txtHeadOfAccount)
        Me.Controls.Add(Me.dtChalanDate)
        Me.Controls.Add(Me.LabelX62)
        Me.Controls.Add(Me.LabelX66)
        Me.Controls.Add(Me.LabelX67)
        Me.Controls.Add(Me.LabelX94)
        Me.Controls.Add(Me.LabelX161)
        Me.Controls.Add(Me.LabelX162)
        Me.DoubleBuffered = True
        Me.EnableGlass = False
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmChalanDetails"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Chalan Details"
        Me.TitleText = "<b>Chalan Details</b>"
        CType(Me.txtAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtChalanDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FingerPrintDataSet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtChalanNumber As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX5 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX4 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX2 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX3 As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtTreasury As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents txtAmount As DevComponents.Editors.IntegerInput
    Friend WithEvents txtHeadOfAccount As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents dtChalanDate As DevComponents.Editors.DateTimeAdv.DateTimeInput
    Friend WithEvents LabelX62 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX66 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX67 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX94 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX161 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX162 As DevComponents.DotNetBar.LabelX
    Friend WithEvents btnClose As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnClearFields As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnAddToList As DevComponents.DotNetBar.ButtonX
    Friend WithEvents FPARegisterAutoTextTableAdapter1 As FingerprintInformationSystem.FingerPrintDataSetTableAdapters.FPRegisterAutoTextTableAdapter
    Friend WithEvents FingerPrintDataSet1 As FingerprintInformationSystem.FingerPrintDataSet
End Class
