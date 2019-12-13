<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRevenueCollection
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRevenueCollection))
        Me.txtYear = New DevComponents.Editors.IntegerInput()
        Me.LabelX3 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX4 = New DevComponents.DotNetBar.LabelX()
        Me.cmbMonth = New DevComponents.DotNetBar.Controls.ComboBoxEx()
        Me.btnOpenFolder = New DevComponents.DotNetBar.ButtonX()
        Me.FingerPrintDataSet = New FingerprintInformationSystem.FingerPrintDataSet()
        Me.FPAttestationRegisterBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.FPARegisterTableAdapter = New FingerprintInformationSystem.FingerPrintDataSetTableAdapters.FPAttestationRegisterTableAdapter()
        Me.LabelX1 = New DevComponents.DotNetBar.LabelX()
        Me.txtHeadofAccount = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.btnGenerateExcel = New DevComponents.DotNetBar.ButtonX()
        Me.LabelX2 = New DevComponents.DotNetBar.LabelX()
        Me.ChalanTableTableAdapter1 = New FingerprintInformationSystem.FingerPrintDataSetTableAdapters.ChalanTableTableAdapter()
        CType(Me.txtYear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FingerPrintDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FPAttestationRegisterBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.txtYear.Location = New System.Drawing.Point(313, 8)
        Me.txtYear.MaxValue = 2099
        Me.txtYear.MinValue = 0
        Me.txtYear.Name = "txtYear"
        Me.txtYear.ShowUpDown = True
        Me.txtYear.Size = New System.Drawing.Size(73, 29)
        Me.txtYear.TabIndex = 1
        Me.txtYear.Value = 1900
        Me.txtYear.WatermarkText = "Year"
        '
        'LabelX3
        '
        Me.LabelX3.AutoSize = True
        '
        '
        '
        Me.LabelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX3.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX3.Location = New System.Drawing.Point(6, 12)
        Me.LabelX3.Name = "LabelX3"
        Me.LabelX3.Size = New System.Drawing.Size(99, 20)
        Me.LabelX3.TabIndex = 28
        Me.LabelX3.Text = "Selected Month"
        '
        'LabelX4
        '
        Me.LabelX4.AutoSize = True
        '
        '
        '
        Me.LabelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX4.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX4.Location = New System.Drawing.Point(277, 13)
        Me.LabelX4.Name = "LabelX4"
        Me.LabelX4.Size = New System.Drawing.Size(30, 20)
        Me.LabelX4.TabIndex = 29
        Me.LabelX4.Text = "Year"
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
        Me.cmbMonth.Location = New System.Drawing.Point(135, 8)
        Me.cmbMonth.MaxDropDownItems = 15
        Me.cmbMonth.MaxLength = 255
        Me.cmbMonth.Name = "cmbMonth"
        Me.cmbMonth.Size = New System.Drawing.Size(136, 29)
        Me.cmbMonth.TabIndex = 0
        Me.cmbMonth.WatermarkText = "Month"
        '
        'btnOpenFolder
        '
        Me.btnOpenFolder.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnOpenFolder.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnOpenFolder.Location = New System.Drawing.Point(268, 85)
        Me.btnOpenFolder.Name = "btnOpenFolder"
        Me.btnOpenFolder.Size = New System.Drawing.Size(118, 50)
        Me.btnOpenFolder.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnOpenFolder.TabIndex = 4
        Me.btnOpenFolder.Text = "Open Folder"
        '
        'FingerPrintDataSet
        '
        Me.FingerPrintDataSet.DataSetName = "FingerPrintDataSet"
        Me.FingerPrintDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'FPAttestationRegisterBindingSource
        '
        Me.FPAttestationRegisterBindingSource.DataMember = "FPAttestationRegister"
        Me.FPAttestationRegisterBindingSource.DataSource = Me.FingerPrintDataSet
        '
        'FPARegisterTableAdapter
        '
        Me.FPARegisterTableAdapter.ClearBeforeFill = True
        '
        'LabelX1
        '
        Me.LabelX1.AutoSize = True
        '
        '
        '
        Me.LabelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX1.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX1.Location = New System.Drawing.Point(6, 50)
        Me.LabelX1.Name = "LabelX1"
        Me.LabelX1.Size = New System.Drawing.Size(105, 20)
        Me.LabelX1.TabIndex = 31
        Me.LabelX1.Text = "Head of Account"
        '
        'txtHeadofAccount
        '
        Me.txtHeadofAccount.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtHeadofAccount.Border.Class = "TextBoxBorder"
        Me.txtHeadofAccount.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtHeadofAccount.DisabledBackColor = System.Drawing.Color.White
        Me.txtHeadofAccount.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHeadofAccount.ForeColor = System.Drawing.Color.Black
        Me.txtHeadofAccount.Location = New System.Drawing.Point(135, 46)
        Me.txtHeadofAccount.Name = "txtHeadofAccount"
        Me.txtHeadofAccount.PreventEnterBeep = True
        Me.txtHeadofAccount.Size = New System.Drawing.Size(251, 29)
        Me.txtHeadofAccount.TabIndex = 2
        '
        'btnGenerateExcel
        '
        Me.btnGenerateExcel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnGenerateExcel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnGenerateExcel.Location = New System.Drawing.Point(135, 85)
        Me.btnGenerateExcel.Name = "btnGenerateExcel"
        Me.btnGenerateExcel.Size = New System.Drawing.Size(118, 50)
        Me.btnGenerateExcel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnGenerateExcel.TabIndex = 3
        Me.btnGenerateExcel.Text = "Excel Format"
        '
        'LabelX2
        '
        Me.LabelX2.AutoSize = True
        '
        '
        '
        Me.LabelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX2.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX2.Location = New System.Drawing.Point(6, 100)
        Me.LabelX2.Name = "LabelX2"
        Me.LabelX2.Size = New System.Drawing.Size(125, 20)
        Me.LabelX2.TabIndex = 34
        Me.LabelX2.Text = "Generate Statement"
        '
        'ChalanTableTableAdapter1
        '
        Me.ChalanTableTableAdapter1.ClearBeforeFill = True
        '
        'frmRevenueCollection
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(389, 138)
        Me.Controls.Add(Me.LabelX2)
        Me.Controls.Add(Me.btnGenerateExcel)
        Me.Controls.Add(Me.txtHeadofAccount)
        Me.Controls.Add(Me.LabelX1)
        Me.Controls.Add(Me.btnOpenFolder)
        Me.Controls.Add(Me.txtYear)
        Me.Controls.Add(Me.LabelX3)
        Me.Controls.Add(Me.LabelX4)
        Me.Controls.Add(Me.cmbMonth)
        Me.DoubleBuffered = True
        Me.EnableGlass = False
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmRevenueCollection"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Revenue Collection Statement"
        Me.TitleText = "<b>Revenue Collection Statement</b>"
        CType(Me.txtYear, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FingerPrintDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FPAttestationRegisterBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtYear As DevComponents.Editors.IntegerInput
    Friend WithEvents LabelX3 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX4 As DevComponents.DotNetBar.LabelX
    Friend WithEvents cmbMonth As DevComponents.DotNetBar.Controls.ComboBoxEx
    Friend WithEvents btnOpenFolder As DevComponents.DotNetBar.ButtonX
    Friend WithEvents FingerPrintDataSet As FingerprintInformationSystem.FingerPrintDataSet
    Friend WithEvents FPAttestationRegisterBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents FPARegisterTableAdapter As FingerprintInformationSystem.FingerPrintDataSetTableAdapters.FPAttestationRegisterTableAdapter
    Friend WithEvents LabelX1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtHeadofAccount As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents btnGenerateExcel As DevComponents.DotNetBar.ButtonX
    Friend WithEvents LabelX2 As DevComponents.DotNetBar.LabelX
    Friend WithEvents ChalanTableTableAdapter1 As FingerprintInformationSystem.FingerPrintDataSetTableAdapters.ChalanTableTableAdapter
End Class
