<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPSList
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPSList))
        Me.PoliceStationListBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.FingerPrintDataSet = New FingerprintInformationSystem.FingerPrintDataSet()
        Me.ReportViewer1 = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.PSRegisterTableAdapter = New FingerprintInformationSystem.FingerPrintDataSetTableAdapters.PoliceStationListTableAdapter()
        Me.btnPrint = New DevComponents.DotNetBar.ButtonX()
        CType(Me.PoliceStationListBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FingerPrintDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PoliceStationListBindingSource
        '
        Me.PoliceStationListBindingSource.DataMember = "PoliceStationList"
        Me.PoliceStationListBindingSource.DataSource = Me.FingerPrintDataSet
        '
        'FingerPrintDataSet
        '
        Me.FingerPrintDataSet.DataSetName = "FingerPrintDataSet"
        Me.FingerPrintDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'ReportViewer1
        '
        Me.ReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill
        ReportDataSource1.Name = "FingerPrintDataSet_PoliceStationList"
        ReportDataSource1.Value = Me.PoliceStationListBindingSource
        Me.ReportViewer1.LocalReport.DataSources.Add(ReportDataSource1)
        Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "FingerprintInformationSystem.List of Police Stations.rdlc"
        Me.ReportViewer1.Location = New System.Drawing.Point(0, 0)
        Me.ReportViewer1.Name = "ReportViewer1"
        Me.ReportViewer1.Size = New System.Drawing.Size(996, 444)
        Me.ReportViewer1.TabIndex = 1
        '
        'PSRegisterTableAdapter
        '
        Me.PSRegisterTableAdapter.ClearBeforeFill = True
        '
        'btnPrint
        '
        Me.btnPrint.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnPrint.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnPrint.Image = CType(resources.GetObject("btnPrint.Image"), System.Drawing.Image)
        Me.btnPrint.Location = New System.Drawing.Point(449, 194)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlP)
        Me.btnPrint.Size = New System.Drawing.Size(98, 56)
        Me.btnPrint.TabIndex = 29
        Me.btnPrint.Text = "Print"
        '
        'frmPSList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(996, 444)
        Me.Controls.Add(Me.ReportViewer1)
        Me.Controls.Add(Me.btnPrint)
        Me.DoubleBuffered = True
        Me.EnableGlass = False
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmPSList"
        Me.Text = "List of Police Stations"
        Me.TitleText = "<b>List of Police Stations</b>"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.PoliceStationListBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FingerPrintDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PoliceStationListBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents FingerPrintDataSet As FingerprintInformationSystem.FingerPrintDataSet
    Friend WithEvents PSRegisterTableAdapter As FingerprintInformationSystem.FingerPrintDataSetTableAdapters.PoliceStationListTableAdapter
    Friend WithEvents btnPrint As DevComponents.DotNetBar.ButtonX
    Private WithEvents ReportViewer1 As Microsoft.Reporting.WinForms.ReportViewer
End Class
