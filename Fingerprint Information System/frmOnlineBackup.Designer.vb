﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOnlineBackup
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmOnlineBackup))
        Me.btnBackupDatabase = New DevComponents.DotNetBar.ButtonX()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.btnRemoveBackupFile = New DevComponents.DotNetBar.ButtonX()
        Me.btnDownloadDatabase = New DevComponents.DotNetBar.ButtonX()
        Me.btnRefresh = New DevComponents.DotNetBar.ButtonX()
        Me.btnRestoreDatabase = New DevComponents.DotNetBar.ButtonX()
        Me.btnOpenFileMSAccess = New DevComponents.DotNetBar.ButtonX()
        Me.btnOpenBackupFolder = New DevComponents.DotNetBar.ButtonX()
        Me.PanelEx1 = New DevComponents.DotNetBar.PanelEx()
        Me.PanelEx3 = New DevComponents.DotNetBar.PanelEx()
        Me.GroupPanel1 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.ContextMenuBar1 = New DevComponents.DotNetBar.ContextMenuBar()
        Me.ButtonItem1 = New DevComponents.DotNetBar.ButtonItem()
        Me.btnRefreshCM = New DevComponents.DotNetBar.ButtonItem()
        Me.btnUploadCM = New DevComponents.DotNetBar.ButtonItem()
        Me.btnDownloadCM = New DevComponents.DotNetBar.ButtonItem()
        Me.btnRestoreCM = New DevComponents.DotNetBar.ButtonItem()
        Me.btnPreviewCM = New DevComponents.DotNetBar.ButtonItem()
        Me.btnRemoveCM = New DevComponents.DotNetBar.ButtonItem()
        Me.btnOpenCM = New DevComponents.DotNetBar.ButtonItem()
        Me.listViewEx1 = New DevComponents.DotNetBar.Controls.ListViewEx()
        Me.BackupFileName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.BackupTime = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.FileID = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.FileSize = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.UploadedBy = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.LastModDate = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.LastSOCNo = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.DI = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.TotalRecords = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.LastModification = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lblProgressStatus = New DevComponents.DotNetBar.LabelX()
        Me.CircularProgress1 = New DevComponents.DotNetBar.Controls.CircularProgress()
        Me.Bar1 = New DevComponents.DotNetBar.Bar()
        Me.lblSelectedFolder = New DevComponents.DotNetBar.LabelItem()
        Me.lblItemCount = New DevComponents.DotNetBar.LabelItem()
        Me.lblTotalFileSize = New DevComponents.DotNetBar.LabelItem()
        Me.PanelEx2 = New DevComponents.DotNetBar.PanelEx()
        Me.btnPreview = New DevComponents.DotNetBar.ButtonX()
        Me.btnViewAllBackupFiles = New DevComponents.DotNetBar.ButtonX()
        Me.bgwListUserFiles = New System.ComponentModel.BackgroundWorker()
        Me.bgwUpload = New System.ComponentModel.BackgroundWorker()
        Me.bgwDownload = New System.ComponentModel.BackgroundWorker()
        Me.bgwGetPassword = New System.ComponentModel.BackgroundWorker()
        Me.bgwListAllFiles = New System.ComponentModel.BackgroundWorker()
        Me.PanelEx1.SuspendLayout()
        Me.PanelEx3.SuspendLayout()
        Me.GroupPanel1.SuspendLayout()
        CType(Me.ContextMenuBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Bar1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelEx2.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnBackupDatabase
        '
        Me.btnBackupDatabase.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnBackupDatabase.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnBackupDatabase.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBackupDatabase.Image = CType(resources.GetObject("btnBackupDatabase.Image"), System.Drawing.Image)
        Me.btnBackupDatabase.Location = New System.Drawing.Point(23, 78)
        Me.btnBackupDatabase.Name = "btnBackupDatabase"
        Me.btnBackupDatabase.Size = New System.Drawing.Size(117, 59)
        Me.btnBackupDatabase.TabIndex = 2
        Me.btnBackupDatabase.Text = "Backup"
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "Google-Drive-icon.png")
        Me.ImageList1.Images.SetKeyName(1, "MS Access.png")
        Me.ImageList1.Images.SetKeyName(2, "gdrive_icon-icons.com_62760.png")
        Me.ImageList1.Images.SetKeyName(3, "Folder small 16x16.png")
        '
        'btnRemoveBackupFile
        '
        Me.btnRemoveBackupFile.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnRemoveBackupFile.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnRemoveBackupFile.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRemoveBackupFile.Image = CType(resources.GetObject("btnRemoveBackupFile.Image"), System.Drawing.Image)
        Me.btnRemoveBackupFile.Location = New System.Drawing.Point(23, 334)
        Me.btnRemoveBackupFile.Name = "btnRemoveBackupFile"
        Me.btnRemoveBackupFile.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.Del)
        Me.btnRemoveBackupFile.Size = New System.Drawing.Size(117, 59)
        Me.btnRemoveBackupFile.TabIndex = 6
        Me.btnRemoveBackupFile.Text = "Remove"
        '
        'btnDownloadDatabase
        '
        Me.btnDownloadDatabase.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnDownloadDatabase.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnDownloadDatabase.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDownloadDatabase.Image = CType(resources.GetObject("btnDownloadDatabase.Image"), System.Drawing.Image)
        Me.btnDownloadDatabase.Location = New System.Drawing.Point(23, 142)
        Me.btnDownloadDatabase.Name = "btnDownloadDatabase"
        Me.btnDownloadDatabase.Size = New System.Drawing.Size(117, 59)
        Me.btnDownloadDatabase.TabIndex = 3
        Me.btnDownloadDatabase.Text = "Download"
        '
        'btnRefresh
        '
        Me.btnRefresh.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnRefresh.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnRefresh.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRefresh.Image = CType(resources.GetObject("btnRefresh.Image"), System.Drawing.Image)
        Me.btnRefresh.Location = New System.Drawing.Point(23, 14)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.F5)
        Me.btnRefresh.Size = New System.Drawing.Size(117, 59)
        Me.btnRefresh.TabIndex = 1
        Me.btnRefresh.Text = "Refresh"
        '
        'btnRestoreDatabase
        '
        Me.btnRestoreDatabase.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnRestoreDatabase.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnRestoreDatabase.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRestoreDatabase.Image = CType(resources.GetObject("btnRestoreDatabase.Image"), System.Drawing.Image)
        Me.btnRestoreDatabase.Location = New System.Drawing.Point(23, 206)
        Me.btnRestoreDatabase.Name = "btnRestoreDatabase"
        Me.btnRestoreDatabase.Size = New System.Drawing.Size(117, 59)
        Me.btnRestoreDatabase.TabIndex = 4
        Me.btnRestoreDatabase.Text = "Restore"
        '
        'btnOpenFileMSAccess
        '
        Me.btnOpenFileMSAccess.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnOpenFileMSAccess.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnOpenFileMSAccess.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOpenFileMSAccess.Image = CType(resources.GetObject("btnOpenFileMSAccess.Image"), System.Drawing.Image)
        Me.btnOpenFileMSAccess.Location = New System.Drawing.Point(23, 398)
        Me.btnOpenFileMSAccess.Name = "btnOpenFileMSAccess"
        Me.btnOpenFileMSAccess.Size = New System.Drawing.Size(117, 59)
        Me.btnOpenFileMSAccess.TabIndex = 7
        Me.btnOpenFileMSAccess.Text = "Open File"
        '
        'btnOpenBackupFolder
        '
        Me.btnOpenBackupFolder.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnOpenBackupFolder.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnOpenBackupFolder.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOpenBackupFolder.Image = CType(resources.GetObject("btnOpenBackupFolder.Image"), System.Drawing.Image)
        Me.btnOpenBackupFolder.Location = New System.Drawing.Point(23, 462)
        Me.btnOpenBackupFolder.Name = "btnOpenBackupFolder"
        Me.btnOpenBackupFolder.Size = New System.Drawing.Size(117, 59)
        Me.btnOpenBackupFolder.TabIndex = 8
        Me.btnOpenBackupFolder.Text = "Open Folder"
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
        Me.PanelEx1.Size = New System.Drawing.Size(1273, 598)
        Me.PanelEx1.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx1.Style.GradientAngle = 90
        Me.PanelEx1.TabIndex = 12
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
        Me.PanelEx3.Size = New System.Drawing.Size(1110, 598)
        Me.PanelEx3.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx3.Style.GradientAngle = 90
        Me.PanelEx3.TabIndex = 27
        Me.PanelEx3.Text = "PanelEx3"
        '
        'GroupPanel1
        '
        Me.GroupPanel1.BackColor = System.Drawing.Color.Transparent
        Me.GroupPanel1.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanel1.Controls.Add(Me.ContextMenuBar1)
        Me.GroupPanel1.Controls.Add(Me.lblProgressStatus)
        Me.GroupPanel1.Controls.Add(Me.CircularProgress1)
        Me.GroupPanel1.Controls.Add(Me.listViewEx1)
        Me.GroupPanel1.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupPanel1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupPanel1.Location = New System.Drawing.Point(0, 0)
        Me.GroupPanel1.Name = "GroupPanel1"
        Me.GroupPanel1.Size = New System.Drawing.Size(1110, 575)
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
        Me.GroupPanel1.Text = "Available Backups"
        '
        'ContextMenuBar1
        '
        Me.ContextMenuBar1.AntiAlias = True
        Me.ContextMenuBar1.DockSide = DevComponents.DotNetBar.eDockSide.Document
        Me.ContextMenuBar1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.ContextMenuBar1.IsMaximized = False
        Me.ContextMenuBar1.Items.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.ButtonItem1})
        Me.ContextMenuBar1.Location = New System.Drawing.Point(613, 71)
        Me.ContextMenuBar1.Name = "ContextMenuBar1"
        Me.ContextMenuBar1.Size = New System.Drawing.Size(75, 25)
        Me.ContextMenuBar1.Stretch = True
        Me.ContextMenuBar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.ContextMenuBar1.TabIndex = 6
        Me.ContextMenuBar1.TabStop = False
        Me.ContextMenuBar1.Text = "ContextMenuBar1"
        '
        'ButtonItem1
        '
        Me.ButtonItem1.AutoExpandOnClick = True
        Me.ButtonItem1.Name = "ButtonItem1"
        Me.ButtonItem1.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnRefreshCM, Me.btnUploadCM, Me.btnDownloadCM, Me.btnRestoreCM, Me.btnPreviewCM, Me.btnRemoveCM, Me.btnOpenCM})
        Me.ButtonItem1.Text = "ButtonItem1"
        '
        'btnRefreshCM
        '
        Me.btnRefreshCM.Image = CType(resources.GetObject("btnRefreshCM.Image"), System.Drawing.Image)
        Me.btnRefreshCM.Name = "btnRefreshCM"
        Me.btnRefreshCM.Text = "Refresh"
        '
        'btnUploadCM
        '
        Me.btnUploadCM.BeginGroup = True
        Me.btnUploadCM.Image = CType(resources.GetObject("btnUploadCM.Image"), System.Drawing.Image)
        Me.btnUploadCM.Name = "btnUploadCM"
        Me.btnUploadCM.Text = "Upload"
        '
        'btnDownloadCM
        '
        Me.btnDownloadCM.Image = CType(resources.GetObject("btnDownloadCM.Image"), System.Drawing.Image)
        Me.btnDownloadCM.Name = "btnDownloadCM"
        Me.btnDownloadCM.Text = "Download"
        '
        'btnRestoreCM
        '
        Me.btnRestoreCM.BeginGroup = True
        Me.btnRestoreCM.Image = CType(resources.GetObject("btnRestoreCM.Image"), System.Drawing.Image)
        Me.btnRestoreCM.Name = "btnRestoreCM"
        Me.btnRestoreCM.Text = "Restore"
        '
        'btnPreviewCM
        '
        Me.btnPreviewCM.Image = CType(resources.GetObject("btnPreviewCM.Image"), System.Drawing.Image)
        Me.btnPreviewCM.Name = "btnPreviewCM"
        Me.btnPreviewCM.Text = "Preview"
        '
        'btnRemoveCM
        '
        Me.btnRemoveCM.BeginGroup = True
        Me.btnRemoveCM.Image = CType(resources.GetObject("btnRemoveCM.Image"), System.Drawing.Image)
        Me.btnRemoveCM.Name = "btnRemoveCM"
        Me.btnRemoveCM.Text = "Remove"
        '
        'btnOpenCM
        '
        Me.btnOpenCM.BeginGroup = True
        Me.btnOpenCM.Image = CType(resources.GetObject("btnOpenCM.Image"), System.Drawing.Image)
        Me.btnOpenCM.Name = "btnOpenCM"
        Me.btnOpenCM.Text = "Open File"
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
        Me.listViewEx1.ColumnHeaderFont = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.listViewEx1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.BackupFileName, Me.BackupTime, Me.FileID, Me.FileSize, Me.UploadedBy, Me.LastSOCNo, Me.DI, Me.TotalRecords, Me.LastModification, Me.LastModDate})
        Me.ContextMenuBar1.SetContextMenuEx(Me.listViewEx1, Me.ButtonItem1)
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
        Me.listViewEx1.Size = New System.Drawing.Size(1104, 551)
        Me.listViewEx1.SmallImageList = Me.ImageList1
        Me.listViewEx1.TabIndex = 0
        Me.listViewEx1.UseCompatibleStateImageBehavior = False
        Me.listViewEx1.View = System.Windows.Forms.View.Details
        '
        'BackupFileName
        '
        Me.BackupFileName.Text = "Backup File"
        Me.BackupFileName.Width = 295
        '
        'BackupTime
        '
        Me.BackupTime.Text = "Backup Time"
        Me.BackupTime.Width = 130
        '
        'FileID
        '
        Me.FileID.Text = "File ID"
        Me.FileID.Width = 0
        '
        'FileSize
        '
        Me.FileSize.Text = "File Size"
        Me.FileSize.Width = 65
        '
        'UploadedBy
        '
        Me.UploadedBy.Text = "Uploaded By"
        Me.UploadedBy.Width = 100
        '
        'LastModDate
        '
        Me.LastModDate.Text = "Modification Date"
        Me.LastModDate.Width = 130
        '
        'LastSOCNo
        '
        Me.LastSOCNo.Text = "Last SOC"
        Me.LastSOCNo.Width = 70
        '
        'DI
        '
        Me.DI.Text = "DI"
        Me.DI.Width = 75
        '
        'TotalRecords
        '
        Me.TotalRecords.Text = "Records"
        Me.TotalRecords.Width = 80
        '
        'LastModification
        '
        Me.LastModification.Text = "Last Modification"
        Me.LastModification.Width = 150
        '
        'lblProgressStatus
        '
        Me.lblProgressStatus.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.lblProgressStatus.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblProgressStatus.Location = New System.Drawing.Point(453, 329)
        Me.lblProgressStatus.Name = "lblProgressStatus"
        Me.lblProgressStatus.Size = New System.Drawing.Size(199, 18)
        Me.lblProgressStatus.TabIndex = 2
        Me.lblProgressStatus.Text = "Fetching Files from Google Drive..."
        Me.lblProgressStatus.TextAlignment = System.Drawing.StringAlignment.Center
        '
        'CircularProgress1
        '
        Me.CircularProgress1.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.CircularProgress1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.CircularProgress1.FocusCuesEnabled = False
        Me.CircularProgress1.Location = New System.Drawing.Point(492, 204)
        Me.CircularProgress1.Name = "CircularProgress1"
        Me.CircularProgress1.ProgressColor = System.Drawing.Color.Red
        Me.CircularProgress1.ProgressTextVisible = True
        Me.CircularProgress1.Size = New System.Drawing.Size(120, 120)
        Me.CircularProgress1.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP
        Me.CircularProgress1.TabIndex = 1
        Me.CircularProgress1.TabStop = False
        '
        'Bar1
        '
        Me.Bar1.AntiAlias = True
        Me.Bar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Bar1.DockSide = DevComponents.DotNetBar.eDockSide.Document
        Me.Bar1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Bar1.IsMaximized = False
        Me.Bar1.Items.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.lblSelectedFolder, Me.lblItemCount, Me.lblTotalFileSize})
        Me.Bar1.Location = New System.Drawing.Point(0, 575)
        Me.Bar1.Name = "Bar1"
        Me.Bar1.Size = New System.Drawing.Size(1110, 23)
        Me.Bar1.Stretch = True
        Me.Bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.Bar1.TabIndex = 24
        Me.Bar1.TabStop = False
        '
        'lblSelectedFolder
        '
        Me.lblSelectedFolder.BorderType = DevComponents.DotNetBar.eBorderType.DoubleLine
        Me.lblSelectedFolder.Name = "lblSelectedFolder"
        Me.lblSelectedFolder.Width = 350
        '
        'lblItemCount
        '
        Me.lblItemCount.BeginGroup = True
        Me.lblItemCount.BorderType = DevComponents.DotNetBar.eBorderType.DoubleLine
        Me.lblItemCount.Name = "lblItemCount"
        Me.lblItemCount.Width = 160
        '
        'lblTotalFileSize
        '
        Me.lblTotalFileSize.BeginGroup = True
        Me.lblTotalFileSize.BorderType = DevComponents.DotNetBar.eBorderType.DoubleLine
        Me.lblTotalFileSize.Name = "lblTotalFileSize"
        Me.lblTotalFileSize.Width = 200
        '
        'PanelEx2
        '
        Me.PanelEx2.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.PanelEx2.Controls.Add(Me.btnPreview)
        Me.PanelEx2.Controls.Add(Me.btnViewAllBackupFiles)
        Me.PanelEx2.Controls.Add(Me.btnBackupDatabase)
        Me.PanelEx2.Controls.Add(Me.btnDownloadDatabase)
        Me.PanelEx2.Controls.Add(Me.btnRefresh)
        Me.PanelEx2.Controls.Add(Me.btnOpenFileMSAccess)
        Me.PanelEx2.Controls.Add(Me.btnRemoveBackupFile)
        Me.PanelEx2.Controls.Add(Me.btnOpenBackupFolder)
        Me.PanelEx2.Controls.Add(Me.btnRestoreDatabase)
        Me.PanelEx2.DisabledBackColor = System.Drawing.Color.Empty
        Me.PanelEx2.Dock = System.Windows.Forms.DockStyle.Right
        Me.PanelEx2.Location = New System.Drawing.Point(1110, 0)
        Me.PanelEx2.Name = "PanelEx2"
        Me.PanelEx2.Size = New System.Drawing.Size(163, 598)
        Me.PanelEx2.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx2.Style.GradientAngle = 90
        Me.PanelEx2.TabIndex = 16
        '
        'btnPreview
        '
        Me.btnPreview.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnPreview.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnPreview.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPreview.Image = CType(resources.GetObject("btnPreview.Image"), System.Drawing.Image)
        Me.btnPreview.Location = New System.Drawing.Point(23, 270)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(117, 59)
        Me.btnPreview.TabIndex = 5
        Me.btnPreview.Text = "Preview"
        '
        'btnViewAllBackupFiles
        '
        Me.btnViewAllBackupFiles.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnViewAllBackupFiles.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnViewAllBackupFiles.Image = CType(resources.GetObject("btnViewAllBackupFiles.Image"), System.Drawing.Image)
        Me.btnViewAllBackupFiles.Location = New System.Drawing.Point(23, 526)
        Me.btnViewAllBackupFiles.Name = "btnViewAllBackupFiles"
        Me.btnViewAllBackupFiles.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlShiftS)
        Me.btnViewAllBackupFiles.Size = New System.Drawing.Size(117, 59)
        Me.btnViewAllBackupFiles.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnViewAllBackupFiles.TabIndex = 9
        Me.btnViewAllBackupFiles.Text = "View All Backups"
        '
        'bgwListUserFiles
        '
        Me.bgwListUserFiles.WorkerReportsProgress = True
        Me.bgwListUserFiles.WorkerSupportsCancellation = True
        '
        'bgwUpload
        '
        Me.bgwUpload.WorkerReportsProgress = True
        Me.bgwUpload.WorkerSupportsCancellation = True
        '
        'bgwDownload
        '
        Me.bgwDownload.WorkerReportsProgress = True
        Me.bgwDownload.WorkerSupportsCancellation = True
        '
        'bgwGetPassword
        '
        Me.bgwGetPassword.WorkerReportsProgress = True
        Me.bgwGetPassword.WorkerSupportsCancellation = True
        '
        'bgwListAllFiles
        '
        Me.bgwListAllFiles.WorkerReportsProgress = True
        Me.bgwListAllFiles.WorkerSupportsCancellation = True
        '
        'frmOnlineBackup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1273, 598)
        Me.Controls.Add(Me.PanelEx1)
        Me.DoubleBuffered = True
        Me.EnableGlass = False
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmOnlineBackup"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Online Database Backup"
        Me.TitleText = "<b>Online Database Backup</b>"
        Me.PanelEx1.ResumeLayout(False)
        Me.PanelEx3.ResumeLayout(False)
        Me.GroupPanel1.ResumeLayout(False)
        CType(Me.ContextMenuBar1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Bar1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelEx2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnBackupDatabase As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnRemoveBackupFile As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnDownloadDatabase As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnRefresh As DevComponents.DotNetBar.ButtonX
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents btnRestoreDatabase As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnOpenFileMSAccess As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnOpenBackupFolder As DevComponents.DotNetBar.ButtonX
    Friend WithEvents PanelEx1 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents PanelEx2 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents PanelEx3 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents GroupPanel1 As DevComponents.DotNetBar.Controls.GroupPanel
    Private WithEvents listViewEx1 As DevComponents.DotNetBar.Controls.ListViewEx
    Private WithEvents BackupFileName As System.Windows.Forms.ColumnHeader
    Friend WithEvents BackupTime As System.Windows.Forms.ColumnHeader
    Friend WithEvents FileID As System.Windows.Forms.ColumnHeader
    Friend WithEvents Bar1 As DevComponents.DotNetBar.Bar
    Friend WithEvents lblItemCount As DevComponents.DotNetBar.LabelItem
    Friend WithEvents bgwListUserFiles As System.ComponentModel.BackgroundWorker
    Friend WithEvents CircularProgress1 As DevComponents.DotNetBar.Controls.CircularProgress
    Friend WithEvents lblProgressStatus As DevComponents.DotNetBar.LabelX
    Friend WithEvents bgwUpload As System.ComponentModel.BackgroundWorker
    Friend WithEvents bgwDownload As System.ComponentModel.BackgroundWorker
    Friend WithEvents FileSize As System.Windows.Forms.ColumnHeader
    Friend WithEvents UploadedBy As System.Windows.Forms.ColumnHeader
    Friend WithEvents lblTotalFileSize As DevComponents.DotNetBar.LabelItem
    Friend WithEvents btnViewAllBackupFiles As DevComponents.DotNetBar.ButtonX
    Friend WithEvents bgwGetPassword As System.ComponentModel.BackgroundWorker
    Friend WithEvents bgwListAllFiles As System.ComponentModel.BackgroundWorker
    Friend WithEvents lblSelectedFolder As DevComponents.DotNetBar.LabelItem
    Friend WithEvents LastModDate As System.Windows.Forms.ColumnHeader
    Friend WithEvents LastSOCNo As System.Windows.Forms.ColumnHeader
    Friend WithEvents DI As System.Windows.Forms.ColumnHeader
    Friend WithEvents TotalRecords As System.Windows.Forms.ColumnHeader
    Friend WithEvents ContextMenuBar1 As DevComponents.DotNetBar.ContextMenuBar
    Friend WithEvents ButtonItem1 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnRefreshCM As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnUploadCM As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnDownloadCM As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnRestoreCM As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnRemoveCM As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnOpenCM As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnPreview As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnPreviewCM As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents LastModification As System.Windows.Forms.ColumnHeader
End Class
