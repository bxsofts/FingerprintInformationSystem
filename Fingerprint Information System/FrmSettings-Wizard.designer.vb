<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSettingsWizard
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmSettingsWizard))
        Me.LabelX25 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX24 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX23 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX22 = New DevComponents.DotNetBar.LabelX()
        Me.txtFullDistrict = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.txtShortDistrict = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX2 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX5 = New DevComponents.DotNetBar.LabelX()
        Me.txtShortOffice = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.txtFullOffice = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX6 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX7 = New DevComponents.DotNetBar.LabelX()
        Me.FingerPrintDataSet1 = New FingerprintInformationSystem.FingerPrintDataSet()
        Me.OfficerTableTableAdapter1 = New FingerprintInformationSystem.FingerPrintDataSetTableAdapters.OfficerTableTableAdapter()
        Me.SettingsTableAdapter1 = New FingerprintInformationSystem.FingerPrintDataSetTableAdapters.SettingsTableAdapter()
        Me.StyleManager1 = New DevComponents.DotNetBar.StyleManager(Me.components)
        Me.CommonSettingsTableAdapter1 = New FingerprintInformationSystem.FingerPrintDataSetTableAdapters.CommonSettingsTableAdapter()
        Me.PanelEx1 = New DevComponents.DotNetBar.PanelEx()
        Me.btnSave = New DevComponents.DotNetBar.ButtonX()
        CType(Me.FingerPrintDataSet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelEx1.SuspendLayout()
        Me.SuspendLayout()
        '
        'LabelX25
        '
        Me.LabelX25.AutoSize = True
        '
        '
        '
        Me.LabelX25.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX25.ForeColor = System.Drawing.Color.Red
        Me.LabelX25.Location = New System.Drawing.Point(450, 143)
        Me.LabelX25.Name = "LabelX25"
        Me.LabelX25.Size = New System.Drawing.Size(8, 18)
        Me.LabelX25.TabIndex = 17
        Me.LabelX25.Text = "*"
        '
        'LabelX24
        '
        Me.LabelX24.AutoSize = True
        '
        '
        '
        Me.LabelX24.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX24.ForeColor = System.Drawing.Color.Red
        Me.LabelX24.Location = New System.Drawing.Point(450, 102)
        Me.LabelX24.Name = "LabelX24"
        Me.LabelX24.Size = New System.Drawing.Size(8, 18)
        Me.LabelX24.TabIndex = 16
        Me.LabelX24.Text = "*"
        '
        'LabelX23
        '
        Me.LabelX23.AutoSize = True
        '
        '
        '
        Me.LabelX23.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX23.ForeColor = System.Drawing.Color.Red
        Me.LabelX23.Location = New System.Drawing.Point(450, 61)
        Me.LabelX23.Name = "LabelX23"
        Me.LabelX23.Size = New System.Drawing.Size(8, 18)
        Me.LabelX23.TabIndex = 15
        Me.LabelX23.Text = "*"
        '
        'LabelX22
        '
        Me.LabelX22.AutoSize = True
        '
        '
        '
        Me.LabelX22.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX22.ForeColor = System.Drawing.Color.Red
        Me.LabelX22.Location = New System.Drawing.Point(450, 20)
        Me.LabelX22.Name = "LabelX22"
        Me.LabelX22.Size = New System.Drawing.Size(8, 18)
        Me.LabelX22.TabIndex = 14
        Me.LabelX22.Text = "*"
        '
        'txtFullDistrict
        '
        Me.txtFullDistrict.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtFullDistrict.Border.Class = "TextBoxBorder"
        Me.txtFullDistrict.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtFullDistrict.DisabledBackColor = System.Drawing.Color.White
        Me.txtFullDistrict.FocusHighlightEnabled = True
        Me.txtFullDistrict.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFullDistrict.ForeColor = System.Drawing.Color.Black
        Me.txtFullDistrict.Location = New System.Drawing.Point(139, 97)
        Me.txtFullDistrict.MaxLength = 255
        Me.txtFullDistrict.Name = "txtFullDistrict"
        Me.txtFullDistrict.Size = New System.Drawing.Size(309, 29)
        Me.txtFullDistrict.TabIndex = 3
        Me.txtFullDistrict.WatermarkBehavior = DevComponents.DotNetBar.eWatermarkBehavior.HideNonEmpty
        Me.txtFullDistrict.WatermarkText = "Full District Name"
        '
        'txtShortDistrict
        '
        Me.txtShortDistrict.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtShortDistrict.Border.Class = "TextBoxBorder"
        Me.txtShortDistrict.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtShortDistrict.DisabledBackColor = System.Drawing.Color.White
        Me.txtShortDistrict.FocusHighlightEnabled = True
        Me.txtShortDistrict.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtShortDistrict.ForeColor = System.Drawing.Color.Black
        Me.txtShortDistrict.Location = New System.Drawing.Point(139, 138)
        Me.txtShortDistrict.MaxLength = 255
        Me.txtShortDistrict.Name = "txtShortDistrict"
        Me.txtShortDistrict.Size = New System.Drawing.Size(309, 29)
        Me.txtShortDistrict.TabIndex = 4
        Me.txtShortDistrict.WatermarkBehavior = DevComponents.DotNetBar.eWatermarkBehavior.HideNonEmpty
        Me.txtShortDistrict.WatermarkText = "Short District Name"
        '
        'LabelX2
        '
        Me.LabelX2.AutoSize = True
        '
        '
        '
        Me.LabelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX2.Location = New System.Drawing.Point(18, 143)
        Me.LabelX2.Name = "LabelX2"
        Me.LabelX2.Size = New System.Drawing.Size(114, 18)
        Me.LabelX2.TabIndex = 13
        Me.LabelX2.Text = "Short District Name"
        '
        'LabelX5
        '
        Me.LabelX5.AutoSize = True
        '
        '
        '
        Me.LabelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX5.Location = New System.Drawing.Point(18, 102)
        Me.LabelX5.Name = "LabelX5"
        Me.LabelX5.Size = New System.Drawing.Size(104, 18)
        Me.LabelX5.TabIndex = 12
        Me.LabelX5.Text = "Full District Name"
        '
        'txtShortOffice
        '
        Me.txtShortOffice.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtShortOffice.Border.Class = "TextBoxBorder"
        Me.txtShortOffice.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtShortOffice.DisabledBackColor = System.Drawing.Color.White
        Me.txtShortOffice.FocusHighlightEnabled = True
        Me.txtShortOffice.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtShortOffice.ForeColor = System.Drawing.Color.Black
        Me.txtShortOffice.Location = New System.Drawing.Point(139, 56)
        Me.txtShortOffice.MaxLength = 255
        Me.txtShortOffice.Name = "txtShortOffice"
        Me.txtShortOffice.Size = New System.Drawing.Size(309, 29)
        Me.txtShortOffice.TabIndex = 2
        Me.txtShortOffice.WatermarkBehavior = DevComponents.DotNetBar.eWatermarkBehavior.HideNonEmpty
        Me.txtShortOffice.WatermarkText = "Short Office Name"
        '
        'txtFullOffice
        '
        Me.txtFullOffice.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtFullOffice.Border.Class = "TextBoxBorder"
        Me.txtFullOffice.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtFullOffice.DisabledBackColor = System.Drawing.Color.White
        Me.txtFullOffice.FocusHighlightEnabled = True
        Me.txtFullOffice.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFullOffice.ForeColor = System.Drawing.Color.Black
        Me.txtFullOffice.Location = New System.Drawing.Point(139, 15)
        Me.txtFullOffice.MaxLength = 255
        Me.txtFullOffice.Name = "txtFullOffice"
        Me.txtFullOffice.Size = New System.Drawing.Size(309, 29)
        Me.txtFullOffice.TabIndex = 1
        Me.txtFullOffice.WatermarkBehavior = DevComponents.DotNetBar.eWatermarkBehavior.HideNonEmpty
        Me.txtFullOffice.WatermarkText = "Full Office Name"
        '
        'LabelX6
        '
        Me.LabelX6.AutoSize = True
        '
        '
        '
        Me.LabelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX6.Location = New System.Drawing.Point(18, 61)
        Me.LabelX6.Name = "LabelX6"
        Me.LabelX6.Size = New System.Drawing.Size(107, 18)
        Me.LabelX6.TabIndex = 8
        Me.LabelX6.Text = "Short Office Name"
        '
        'LabelX7
        '
        Me.LabelX7.AutoSize = True
        '
        '
        '
        Me.LabelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX7.Location = New System.Drawing.Point(18, 20)
        Me.LabelX7.Name = "LabelX7"
        Me.LabelX7.Size = New System.Drawing.Size(97, 18)
        Me.LabelX7.TabIndex = 6
        Me.LabelX7.Text = "Full Office Name"
        '
        'FingerPrintDataSet1
        '
        Me.FingerPrintDataSet1.DataSetName = "FingerPrintDataSet"
        Me.FingerPrintDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'OfficerTableTableAdapter1
        '
        Me.OfficerTableTableAdapter1.ClearBeforeFill = True
        '
        'SettingsTableAdapter1
        '
        Me.SettingsTableAdapter1.ClearBeforeFill = True
        '
        'StyleManager1
        '
        Me.StyleManager1.ManagerStyle = DevComponents.DotNetBar.eStyle.Office2016
        Me.StyleManager1.MetroColorParameters = New DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(115, Byte), Integer), CType(CType(199, Byte), Integer)))
        '
        'CommonSettingsTableAdapter1
        '
        Me.CommonSettingsTableAdapter1.ClearBeforeFill = True
        '
        'PanelEx1
        '
        Me.PanelEx1.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.PanelEx1.Controls.Add(Me.btnSave)
        Me.PanelEx1.Controls.Add(Me.LabelX25)
        Me.PanelEx1.Controls.Add(Me.LabelX7)
        Me.PanelEx1.Controls.Add(Me.LabelX24)
        Me.PanelEx1.Controls.Add(Me.LabelX6)
        Me.PanelEx1.Controls.Add(Me.LabelX23)
        Me.PanelEx1.Controls.Add(Me.txtFullOffice)
        Me.PanelEx1.Controls.Add(Me.LabelX22)
        Me.PanelEx1.Controls.Add(Me.txtShortOffice)
        Me.PanelEx1.Controls.Add(Me.txtFullDistrict)
        Me.PanelEx1.Controls.Add(Me.LabelX5)
        Me.PanelEx1.Controls.Add(Me.txtShortDistrict)
        Me.PanelEx1.Controls.Add(Me.LabelX2)
        Me.PanelEx1.DisabledBackColor = System.Drawing.Color.Empty
        Me.PanelEx1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelEx1.Location = New System.Drawing.Point(0, 0)
        Me.PanelEx1.Name = "PanelEx1"
        Me.PanelEx1.Size = New System.Drawing.Size(590, 178)
        Me.PanelEx1.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx1.Style.GradientAngle = 90
        Me.PanelEx1.TabIndex = 1
        '
        'btnSave
        '
        Me.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnSave.Location = New System.Drawing.Point(475, 59)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(98, 61)
        Me.btnSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnSave.TabIndex = 5
        Me.btnSave.Text = "Save"
        '
        'FrmSettingsWizard
        '
        Me.AcceptButton = Me.btnSave
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(590, 178)
        Me.ControlBox = False
        Me.Controls.Add(Me.PanelEx1)
        Me.DoubleBuffered = True
        Me.EnableGlass = False
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FrmSettingsWizard"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Office Settings"
        Me.TitleText = "<b>Office Settings</b>"
        CType(Me.FingerPrintDataSet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelEx1.ResumeLayout(False)
        Me.PanelEx1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtFullDistrict As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents txtShortDistrict As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX2 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX5 As DevComponents.DotNetBar.LabelX
    Friend WithEvents txtShortOffice As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents txtFullOffice As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX6 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX7 As DevComponents.DotNetBar.LabelX
    Friend WithEvents FingerPrintDataSet1 As FingerprintInformationSystem.FingerPrintDataSet
    Friend WithEvents OfficerTableTableAdapter1 As FingerprintInformationSystem.FingerPrintDataSetTableAdapters.OfficerTableTableAdapter
    Friend WithEvents SettingsTableAdapter1 As FingerprintInformationSystem.FingerPrintDataSetTableAdapters.SettingsTableAdapter
    Friend WithEvents StyleManager1 As DevComponents.DotNetBar.StyleManager
    Friend WithEvents LabelX25 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX24 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX23 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX22 As DevComponents.DotNetBar.LabelX
    Friend WithEvents CommonSettingsTableAdapter1 As FingerprintInformationSystem.FingerPrintDataSetTableAdapters.CommonSettingsTableAdapter
    Friend WithEvents PanelEx1 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents btnSave As DevComponents.DotNetBar.ButtonX
End Class
