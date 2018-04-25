<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSOCFacingSheetWithGist
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
        Dim ReportDataSource1 As Microsoft.Reporting.WinForms.ReportDataSource = New Microsoft.Reporting.WinForms.ReportDataSource()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSOCFacingSheetWithGist))
        Me.SOCRegisterBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.FingerPrintDataSet = New FingerprintInformationSystem.FingerPrintDataSet()
        Me.ReportViewer1 = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.PanelEx1 = New DevComponents.DotNetBar.PanelEx()
        Me.btnSOCReport = New DevComponents.DotNetBar.ButtonX()
        Me.btnNextSOC = New DevComponents.DotNetBar.ButtonX()
        Me.btnPrevSOC = New DevComponents.DotNetBar.ButtonX()
        Me.btnOpenInWord = New DevComponents.DotNetBar.ButtonX()
        Me.btnPrint = New DevComponents.DotNetBar.ButtonX()
        Me.btnOK = New DevComponents.DotNetBar.ButtonX()
        Me.txtArticlesTaken = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.txtSequenceprints = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX3 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX2 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX1 = New DevComponents.DotNetBar.LabelX()
        Me.SOCRegisterTableAdapter = New FingerprintInformationSystem.FingerPrintDataSetTableAdapters.SOCRegisterTableAdapter()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        CType(Me.SOCRegisterBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FingerPrintDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.PanelEx1.SuspendLayout()
        Me.SuspendLayout()
        '
        'SOCRegisterBindingSource
        '
        Me.SOCRegisterBindingSource.DataMember = "SOCRegister"
        Me.SOCRegisterBindingSource.DataSource = Me.FingerPrintDataSet
        '
        'FingerPrintDataSet
        '
        Me.FingerPrintDataSet.DataSetName = "FingerPrintDataSet"
        Me.FingerPrintDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'ReportViewer1
        '
        Me.ReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill
        ReportDataSource1.Name = "FingerPrintDataSet_SOCRegister"
        ReportDataSource1.Value = Me.SOCRegisterBindingSource
        Me.ReportViewer1.LocalReport.DataSources.Add(ReportDataSource1)
        Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "FingerprintInformationSystem.Facing Sheet.rdlc"
        Me.ReportViewer1.Location = New System.Drawing.Point(3, 145)
        Me.ReportViewer1.Name = "ReportViewer1"
        Me.ReportViewer1.Size = New System.Drawing.Size(1348, 330)
        Me.ReportViewer1.TabIndex = 0
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.PanelEx1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.ReportViewer1, 0, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 142.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1354, 478)
        Me.TableLayoutPanel1.TabIndex = 2
        '
        'PanelEx1
        '
        Me.PanelEx1.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.PanelEx1.Controls.Add(Me.btnSOCReport)
        Me.PanelEx1.Controls.Add(Me.btnNextSOC)
        Me.PanelEx1.Controls.Add(Me.btnPrevSOC)
        Me.PanelEx1.Controls.Add(Me.btnOpenInWord)
        Me.PanelEx1.Controls.Add(Me.btnPrint)
        Me.PanelEx1.Controls.Add(Me.btnOK)
        Me.PanelEx1.Controls.Add(Me.txtArticlesTaken)
        Me.PanelEx1.Controls.Add(Me.txtSequenceprints)
        Me.PanelEx1.Controls.Add(Me.LabelX3)
        Me.PanelEx1.Controls.Add(Me.LabelX2)
        Me.PanelEx1.Controls.Add(Me.LabelX1)
        Me.PanelEx1.DisabledBackColor = System.Drawing.Color.Empty
        Me.PanelEx1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelEx1.Location = New System.Drawing.Point(3, 3)
        Me.PanelEx1.Name = "PanelEx1"
        Me.PanelEx1.Size = New System.Drawing.Size(1348, 136)
        Me.PanelEx1.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx1.Style.GradientAngle = 90
        Me.PanelEx1.TabIndex = 1
        '
        'btnSOCReport
        '
        Me.btnSOCReport.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnSOCReport.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnSOCReport.Location = New System.Drawing.Point(1190, 29)
        Me.btnSOCReport.Name = "btnSOCReport"
        Me.btnSOCReport.Size = New System.Drawing.Size(125, 80)
        Me.btnSOCReport.TabIndex = 12
        Me.btnSOCReport.TabStop = False
        Me.btnSOCReport.Text = "SoC Report"
        '
        'btnNextSOC
        '
        Me.btnNextSOC.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnNextSOC.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnNextSOC.Location = New System.Drawing.Point(1018, 9)
        Me.btnNextSOC.Name = "btnNextSOC"
        Me.btnNextSOC.Size = New System.Drawing.Size(121, 41)
        Me.btnNextSOC.TabIndex = 11
        Me.btnNextSOC.TabStop = False
        Me.btnNextSOC.Text = "Next SOC"
        '
        'btnPrevSOC
        '
        Me.btnPrevSOC.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnPrevSOC.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnPrevSOC.Location = New System.Drawing.Point(866, 9)
        Me.btnPrevSOC.Name = "btnPrevSOC"
        Me.btnPrevSOC.Size = New System.Drawing.Size(121, 41)
        Me.btnPrevSOC.TabIndex = 10
        Me.btnPrevSOC.TabStop = False
        Me.btnPrevSOC.Text = "Prev SOC"
        '
        'btnOpenInWord
        '
        Me.btnOpenInWord.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnOpenInWord.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnOpenInWord.Image = CType(resources.GetObject("btnOpenInWord.Image"), System.Drawing.Image)
        Me.btnOpenInWord.Location = New System.Drawing.Point(1018, 70)
        Me.btnOpenInWord.Name = "btnOpenInWord"
        Me.btnOpenInWord.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlW)
        Me.btnOpenInWord.Size = New System.Drawing.Size(121, 59)
        Me.btnOpenInWord.TabIndex = 6
        Me.btnOpenInWord.Text = "MS Word"
        '
        'btnPrint
        '
        Me.btnPrint.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnPrint.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnPrint.Image = CType(resources.GetObject("btnPrint.Image"), System.Drawing.Image)
        Me.btnPrint.Location = New System.Drawing.Point(866, 70)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlP)
        Me.btnPrint.Size = New System.Drawing.Size(121, 59)
        Me.btnPrint.TabIndex = 5
        Me.btnPrint.Text = "Print"
        '
        'btnOK
        '
        Me.btnOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnOK.Location = New System.Drawing.Point(741, 46)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlG)
        Me.btnOK.Size = New System.Drawing.Size(87, 76)
        Me.btnOK.TabIndex = 4
        Me.btnOK.Text = "Generate"
        '
        'txtArticlesTaken
        '
        Me.txtArticlesTaken.AcceptsReturn = True
        Me.txtArticlesTaken.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtArticlesTaken.Border.Class = "TextBoxBorder"
        Me.txtArticlesTaken.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtArticlesTaken.ButtonCustom.Image = CType(resources.GetObject("txtArticlesTaken.ButtonCustom.Image"), System.Drawing.Image)
        Me.txtArticlesTaken.DisabledBackColor = System.Drawing.Color.White
        Me.txtArticlesTaken.FocusHighlightEnabled = True
        Me.txtArticlesTaken.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtArticlesTaken.ForeColor = System.Drawing.Color.Black
        Me.txtArticlesTaken.Location = New System.Drawing.Point(493, 46)
        Me.txtArticlesTaken.MaxLength = 255
        Me.txtArticlesTaken.Multiline = True
        Me.txtArticlesTaken.Name = "txtArticlesTaken"
        Me.txtArticlesTaken.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtArticlesTaken.Size = New System.Drawing.Size(216, 76)
        Me.txtArticlesTaken.TabIndex = 2
        Me.txtArticlesTaken.WatermarkText = "Articles taken from SOC if any"
        '
        'txtSequenceprints
        '
        Me.txtSequenceprints.AcceptsReturn = True
        Me.txtSequenceprints.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.txtSequenceprints.Border.Class = "TextBoxBorder"
        Me.txtSequenceprints.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.txtSequenceprints.ButtonCustom.Image = CType(resources.GetObject("txtSequenceprints.ButtonCustom.Image"), System.Drawing.Image)
        Me.txtSequenceprints.DisabledBackColor = System.Drawing.Color.White
        Me.txtSequenceprints.FocusHighlightEnabled = True
        Me.txtSequenceprints.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSequenceprints.ForeColor = System.Drawing.Color.Black
        Me.txtSequenceprints.Location = New System.Drawing.Point(128, 46)
        Me.txtSequenceprints.MaxLength = 255
        Me.txtSequenceprints.Multiline = True
        Me.txtSequenceprints.Name = "txtSequenceprints"
        Me.txtSequenceprints.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtSequenceprints.Size = New System.Drawing.Size(216, 76)
        Me.txtSequenceprints.TabIndex = 1
        Me.txtSequenceprints.WatermarkText = "Sequence prints if any"
        '
        'LabelX3
        '
        Me.LabelX3.AutoSize = True
        '
        '
        '
        Me.LabelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX3.Location = New System.Drawing.Point(9, 46)
        Me.LabelX3.Name = "LabelX3"
        Me.LabelX3.Size = New System.Drawing.Size(114, 34)
        Me.LabelX3.TabIndex = 2
        Me.LabelX3.Text = "Details of Sequence" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Prints if any"
        '
        'LabelX2
        '
        Me.LabelX2.AutoSize = True
        '
        '
        '
        Me.LabelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX2.Location = New System.Drawing.Point(350, 46)
        Me.LabelX2.Name = "LabelX2"
        Me.LabelX2.Size = New System.Drawing.Size(137, 34)
        Me.LabelX2.TabIndex = 1
        Me.LabelX2.Text = "Details of Articles taken" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "from SOC if any"
        '
        'LabelX1
        '
        Me.LabelX1.AutoSize = True
        '
        '
        '
        Me.LabelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX1.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX1.Location = New System.Drawing.Point(3, 9)
        Me.LabelX1.Name = "LabelX1"
        Me.LabelX1.Size = New System.Drawing.Size(660, 24)
        Me.LabelX1.TabIndex = 0
        Me.LabelX1.Text = "Please fill the following fields also (not mandatory) and then press the Generate" & _
    " button"
        '
        'SOCRegisterTableAdapter
        '
        Me.SOCRegisterTableAdapter.ClearBeforeFill = True
        '
        'frmSOCFacingSheetWithGist
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1354, 478)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.DoubleBuffered = True
        Me.EnableGlass = False
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmSOCFacingSheetWithGist"
        Me.Text = "Facing Sheet With Gist"
        Me.TitleText = "<b>Facing Sheet With Gist</b>"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.SOCRegisterBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FingerPrintDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.PanelEx1.ResumeLayout(False)
        Me.PanelEx1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SOCRegisterBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents FingerPrintDataSet As FingerprintInformationSystem.FingerPrintDataSet
    Friend WithEvents SOCRegisterTableAdapter As FingerprintInformationSystem.FingerPrintDataSetTableAdapters.SOCRegisterTableAdapter
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents PanelEx1 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents btnPrint As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnOK As DevComponents.DotNetBar.ButtonX
    Friend WithEvents txtArticlesTaken As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents txtSequenceprints As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX3 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX2 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents btnOpenInWord As DevComponents.DotNetBar.ButtonX
    Private WithEvents ReportViewer1 As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents btnPrevSOC As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnNextSOC As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnSOCReport As DevComponents.DotNetBar.ButtonX
End Class
