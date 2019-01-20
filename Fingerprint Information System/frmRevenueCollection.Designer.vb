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
        Me.btnGenerate = New DevComponents.DotNetBar.ButtonX()
        Me.FingerPrintDataSet = New FingerprintInformationSystem.FingerPrintDataSet()
        Me.FPAttestationRegisterBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.FPARegisterTableAdapter = New FingerprintInformationSystem.FingerPrintDataSetTableAdapters.FPAttestationRegisterTableAdapter()
        Me.LabelX1 = New DevComponents.DotNetBar.LabelX()
        Me.txtHeadofAccount = New DevComponents.DotNetBar.Controls.TextBoxX()
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
        Me.txtYear.Location = New System.Drawing.Point(295, 8)
        Me.txtYear.MaxValue = 2099
        Me.txtYear.MinValue = 0
        Me.txtYear.Name = "txtYear"
        Me.txtYear.ShowUpDown = True
        Me.txtYear.Size = New System.Drawing.Size(73, 29)
        Me.txtYear.TabIndex = 27
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
        Me.LabelX4.Location = New System.Drawing.Point(259, 13)
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
        Me.cmbMonth.Location = New System.Drawing.Point(117, 8)
        Me.cmbMonth.MaxDropDownItems = 15
        Me.cmbMonth.MaxLength = 255
        Me.cmbMonth.Name = "cmbMonth"
        Me.cmbMonth.Size = New System.Drawing.Size(136, 29)
        Me.cmbMonth.TabIndex = 26
        Me.cmbMonth.WatermarkText = "Month"
        '
        'btnGenerate
        '
        Me.btnGenerate.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnGenerate.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnGenerate.Location = New System.Drawing.Point(250, 87)
        Me.btnGenerate.Name = "btnGenerate"
        Me.btnGenerate.Size = New System.Drawing.Size(118, 50)
        Me.btnGenerate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnGenerate.TabIndex = 30
        Me.btnGenerate.Text = "Generate"
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
        Me.LabelX1.Location = New System.Drawing.Point(6, 54)
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
        Me.txtHeadofAccount.Location = New System.Drawing.Point(117, 50)
        Me.txtHeadofAccount.Name = "txtHeadofAccount"
        Me.txtHeadofAccount.PreventEnterBeep = True
        Me.txtHeadofAccount.Size = New System.Drawing.Size(251, 29)
        Me.txtHeadofAccount.TabIndex = 32
        '
        'frmRevenueCollection
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(370, 138)
        Me.Controls.Add(Me.txtHeadofAccount)
        Me.Controls.Add(Me.LabelX1)
        Me.Controls.Add(Me.btnGenerate)
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
    Friend WithEvents btnGenerate As DevComponents.DotNetBar.ButtonX
    Friend WithEvents FingerPrintDataSet As FingerprintInformationSystem.FingerPrintDataSet
    Friend WithEvents FPAttestationRegisterBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents FPARegisterTableAdapter As FingerprintInformationSystem.FingerPrintDataSetTableAdapters.FPAttestationRegisterTableAdapter
    Friend WithEvents LabelX1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtHeadofAccount As DevComponents.DotNetBar.Controls.TextBoxX
End Class
