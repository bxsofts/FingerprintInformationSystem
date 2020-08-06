<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOnlineSendStatements
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmOnlineSendStatements))
        Me.PanelEx1 = New DevComponents.DotNetBar.PanelEx()
        Me.ListViewEx1 = New DevComponents.DotNetBar.Controls.ListViewEx()
        Me.District = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.FolderID = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.CircularProgress1 = New DevComponents.DotNetBar.Controls.CircularProgress()
        Me.btnSend = New DevComponents.DotNetBar.ButtonX()
        Me.LabelX4 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX3 = New DevComponents.DotNetBar.LabelX()
        Me.txtYear = New DevComponents.Editors.IntegerInput()
        Me.cmbMonth = New DevComponents.DotNetBar.Controls.ComboBoxEx()
        Me.chkPerf = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.chkID = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.chkGrave = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.chkSOC = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.bgwListFiles = New System.ComponentModel.BackgroundWorker()
        Me.bgwUploadFile = New System.ComponentModel.BackgroundWorker()
        Me.bgwUpdateFileContent = New System.ComponentModel.BackgroundWorker()
        Me.PanelEx1.SuspendLayout()
        CType(Me.txtYear, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelEx1
        '
        Me.PanelEx1.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.PanelEx1.Controls.Add(Me.ListViewEx1)
        Me.PanelEx1.Controls.Add(Me.CircularProgress1)
        Me.PanelEx1.Controls.Add(Me.btnSend)
        Me.PanelEx1.Controls.Add(Me.LabelX4)
        Me.PanelEx1.Controls.Add(Me.LabelX3)
        Me.PanelEx1.Controls.Add(Me.txtYear)
        Me.PanelEx1.Controls.Add(Me.cmbMonth)
        Me.PanelEx1.Controls.Add(Me.chkPerf)
        Me.PanelEx1.Controls.Add(Me.chkID)
        Me.PanelEx1.Controls.Add(Me.chkGrave)
        Me.PanelEx1.Controls.Add(Me.chkSOC)
        Me.PanelEx1.DisabledBackColor = System.Drawing.Color.Empty
        Me.PanelEx1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelEx1.Location = New System.Drawing.Point(0, 0)
        Me.PanelEx1.Name = "PanelEx1"
        Me.PanelEx1.Size = New System.Drawing.Size(691, 410)
        Me.PanelEx1.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx1.Style.GradientAngle = 90
        Me.PanelEx1.TabIndex = 0
        '
        'ListViewEx1
        '
        Me.ListViewEx1.Alignment = System.Windows.Forms.ListViewAlignment.Left
        Me.ListViewEx1.AllowDrop = True
        Me.ListViewEx1.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.ListViewEx1.Border.Class = "ListViewBorder"
        Me.ListViewEx1.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.ListViewEx1.ColumnHeaderFont = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListViewEx1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.District, Me.FolderID})
        Me.ListViewEx1.DisabledBackColor = System.Drawing.Color.Empty
        Me.ListViewEx1.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListViewEx1.ForeColor = System.Drawing.Color.Black
        Me.ListViewEx1.FullRowSelect = True
        Me.ListViewEx1.GridLines = True
        Me.ListViewEx1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.ListViewEx1.HideSelection = False
        Me.ListViewEx1.Location = New System.Drawing.Point(292, 10)
        Me.ListViewEx1.MultiSelect = False
        Me.ListViewEx1.Name = "ListViewEx1"
        Me.ListViewEx1.ShowItemToolTips = True
        Me.ListViewEx1.Size = New System.Drawing.Size(262, 392)
        Me.ListViewEx1.TabIndex = 50
        Me.ListViewEx1.UseCompatibleStateImageBehavior = False
        Me.ListViewEx1.View = System.Windows.Forms.View.Details
        '
        'District
        '
        Me.District.Text = "District"
        Me.District.Width = 250
        '
        'FolderID
        '
        Me.FolderID.Text = "ID"
        Me.FolderID.Width = 0
        '
        'CircularProgress1
        '
        '
        '
        '
        Me.CircularProgress1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.CircularProgress1.Dock = System.Windows.Forms.DockStyle.Right
        Me.CircularProgress1.FocusCuesEnabled = False
        Me.CircularProgress1.Location = New System.Drawing.Point(560, 0)
        Me.CircularProgress1.Name = "CircularProgress1"
        Me.CircularProgress1.ProgressColor = System.Drawing.Color.Red
        Me.CircularProgress1.ProgressTextVisible = True
        Me.CircularProgress1.Size = New System.Drawing.Size(131, 410)
        Me.CircularProgress1.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP
        Me.CircularProgress1.TabIndex = 49
        Me.CircularProgress1.TabStop = False
        '
        'btnSend
        '
        Me.btnSend.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnSend.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnSend.Location = New System.Drawing.Point(572, 177)
        Me.btnSend.Name = "btnSend"
        Me.btnSend.Size = New System.Drawing.Size(101, 56)
        Me.btnSend.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnSend.TabIndex = 35
        Me.btnSend.Text = "Send"
        '
        'LabelX4
        '
        Me.LabelX4.AutoSize = True
        '
        '
        '
        Me.LabelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX4.Location = New System.Drawing.Point(188, 15)
        Me.LabelX4.Name = "LabelX4"
        Me.LabelX4.Size = New System.Drawing.Size(28, 18)
        Me.LabelX4.TabIndex = 34
        Me.LabelX4.Text = "Year"
        '
        'LabelX3
        '
        Me.LabelX3.AutoSize = True
        '
        '
        '
        Me.LabelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX3.Location = New System.Drawing.Point(10, 15)
        Me.LabelX3.Name = "LabelX3"
        Me.LabelX3.Size = New System.Drawing.Size(40, 18)
        Me.LabelX3.TabIndex = 33
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
        Me.txtYear.Location = New System.Drawing.Point(222, 10)
        Me.txtYear.MaxValue = 2099
        Me.txtYear.MinValue = 0
        Me.txtYear.Name = "txtYear"
        Me.txtYear.ShowUpDown = True
        Me.txtYear.Size = New System.Drawing.Size(64, 29)
        Me.txtYear.TabIndex = 32
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
        Me.cmbMonth.Location = New System.Drawing.Point(65, 10)
        Me.cmbMonth.MaxDropDownItems = 15
        Me.cmbMonth.MaxLength = 255
        Me.cmbMonth.Name = "cmbMonth"
        Me.cmbMonth.Size = New System.Drawing.Size(110, 29)
        Me.cmbMonth.TabIndex = 31
        Me.cmbMonth.WatermarkText = "Month"
        '
        'chkPerf
        '
        Me.chkPerf.AutoSize = True
        '
        '
        '
        Me.chkPerf.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.chkPerf.Location = New System.Drawing.Point(65, 189)
        Me.chkPerf.Name = "chkPerf"
        Me.chkPerf.Size = New System.Drawing.Size(87, 18)
        Me.chkPerf.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.chkPerf.TabIndex = 4
        Me.chkPerf.Text = "Work Done"
        '
        'chkID
        '
        Me.chkID.AutoSize = True
        '
        '
        '
        Me.chkID.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.chkID.Location = New System.Drawing.Point(65, 150)
        Me.chkID.Name = "chkID"
        Me.chkID.Size = New System.Drawing.Size(98, 18)
        Me.chkID.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.chkID.TabIndex = 3
        Me.chkID.Text = "Identification"
        '
        'chkGrave
        '
        Me.chkGrave.AutoSize = True
        '
        '
        '
        Me.chkGrave.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.chkGrave.Location = New System.Drawing.Point(65, 111)
        Me.chkGrave.Name = "chkGrave"
        Me.chkGrave.Size = New System.Drawing.Size(92, 18)
        Me.chkGrave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.chkGrave.TabIndex = 2
        Me.chkGrave.Text = "Grave Crime"
        '
        'chkSOC
        '
        Me.chkSOC.AutoSize = True
        '
        '
        '
        Me.chkSOC.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.chkSOC.Location = New System.Drawing.Point(65, 72)
        Me.chkSOC.Name = "chkSOC"
        Me.chkSOC.Size = New System.Drawing.Size(108, 18)
        Me.chkSOC.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.chkSOC.TabIndex = 1
        Me.chkSOC.Text = "SOC Statement"
        '
        'bgwListFiles
        '
        Me.bgwListFiles.WorkerReportsProgress = True
        Me.bgwListFiles.WorkerSupportsCancellation = True
        '
        'bgwUploadFile
        '
        Me.bgwUploadFile.WorkerReportsProgress = True
        Me.bgwUploadFile.WorkerSupportsCancellation = True
        '
        'bgwUpdateFileContent
        '
        Me.bgwUpdateFileContent.WorkerReportsProgress = True
        Me.bgwUpdateFileContent.WorkerSupportsCancellation = True
        '
        'frmOnlineSendStatements
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(691, 410)
        Me.Controls.Add(Me.PanelEx1)
        Me.DoubleBuffered = True
        Me.EnableGlass = False
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmOnlineSendStatements"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Send Statements to Range TI"
        Me.TitleText = "<b>Send Statements to Range TI</b>"
        Me.PanelEx1.ResumeLayout(False)
        Me.PanelEx1.PerformLayout()
        CType(Me.txtYear, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PanelEx1 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents chkPerf As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents chkID As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents chkGrave As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents chkSOC As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents LabelX4 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX3 As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtYear As DevComponents.Editors.IntegerInput
    Friend WithEvents cmbMonth As DevComponents.DotNetBar.Controls.ComboBoxEx
    Friend WithEvents btnSend As DevComponents.DotNetBar.ButtonX
    Friend WithEvents CircularProgress1 As DevComponents.DotNetBar.Controls.CircularProgress
    Friend WithEvents bgwListFiles As System.ComponentModel.BackgroundWorker
    Friend WithEvents bgwUploadFile As System.ComponentModel.BackgroundWorker
    Friend WithEvents bgwUpdateFileContent As System.ComponentModel.BackgroundWorker
    Private WithEvents ListViewEx1 As DevComponents.DotNetBar.Controls.ListViewEx
    Private WithEvents District As System.Windows.Forms.ColumnHeader
    Friend WithEvents FolderID As System.Windows.Forms.ColumnHeader
End Class
