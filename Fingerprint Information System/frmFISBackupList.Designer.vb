<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFISBackupList
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFISBackupList))
        Me.bgwDownloadFile = New System.ComponentModel.BackgroundWorker()
        Me.bgwListFiles = New System.ComponentModel.BackgroundWorker()
        Me.PanelEx2 = New DevComponents.DotNetBar.PanelEx()
        Me.btnGetAdminPrivilege = New DevComponents.DotNetBar.ButtonX()
        Me.btnUpdateFileContent = New DevComponents.DotNetBar.ButtonX()
        Me.btnRename = New DevComponents.DotNetBar.ButtonX()
        Me.btnNewFolder = New DevComponents.DotNetBar.ButtonX()
        Me.btnUploadFile = New DevComponents.DotNetBar.ButtonX()
        Me.btnDownloadFile = New DevComponents.DotNetBar.ButtonX()
        Me.btnRemoveFile = New DevComponents.DotNetBar.ButtonX()
        Me.btnRefresh = New DevComponents.DotNetBar.ButtonX()
        Me.lblDriveSpaceUsed = New DevComponents.DotNetBar.LabelItem()
        Me.Bar1 = New DevComponents.DotNetBar.Bar()
        Me.lblItemCount = New DevComponents.DotNetBar.LabelItem()
        Me.lblCurrentFolderPath = New DevComponents.DotNetBar.LabelItem()
        Me.FileSize = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.UploadedDate = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.FileName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.GroupPanel1 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.lblProgressStatus = New DevComponents.DotNetBar.LabelX()
        Me.CircularProgress1 = New DevComponents.DotNetBar.Controls.CircularProgress()
        Me.listViewEx1 = New DevComponents.DotNetBar.Controls.ListViewEx()
        Me.FileID = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.UploadedBy = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.PanelEx3 = New DevComponents.DotNetBar.PanelEx()
        Me.PanelEx1 = New DevComponents.DotNetBar.PanelEx()
        Me.bgwUploadFile = New System.ComponentModel.BackgroundWorker()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.bgwUpdateFileContent = New System.ComponentModel.BackgroundWorker()
        Me.bgwGetPassword = New System.ComponentModel.BackgroundWorker()
        Me.PanelEx2.SuspendLayout()
        CType(Me.Bar1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupPanel1.SuspendLayout()
        Me.PanelEx3.SuspendLayout()
        Me.PanelEx1.SuspendLayout()
        Me.SuspendLayout()
        '
        'bgwDownloadFile
        '
        Me.bgwDownloadFile.WorkerReportsProgress = True
        Me.bgwDownloadFile.WorkerSupportsCancellation = True
        '
        'bgwListFiles
        '
        Me.bgwListFiles.WorkerReportsProgress = True
        Me.bgwListFiles.WorkerSupportsCancellation = True
        '
        'PanelEx2
        '
        Me.PanelEx2.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.PanelEx2.Controls.Add(Me.btnGetAdminPrivilege)
        Me.PanelEx2.Controls.Add(Me.btnUpdateFileContent)
        Me.PanelEx2.Controls.Add(Me.btnRename)
        Me.PanelEx2.Controls.Add(Me.btnNewFolder)
        Me.PanelEx2.Controls.Add(Me.btnUploadFile)
        Me.PanelEx2.Controls.Add(Me.btnDownloadFile)
        Me.PanelEx2.Controls.Add(Me.btnRemoveFile)
        Me.PanelEx2.Controls.Add(Me.btnRefresh)
        Me.PanelEx2.DisabledBackColor = System.Drawing.Color.Empty
        Me.PanelEx2.Dock = System.Windows.Forms.DockStyle.Right
        Me.PanelEx2.Location = New System.Drawing.Point(803, 0)
        Me.PanelEx2.Name = "PanelEx2"
        Me.PanelEx2.Size = New System.Drawing.Size(163, 512)
        Me.PanelEx2.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx2.Style.GradientAngle = 90
        Me.PanelEx2.TabIndex = 16
        '
        'btnGetAdminPrivilege
        '
        Me.btnGetAdminPrivilege.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnGetAdminPrivilege.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnGetAdminPrivilege.Image = CType(resources.GetObject("btnGetAdminPrivilege.Image"), System.Drawing.Image)
        Me.btnGetAdminPrivilege.Location = New System.Drawing.Point(16, 423)
        Me.btnGetAdminPrivilege.Name = "btnGetAdminPrivilege"
        Me.btnGetAdminPrivilege.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlShiftS)
        Me.btnGetAdminPrivilege.Size = New System.Drawing.Size(131, 59)
        Me.btnGetAdminPrivilege.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnGetAdminPrivilege.TabIndex = 7
        Me.btnGetAdminPrivilege.Text = "Admin"
        '
        'btnUpdateFileContent
        '
        Me.btnUpdateFileContent.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnUpdateFileContent.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnUpdateFileContent.Location = New System.Drawing.Point(16, 486)
        Me.btnUpdateFileContent.Name = "btnUpdateFileContent"
        Me.btnUpdateFileContent.Size = New System.Drawing.Size(131, 23)
        Me.btnUpdateFileContent.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnUpdateFileContent.TabIndex = 8
        Me.btnUpdateFileContent.Text = "Update File"
        '
        'btnRename
        '
        Me.btnRename.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnRename.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnRename.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRename.Image = CType(resources.GetObject("btnRename.Image"), System.Drawing.Image)
        Me.btnRename.Location = New System.Drawing.Point(16, 293)
        Me.btnRename.Name = "btnRename"
        Me.btnRename.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.F2)
        Me.btnRename.Size = New System.Drawing.Size(131, 59)
        Me.btnRename.TabIndex = 5
        Me.btnRename.Text = "Rename"
        '
        'btnNewFolder
        '
        Me.btnNewFolder.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnNewFolder.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnNewFolder.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNewFolder.Image = CType(resources.GetObject("btnNewFolder.Image"), System.Drawing.Image)
        Me.btnNewFolder.Location = New System.Drawing.Point(16, 96)
        Me.btnNewFolder.Name = "btnNewFolder"
        Me.btnNewFolder.Size = New System.Drawing.Size(131, 59)
        Me.btnNewFolder.TabIndex = 2
        Me.btnNewFolder.Text = "New Folder"
        '
        'btnUploadFile
        '
        Me.btnUploadFile.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnUploadFile.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnUploadFile.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUploadFile.Image = CType(resources.GetObject("btnUploadFile.Image"), System.Drawing.Image)
        Me.btnUploadFile.Location = New System.Drawing.Point(16, 161)
        Me.btnUploadFile.Name = "btnUploadFile"
        Me.btnUploadFile.Size = New System.Drawing.Size(131, 59)
        Me.btnUploadFile.TabIndex = 3
        Me.btnUploadFile.Text = "Upload"
        '
        'btnDownloadFile
        '
        Me.btnDownloadFile.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnDownloadFile.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnDownloadFile.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDownloadFile.Image = CType(resources.GetObject("btnDownloadFile.Image"), System.Drawing.Image)
        Me.btnDownloadFile.Location = New System.Drawing.Point(16, 226)
        Me.btnDownloadFile.Name = "btnDownloadFile"
        Me.btnDownloadFile.Size = New System.Drawing.Size(131, 59)
        Me.btnDownloadFile.TabIndex = 4
        Me.btnDownloadFile.Text = "Download"
        '
        'btnRemoveFile
        '
        Me.btnRemoveFile.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnRemoveFile.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnRemoveFile.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRemoveFile.Image = CType(resources.GetObject("btnRemoveFile.Image"), System.Drawing.Image)
        Me.btnRemoveFile.Location = New System.Drawing.Point(16, 358)
        Me.btnRemoveFile.Name = "btnRemoveFile"
        Me.btnRemoveFile.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.Del)
        Me.btnRemoveFile.Size = New System.Drawing.Size(131, 59)
        Me.btnRemoveFile.TabIndex = 6
        Me.btnRemoveFile.Text = "Remove"
        '
        'btnRefresh
        '
        Me.btnRefresh.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnRefresh.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnRefresh.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRefresh.Image = CType(resources.GetObject("btnRefresh.Image"), System.Drawing.Image)
        Me.btnRefresh.Location = New System.Drawing.Point(16, 31)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.F5)
        Me.btnRefresh.Size = New System.Drawing.Size(131, 59)
        Me.btnRefresh.TabIndex = 1
        Me.btnRefresh.Text = "Refresh List"
        '
        'lblDriveSpaceUsed
        '
        Me.lblDriveSpaceUsed.BorderType = DevComponents.DotNetBar.eBorderType.DoubleLine
        Me.lblDriveSpaceUsed.Name = "lblDriveSpaceUsed"
        Me.lblDriveSpaceUsed.Text = "Drive Space used: "
        Me.lblDriveSpaceUsed.Width = 250
        '
        'Bar1
        '
        Me.Bar1.AntiAlias = True
        Me.Bar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Bar1.DockSide = DevComponents.DotNetBar.eDockSide.Document
        Me.Bar1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Bar1.IsMaximized = False
        Me.Bar1.Items.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.lblItemCount, Me.lblDriveSpaceUsed, Me.lblCurrentFolderPath})
        Me.Bar1.Location = New System.Drawing.Point(0, 489)
        Me.Bar1.Name = "Bar1"
        Me.Bar1.Size = New System.Drawing.Size(803, 23)
        Me.Bar1.Stretch = True
        Me.Bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.Bar1.TabIndex = 24
        Me.Bar1.TabStop = False
        '
        'lblItemCount
        '
        Me.lblItemCount.BorderType = DevComponents.DotNetBar.eBorderType.DoubleLine
        Me.lblItemCount.Name = "lblItemCount"
        Me.lblItemCount.Text = "Item Count:"
        Me.lblItemCount.Width = 100
        '
        'lblCurrentFolderPath
        '
        Me.lblCurrentFolderPath.BorderType = DevComponents.DotNetBar.eBorderType.DoubleLine
        Me.lblCurrentFolderPath.Name = "lblCurrentFolderPath"
        Me.lblCurrentFolderPath.Width = 450
        '
        'FileSize
        '
        Me.FileSize.Text = "File Size"
        Me.FileSize.Width = 70
        '
        'UploadedDate
        '
        Me.UploadedDate.Text = "Uploaded Date"
        Me.UploadedDate.Width = 140
        '
        'FileName
        '
        Me.FileName.Text = "File Name"
        Me.FileName.Width = 300
        '
        'GroupPanel1
        '
        Me.GroupPanel1.BackColor = System.Drawing.Color.Transparent
        Me.GroupPanel1.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanel1.Controls.Add(Me.lblProgressStatus)
        Me.GroupPanel1.Controls.Add(Me.CircularProgress1)
        Me.GroupPanel1.Controls.Add(Me.listViewEx1)
        Me.GroupPanel1.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupPanel1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupPanel1.Location = New System.Drawing.Point(0, 0)
        Me.GroupPanel1.Name = "GroupPanel1"
        Me.GroupPanel1.Size = New System.Drawing.Size(803, 489)
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
        Me.GroupPanel1.TabIndex = 25
        '
        'lblProgressStatus
        '
        Me.lblProgressStatus.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.lblProgressStatus.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblProgressStatus.Location = New System.Drawing.Point(295, 295)
        Me.lblProgressStatus.Name = "lblProgressStatus"
        Me.lblProgressStatus.Size = New System.Drawing.Size(207, 18)
        Me.lblProgressStatus.TabIndex = 4
        Me.lblProgressStatus.Text = "Fetching Files from Google Drive..."
        Me.lblProgressStatus.TextAlignment = System.Drawing.StringAlignment.Center
        Me.lblProgressStatus.Visible = False
        '
        'CircularProgress1
        '
        Me.CircularProgress1.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.CircularProgress1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.CircularProgress1.FocusCuesEnabled = False
        Me.CircularProgress1.Location = New System.Drawing.Point(338, 170)
        Me.CircularProgress1.Name = "CircularProgress1"
        Me.CircularProgress1.ProgressColor = System.Drawing.Color.Red
        Me.CircularProgress1.ProgressTextVisible = True
        Me.CircularProgress1.Size = New System.Drawing.Size(120, 120)
        Me.CircularProgress1.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP
        Me.CircularProgress1.TabIndex = 3
        Me.CircularProgress1.TabStop = False
        Me.CircularProgress1.Visible = False
        '
        'listViewEx1
        '
        Me.listViewEx1.Alignment = System.Windows.Forms.ListViewAlignment.Left
        Me.listViewEx1.AllowDrop = True
        Me.listViewEx1.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.listViewEx1.Border.Class = "ListViewBorder"
        Me.listViewEx1.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.listViewEx1.ColumnHeaderFont = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.listViewEx1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.FileName, Me.UploadedDate, Me.FileSize, Me.FileID, Me.UploadedBy})
        Me.listViewEx1.DisabledBackColor = System.Drawing.Color.Empty
        Me.listViewEx1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.listViewEx1.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.listViewEx1.ForeColor = System.Drawing.Color.Black
        Me.listViewEx1.FullRowSelect = True
        Me.listViewEx1.GridLines = True
        Me.listViewEx1.HideSelection = False
        Me.listViewEx1.Location = New System.Drawing.Point(0, 0)
        Me.listViewEx1.Name = "listViewEx1"
        Me.listViewEx1.ShowItemToolTips = True
        Me.listViewEx1.Size = New System.Drawing.Size(797, 483)
        Me.listViewEx1.SmallImageList = Me.ImageList1
        Me.listViewEx1.TabIndex = 0
        Me.listViewEx1.UseCompatibleStateImageBehavior = False
        Me.listViewEx1.View = System.Windows.Forms.View.Details
        '
        'FileID
        '
        Me.FileID.Text = "File ID"
        Me.FileID.Width = 290
        '
        'UploadedBy
        '
        Me.UploadedBy.Text = "Uploaded By"
        Me.UploadedBy.Width = 280
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "Folder small 16x16.png")
        Me.ImageList1.Images.SetKeyName(1, "gdrive round 48x48.png")
        Me.ImageList1.Images.SetKeyName(2, "Back 16x16.png")
        Me.ImageList1.Images.SetKeyName(3, "MS Access.png")
        Me.ImageList1.Images.SetKeyName(4, "Exe 16x16.png")
        Me.ImageList1.Images.SetKeyName(5, "PDF.png")
        Me.ImageList1.Images.SetKeyName(6, "Word.png")
        Me.ImageList1.Images.SetKeyName(7, "Excel.png")
        Me.ImageList1.Images.SetKeyName(8, "powerpoint.png")
        Me.ImageList1.Images.SetKeyName(9, "txt.png")
        Me.ImageList1.Images.SetKeyName(10, "jpeg.png")
        Me.ImageList1.Images.SetKeyName(11, "rar.png")
        Me.ImageList1.Images.SetKeyName(12, "BlankFile.png")
        '
        'PanelEx3
        '
        Me.PanelEx3.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.PanelEx3.Controls.Add(Me.GroupPanel1)
        Me.PanelEx3.Controls.Add(Me.Bar1)
        Me.PanelEx3.DisabledBackColor = System.Drawing.Color.Empty
        Me.PanelEx3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelEx3.Location = New System.Drawing.Point(0, 0)
        Me.PanelEx3.Name = "PanelEx3"
        Me.PanelEx3.Size = New System.Drawing.Size(803, 512)
        Me.PanelEx3.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx3.Style.GradientAngle = 90
        Me.PanelEx3.TabIndex = 27
        Me.PanelEx3.Text = "PanelEx3"
        '
        'PanelEx1
        '
        Me.PanelEx1.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.PanelEx1.Controls.Add(Me.PanelEx3)
        Me.PanelEx1.Controls.Add(Me.PanelEx2)
        Me.PanelEx1.DisabledBackColor = System.Drawing.Color.Empty
        Me.PanelEx1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelEx1.Location = New System.Drawing.Point(0, 0)
        Me.PanelEx1.Name = "PanelEx1"
        Me.PanelEx1.Size = New System.Drawing.Size(966, 512)
        Me.PanelEx1.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx1.Style.GradientAngle = 90
        Me.PanelEx1.TabIndex = 16
        '
        'bgwUploadFile
        '
        Me.bgwUploadFile.WorkerReportsProgress = True
        Me.bgwUploadFile.WorkerSupportsCancellation = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'bgwUpdateFileContent
        '
        Me.bgwUpdateFileContent.WorkerReportsProgress = True
        Me.bgwUpdateFileContent.WorkerSupportsCancellation = True
        '
        'bgwGetPassword
        '
        Me.bgwGetPassword.WorkerReportsProgress = True
        Me.bgwGetPassword.WorkerSupportsCancellation = True
        '
        'frmFISBackupList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(966, 512)
        Me.Controls.Add(Me.PanelEx1)
        Me.DoubleBuffered = True
        Me.EnableGlass = False
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmFISBackupList"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "FIS Online File List"
        Me.TitleText = "<b>FIS Online File List</b>"
        Me.PanelEx2.ResumeLayout(False)
        CType(Me.Bar1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupPanel1.ResumeLayout(False)
        Me.PanelEx3.ResumeLayout(False)
        Me.PanelEx1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnDownloadFile As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnRemoveFile As DevComponents.DotNetBar.ButtonX
    Friend WithEvents bgwDownloadFile As System.ComponentModel.BackgroundWorker
    Friend WithEvents bgwListFiles As System.ComponentModel.BackgroundWorker
    Friend WithEvents PanelEx2 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents btnRefresh As DevComponents.DotNetBar.ButtonX
    Friend WithEvents lblDriveSpaceUsed As DevComponents.DotNetBar.LabelItem
    Friend WithEvents Bar1 As DevComponents.DotNetBar.Bar
    Friend WithEvents FileSize As System.Windows.Forms.ColumnHeader
    Friend WithEvents UploadedDate As System.Windows.Forms.ColumnHeader
    Private WithEvents FileName As System.Windows.Forms.ColumnHeader
    Friend WithEvents GroupPanel1 As DevComponents.DotNetBar.Controls.GroupPanel
    Private WithEvents listViewEx1 As DevComponents.DotNetBar.Controls.ListViewEx
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents PanelEx3 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents PanelEx1 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents FileID As System.Windows.Forms.ColumnHeader
    Friend WithEvents lblProgressStatus As DevComponents.DotNetBar.LabelX
    Friend WithEvents CircularProgress1 As DevComponents.DotNetBar.Controls.CircularProgress
    Friend WithEvents lblItemCount As DevComponents.DotNetBar.LabelItem
    Friend WithEvents btnUploadFile As DevComponents.DotNetBar.ButtonX
    Friend WithEvents bgwUploadFile As System.ComponentModel.BackgroundWorker
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents UploadedBy As System.Windows.Forms.ColumnHeader
    Friend WithEvents btnNewFolder As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnRename As DevComponents.DotNetBar.ButtonX
    Friend WithEvents lblCurrentFolderPath As DevComponents.DotNetBar.LabelItem
    Friend WithEvents btnGetAdminPrivilege As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnUpdateFileContent As DevComponents.DotNetBar.ButtonX
    Friend WithEvents bgwUpdateFileContent As System.ComponentModel.BackgroundWorker
    Friend WithEvents bgwGetPassword As System.ComponentModel.BackgroundWorker
End Class
