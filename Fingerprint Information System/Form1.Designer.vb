<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

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
        Me.PanelEx1 = New DevComponents.DotNetBar.PanelEx()
        Me.ReportViewer1 = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.WeeklyDiaryDataSet = New FingerprintInformationSystem.WeeklyDiaryDataSet()
        Me.WeeklyDiaryBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.WeeklyDiaryTableAdapter = New FingerprintInformationSystem.WeeklyDiaryDataSetTableAdapters.WeeklyDiaryTableAdapter()
        Me.PanelEx1.SuspendLayout()
        CType(Me.WeeklyDiaryDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.WeeklyDiaryBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelEx1
        '
        Me.PanelEx1.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.PanelEx1.Controls.Add(Me.ReportViewer1)
        Me.PanelEx1.DisabledBackColor = System.Drawing.Color.Empty
        Me.PanelEx1.Location = New System.Drawing.Point(12, 93)
        Me.PanelEx1.Name = "PanelEx1"
        Me.PanelEx1.Size = New System.Drawing.Size(792, 138)
        Me.PanelEx1.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx1.Style.GradientAngle = 90
        Me.PanelEx1.TabIndex = 0
        '
        'ReportViewer1
        '
        ReportDataSource1.Name = "DataSet1"
        ReportDataSource1.Value = Me.WeeklyDiaryBindingSource
        Me.ReportViewer1.LocalReport.DataSources.Add(ReportDataSource1)
        Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "FingerprintInformationSystem.WeeklyDiary.rdlc"
        Me.ReportViewer1.Location = New System.Drawing.Point(13, 3)
        Me.ReportViewer1.Name = "ReportViewer1"
        Me.ReportViewer1.Size = New System.Drawing.Size(779, 115)
        Me.ReportViewer1.TabIndex = 0
        '
        'WeeklyDiaryDataSet
        '
        Me.WeeklyDiaryDataSet.DataSetName = "WeeklyDiaryDataSet"
        Me.WeeklyDiaryDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'WeeklyDiaryBindingSource
        '
        Me.WeeklyDiaryBindingSource.DataMember = "WeeklyDiary"
        Me.WeeklyDiaryBindingSource.DataSource = Me.WeeklyDiaryDataSet
        '
        'WeeklyDiaryTableAdapter
        '
        Me.WeeklyDiaryTableAdapter.ClearBeforeFill = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(816, 261)
        Me.Controls.Add(Me.PanelEx1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.PanelEx1.ResumeLayout(False)
        CType(Me.WeeklyDiaryDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.WeeklyDiaryBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PanelEx1 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents ReportViewer1 As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents WeeklyDiaryBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents WeeklyDiaryDataSet As FingerprintInformationSystem.WeeklyDiaryDataSet
    Friend WithEvents WeeklyDiaryTableAdapter As FingerprintInformationSystem.WeeklyDiaryDataSetTableAdapters.WeeklyDiaryTableAdapter
End Class
