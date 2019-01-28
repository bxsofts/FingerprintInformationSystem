<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmLocalBackup
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmLocalBackup))
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.btnRestoreDatabase = New DevComponents.DotNetBar.ButtonX()
        Me.btnRemoveBackupFile = New DevComponents.DotNetBar.ButtonX()
        Me.btnOpenBackupFolder = New DevComponents.DotNetBar.ButtonX()
        Me.GroupPanel1 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.listViewEx1 = New DevComponents.DotNetBar.Controls.ListViewEx()
        Me.BackupFile = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.BackupDate = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.BackupFolder = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.FileSize = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btnBackupDatabase = New DevComponents.DotNetBar.ButtonX()
        Me.btnCopyDatabase = New DevComponents.DotNetBar.ButtonX()
        Me.btnImportDatabase = New DevComponents.DotNetBar.ButtonX()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.btnOpenFileMSAccess = New DevComponents.DotNetBar.ButtonX()
        Me.PanelEx1 = New DevComponents.DotNetBar.PanelEx()
        Me.PanelEx3 = New DevComponents.DotNetBar.PanelEx()
        Me.Bar1 = New DevComponents.DotNetBar.Bar()
        Me.lblCount = New DevComponents.DotNetBar.LabelItem()
        Me.lblSelectedFile = New DevComponents.DotNetBar.LabelItem()
        Me.PanelEx2 = New DevComponents.DotNetBar.PanelEx()
        Me.lblTotalFileSize = New DevComponents.DotNetBar.LabelItem()
        Me.GroupPanel1.SuspendLayout()
        Me.PanelEx1.SuspendLayout()
        Me.PanelEx3.SuspendLayout()
        CType(Me.Bar1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelEx2.SuspendLayout()
        Me.SuspendLayout()
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "Hopstarter-Office-2010-Microsoft-Office-Access.ico")
        Me.ImageList1.Images.SetKeyName(1, "gdrive_icon-icons.com_62760.png")
        '
        'btnRestoreDatabase
        '
        Me.btnRestoreDatabase.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnRestoreDatabase.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnRestoreDatabase.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRestoreDatabase.Image = CType(resources.GetObject("btnRestoreDatabase.Image"), System.Drawing.Image)
        Me.btnRestoreDatabase.Location = New System.Drawing.Point(15, 94)
        Me.btnRestoreDatabase.Name = "btnRestoreDatabase"
        Me.btnRestoreDatabase.Size = New System.Drawing.Size(117, 59)
        Me.btnRestoreDatabase.TabIndex = 2
        Me.btnRestoreDatabase.Text = "Restore"
        '
        'btnRemoveBackupFile
        '
        Me.btnRemoveBackupFile.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnRemoveBackupFile.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnRemoveBackupFile.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRemoveBackupFile.Image = CType(resources.GetObject("btnRemoveBackupFile.Image"), System.Drawing.Image)
        Me.btnRemoveBackupFile.Location = New System.Drawing.Point(15, 161)
        Me.btnRemoveBackupFile.Name = "btnRemoveBackupFile"
        Me.btnRemoveBackupFile.Size = New System.Drawing.Size(117, 59)
        Me.btnRemoveBackupFile.TabIndex = 3
        Me.btnRemoveBackupFile.Text = "Remove"
        '
        'btnOpenBackupFolder
        '
        Me.btnOpenBackupFolder.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnOpenBackupFolder.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnOpenBackupFolder.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOpenBackupFolder.Image = CType(resources.GetObject("btnOpenBackupFolder.Image"), System.Drawing.Image)
        Me.btnOpenBackupFolder.Location = New System.Drawing.Point(15, 295)
        Me.btnOpenBackupFolder.Name = "btnOpenBackupFolder"
        Me.btnOpenBackupFolder.Size = New System.Drawing.Size(117, 59)
        Me.btnOpenBackupFolder.TabIndex = 5
        Me.btnOpenBackupFolder.Text = "Open Folder"
        '
        'GroupPanel1
        '
        Me.GroupPanel1.BackColor = System.Drawing.Color.Transparent
        Me.GroupPanel1.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanel1.Controls.Add(Me.listViewEx1)
        Me.GroupPanel1.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupPanel1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupPanel1.Location = New System.Drawing.Point(0, 0)
        Me.GroupPanel1.Name = "GroupPanel1"
        Me.GroupPanel1.Size = New System.Drawing.Size(757, 491)
        '
        '
        '
        Me.GroupPanel1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.GroupPanel1.Style.BackColorGradientAngle = 90
        Me.GroupPanel1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.GroupPanel1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel1.Style.BorderBottomWidth = 1
        Me.GroupPanel1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.GroupPanel1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel1.Style.BorderLeftWidth = 1
        Me.GroupPanel1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel1.Style.BorderRightWidth = 1
        Me.GroupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel1.Style.BorderTopWidth = 1
        Me.GroupPanel1.Style.CornerDiameter = 4
        Me.GroupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.GroupPanel1.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.GroupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.GroupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.GroupPanel1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.GroupPanel1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.GroupPanel1.TabIndex = 9
        Me.GroupPanel1.Text = "Available Backups"
        '
        'listViewEx1
        '
        Me.listViewEx1.Alignment = System.Windows.Forms.ListViewAlignment.Left
        Me.listViewEx1.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.listViewEx1.Border.Class = "ListViewBorder"
        Me.listViewEx1.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.listViewEx1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.BackupFile, Me.BackupDate, Me.BackupFolder, Me.FileSize})
        Me.listViewEx1.DisabledBackColor = System.Drawing.Color.Empty
        Me.listViewEx1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.listViewEx1.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.listViewEx1.ForeColor = System.Drawing.Color.Black
        Me.listViewEx1.FullRowSelect = True
        Me.listViewEx1.GridLines = True
        Me.listViewEx1.Location = New System.Drawing.Point(0, 0)
        Me.listViewEx1.MultiSelect = False
        Me.listViewEx1.Name = "listViewEx1"
        Me.listViewEx1.ShowItemToolTips = True
        Me.listViewEx1.Size = New System.Drawing.Size(751, 467)
        Me.listViewEx1.SmallImageList = Me.ImageList1
        Me.listViewEx1.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.listViewEx1.TabIndex = 0
        Me.listViewEx1.UseCompatibleStateImageBehavior = False
        Me.listViewEx1.View = System.Windows.Forms.View.Details
        '
        'BackupFile
        '
        Me.BackupFile.Text = "Backup File"
        Me.BackupFile.Width = 315
        '
        'BackupDate
        '
        Me.BackupDate.Text = "Backup Date"
        Me.BackupDate.Width = 223
        '
        'BackupFolder
        '
        Me.BackupFolder.Text = "Backup Folder"
        Me.BackupFolder.Width = 0
        '
        'FileSize
        '
        Me.FileSize.Text = "File Size"
        Me.FileSize.Width = 100
        '
        'btnBackupDatabase
        '
        Me.btnBackupDatabase.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnBackupDatabase.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnBackupDatabase.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBackupDatabase.Image = CType(resources.GetObject("btnBackupDatabase.Image"), System.Drawing.Image)
        Me.btnBackupDatabase.Location = New System.Drawing.Point(15, 27)
        Me.btnBackupDatabase.Name = "btnBackupDatabase"
        Me.btnBackupDatabase.Size = New System.Drawing.Size(117, 59)
        Me.btnBackupDatabase.TabIndex = 1
        Me.btnBackupDatabase.Text = "Backup"
        '
        'btnCopyDatabase
        '
        Me.btnCopyDatabase.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnCopyDatabase.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnCopyDatabase.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCopyDatabase.Image = CType(resources.GetObject("btnCopyDatabase.Image"), System.Drawing.Image)
        Me.btnCopyDatabase.Location = New System.Drawing.Point(15, 362)
        Me.btnCopyDatabase.Name = "btnCopyDatabase"
        Me.btnCopyDatabase.Size = New System.Drawing.Size(117, 59)
        Me.btnCopyDatabase.TabIndex = 6
        Me.btnCopyDatabase.Text = "Copy Database"
        '
        'btnImportDatabase
        '
        Me.btnImportDatabase.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnImportDatabase.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnImportDatabase.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnImportDatabase.Image = CType(resources.GetObject("btnImportDatabase.Image"), System.Drawing.Image)
        Me.btnImportDatabase.Location = New System.Drawing.Point(15, 429)
        Me.btnImportDatabase.Name = "btnImportDatabase"
        Me.btnImportDatabase.Size = New System.Drawing.Size(117, 59)
        Me.btnImportDatabase.TabIndex = 7
        Me.btnImportDatabase.Text = "Import Database"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'btnOpenFileMSAccess
        '
        Me.btnOpenFileMSAccess.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnOpenFileMSAccess.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnOpenFileMSAccess.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOpenFileMSAccess.Image = CType(resources.GetObject("btnOpenFileMSAccess.Image"), System.Drawing.Image)
        Me.btnOpenFileMSAccess.Location = New System.Drawing.Point(15, 228)
        Me.btnOpenFileMSAccess.Name = "btnOpenFileMSAccess"
        Me.btnOpenFileMSAccess.Size = New System.Drawing.Size(117, 59)
        Me.btnOpenFileMSAccess.TabIndex = 4
        Me.btnOpenFileMSAccess.Text = "Open File"
        '
        'PanelEx1
        '
        Me.PanelEx1.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.PanelEx1.Controls.Add(Me.PanelEx3)
        Me.PanelEx1.Controls.Add(Me.Bar1)
        Me.PanelEx1.Controls.Add(Me.PanelEx2)
        Me.PanelEx1.DisabledBackColor = System.Drawing.Color.Empty
        Me.PanelEx1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelEx1.Location = New System.Drawing.Point(0, 0)
        Me.PanelEx1.Name = "PanelEx1"
        Me.PanelEx1.Size = New System.Drawing.Size(904, 514)
        Me.PanelEx1.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx1.Style.GradientAngle = 90
        Me.PanelEx1.TabIndex = 25
        '
        'PanelEx3
        '
        Me.PanelEx3.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.PanelEx3.Controls.Add(Me.GroupPanel1)
        Me.PanelEx3.DisabledBackColor = System.Drawing.Color.Empty
        Me.PanelEx3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelEx3.Location = New System.Drawing.Point(0, 0)
        Me.PanelEx3.Name = "PanelEx3"
        Me.PanelEx3.Size = New System.Drawing.Size(757, 491)
        Me.PanelEx3.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx3.Style.GradientAngle = 90
        Me.PanelEx3.TabIndex = 40
        Me.PanelEx3.Text = "PanelEx3"
        '
        'Bar1
        '
        Me.Bar1.AntiAlias = True
        Me.Bar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Bar1.DockSide = DevComponents.DotNetBar.eDockSide.Document
        Me.Bar1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Bar1.IsMaximized = False
        Me.Bar1.Items.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.lblCount, Me.lblSelectedFile, Me.lblTotalFileSize})
        Me.Bar1.Location = New System.Drawing.Point(0, 491)
        Me.Bar1.Name = "Bar1"
        Me.Bar1.Size = New System.Drawing.Size(757, 23)
        Me.Bar1.Stretch = True
        Me.Bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.Bar1.TabIndex = 36
        Me.Bar1.TabStop = False
        '
        'lblCount
        '
        Me.lblCount.BorderType = DevComponents.DotNetBar.eBorderType.DoubleLine
        Me.lblCount.Name = "lblCount"
        Me.lblCount.Width = 140
        '
        'lblSelectedFile
        '
        Me.lblSelectedFile.BeginGroup = True
        Me.lblSelectedFile.BorderType = DevComponents.DotNetBar.eBorderType.DoubleLine
        Me.lblSelectedFile.Name = "lblSelectedFile"
        Me.lblSelectedFile.Width = 300
        '
        'PanelEx2
        '
        Me.PanelEx2.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.PanelEx2.Controls.Add(Me.btnBackupDatabase)
        Me.PanelEx2.Controls.Add(Me.btnRestoreDatabase)
        Me.PanelEx2.Controls.Add(Me.btnOpenFileMSAccess)
        Me.PanelEx2.Controls.Add(Me.btnRemoveBackupFile)
        Me.PanelEx2.Controls.Add(Me.btnImportDatabase)
        Me.PanelEx2.Controls.Add(Me.btnOpenBackupFolder)
        Me.PanelEx2.Controls.Add(Me.btnCopyDatabase)
        Me.PanelEx2.DisabledBackColor = System.Drawing.Color.Empty
        Me.PanelEx2.Dock = System.Windows.Forms.DockStyle.Right
        Me.PanelEx2.Location = New System.Drawing.Point(757, 0)
        Me.PanelEx2.Name = "PanelEx2"
        Me.PanelEx2.Size = New System.Drawing.Size(147, 514)
        Me.PanelEx2.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx2.Style.GradientAngle = 90
        Me.PanelEx2.TabIndex = 29
        '
        'lblTotalFileSize
        '
        Me.lblTotalFileSize.BeginGroup = True
        Me.lblTotalFileSize.BorderType = DevComponents.DotNetBar.eBorderType.DoubleLine
        Me.lblTotalFileSize.Name = "lblTotalFileSize"
        Me.lblTotalFileSize.Width = 200
        '
        'FrmLocalBackup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CaptionFont = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ClientSize = New System.Drawing.Size(904, 514)
        Me.Controls.Add(Me.PanelEx1)
        Me.DoubleBuffered = True
        Me.EnableGlass = False
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmLocalBackup"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Local Database Backup"
        Me.TitleText = "<b>Local Database Backup</b>"
        Me.GroupPanel1.ResumeLayout(False)
        Me.PanelEx1.ResumeLayout(False)
        Me.PanelEx3.ResumeLayout(False)
        CType(Me.Bar1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelEx2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnRestoreDatabase As DevComponents.DotNetBar.ButtonX
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents btnRemoveBackupFile As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnOpenBackupFolder As DevComponents.DotNetBar.ButtonX
    Friend WithEvents GroupPanel1 As DevComponents.DotNetBar.Controls.GroupPanel
    Private WithEvents listViewEx1 As DevComponents.DotNetBar.Controls.ListViewEx
    Private WithEvents BackupFile As System.Windows.Forms.ColumnHeader
    Friend WithEvents BackupDate As System.Windows.Forms.ColumnHeader
    Friend WithEvents btnBackupDatabase As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnCopyDatabase As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnImportDatabase As DevComponents.DotNetBar.ButtonX
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents BackupFolder As System.Windows.Forms.ColumnHeader
    Friend WithEvents btnOpenFileMSAccess As DevComponents.DotNetBar.ButtonX
    Friend WithEvents PanelEx1 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents PanelEx3 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents Bar1 As DevComponents.DotNetBar.Bar
    Friend WithEvents lblCount As DevComponents.DotNetBar.LabelItem
    Friend WithEvents lblSelectedFile As DevComponents.DotNetBar.LabelItem
    Friend WithEvents PanelEx2 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents FileSize As System.Windows.Forms.ColumnHeader
    Friend WithEvents lblTotalFileSize As DevComponents.DotNetBar.LabelItem
End Class
