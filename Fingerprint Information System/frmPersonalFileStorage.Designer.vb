<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPersonalFileStorage
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPersonalFileStorage))
        Me.bgwGetPassword = New System.ComponentModel.BackgroundWorker()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.bgwUploadFile = New System.ComponentModel.BackgroundWorker()
        Me.PanelEx1 = New DevComponents.DotNetBar.PanelEx()
        Me.PanelEx3 = New DevComponents.DotNetBar.PanelEx()
        Me.GroupPanel1 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.SideNav1 = New DevComponents.DotNetBar.Controls.SideNav()
        Me.SideNavPanel1 = New DevComponents.DotNetBar.Controls.SideNavPanel()
        Me.CircularProgress1 = New DevComponents.DotNetBar.Controls.CircularProgress()
        Me.lblProgressStatus = New DevComponents.DotNetBar.LabelX()
        Me.listViewEx1 = New DevComponents.DotNetBar.Controls.ListViewEx()
        Me.FileName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.UploadedDate = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.FileSize = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.FileID = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.FileOwner = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.SideNavPanel2 = New DevComponents.DotNetBar.Controls.SideNavPanel()
        Me.SideNavItem1 = New DevComponents.DotNetBar.Controls.SideNavItem()
        Me.Separator1 = New DevComponents.DotNetBar.Separator()
        Me.btnLogin = New DevComponents.DotNetBar.Controls.SideNavItem()
        Me.Separator2 = New DevComponents.DotNetBar.Separator()
        Me.btnMyFiles = New DevComponents.DotNetBar.Controls.SideNavItem()
        Me.btnSharedWithMe = New DevComponents.DotNetBar.Controls.SideNavItem()
        Me.Separator3 = New DevComponents.DotNetBar.Separator()
        Me.btnChangePassword = New DevComponents.DotNetBar.Controls.SideNavItem()
        Me.Bar1 = New DevComponents.DotNetBar.Bar()
        Me.lblItemCount = New DevComponents.DotNetBar.LabelItem()
        Me.lblDriveSpaceUsed = New DevComponents.DotNetBar.LabelItem()
        Me.lblCurrentFolderPath = New DevComponents.DotNetBar.LabelItem()
        Me.PanelEx2 = New DevComponents.DotNetBar.PanelEx()
        Me.btnShareFile = New DevComponents.DotNetBar.ButtonX()
        Me.btnRename = New DevComponents.DotNetBar.ButtonX()
        Me.btnNewFolder = New DevComponents.DotNetBar.ButtonX()
        Me.btnUploadFile = New DevComponents.DotNetBar.ButtonX()
        Me.btnDownloadFile = New DevComponents.DotNetBar.ButtonX()
        Me.btnRemoveFile = New DevComponents.DotNetBar.ButtonX()
        Me.btnRefresh = New DevComponents.DotNetBar.ButtonX()
        Me.bgwUpdateFileContent = New System.ComponentModel.BackgroundWorker()
        Me.bgwListFiles = New System.ComponentModel.BackgroundWorker()
        Me.bgwDownloadFile = New System.ComponentModel.BackgroundWorker()
        Me.PanelEx1.SuspendLayout()
        Me.PanelEx3.SuspendLayout()
        Me.GroupPanel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SideNav1.SuspendLayout()
        Me.SideNavPanel1.SuspendLayout()
        CType(Me.Bar1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelEx2.SuspendLayout()
        Me.SuspendLayout()
        '
        'bgwGetPassword
        '
        Me.bgwGetPassword.WorkerReportsProgress = True
        Me.bgwGetPassword.WorkerSupportsCancellation = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'bgwUploadFile
        '
        Me.bgwUploadFile.WorkerReportsProgress = True
        Me.bgwUploadFile.WorkerSupportsCancellation = True
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
        Me.PanelEx1.Size = New System.Drawing.Size(1117, 520)
        Me.PanelEx1.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx1.Style.GradientAngle = 90
        Me.PanelEx1.TabIndex = 20
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
        Me.PanelEx3.Size = New System.Drawing.Size(960, 520)
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
        Me.GroupPanel1.Controls.Add(Me.PictureBox1)
        Me.GroupPanel1.Controls.Add(Me.SideNav1)
        Me.GroupPanel1.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupPanel1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupPanel1.Location = New System.Drawing.Point(0, 0)
        Me.GroupPanel1.Name = "GroupPanel1"
        Me.GroupPanel1.Size = New System.Drawing.Size(960, 497)
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
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(0, 440)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(49, 49)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 10
        Me.PictureBox1.TabStop = False
        '
        'SideNav1
        '
        Me.SideNav1.Controls.Add(Me.SideNavPanel1)
        Me.SideNav1.Controls.Add(Me.SideNavPanel2)
        Me.SideNav1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SideNav1.EnableClose = False
        Me.SideNav1.EnableMaximize = False
        Me.SideNav1.Items.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.SideNavItem1, Me.Separator1, Me.btnLogin, Me.Separator2, Me.btnMyFiles, Me.btnSharedWithMe, Me.Separator3, Me.btnChangePassword})
        Me.SideNav1.Location = New System.Drawing.Point(0, 0)
        Me.SideNav1.Name = "SideNav1"
        Me.SideNav1.Padding = New System.Windows.Forms.Padding(1)
        Me.SideNav1.Size = New System.Drawing.Size(954, 491)
        Me.SideNav1.TabIndex = 5
        '
        'SideNavPanel1
        '
        Me.SideNavPanel1.Controls.Add(Me.CircularProgress1)
        Me.SideNavPanel1.Controls.Add(Me.lblProgressStatus)
        Me.SideNavPanel1.Controls.Add(Me.listViewEx1)
        Me.SideNavPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SideNavPanel1.Location = New System.Drawing.Point(147, 31)
        Me.SideNavPanel1.Name = "SideNavPanel1"
        Me.SideNavPanel1.Size = New System.Drawing.Size(802, 459)
        Me.SideNavPanel1.TabIndex = 2
        '
        'CircularProgress1
        '
        Me.CircularProgress1.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.CircularProgress1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.CircularProgress1.FocusCuesEnabled = False
        Me.CircularProgress1.Location = New System.Drawing.Point(620, 157)
        Me.CircularProgress1.Name = "CircularProgress1"
        Me.CircularProgress1.ProgressColor = System.Drawing.Color.Red
        Me.CircularProgress1.ProgressTextVisible = True
        Me.CircularProgress1.Size = New System.Drawing.Size(120, 120)
        Me.CircularProgress1.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP
        Me.CircularProgress1.TabIndex = 3
        Me.CircularProgress1.TabStop = False
        Me.CircularProgress1.Visible = False
        '
        'lblProgressStatus
        '
        Me.lblProgressStatus.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.lblProgressStatus.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblProgressStatus.Location = New System.Drawing.Point(577, 283)
        Me.lblProgressStatus.Name = "lblProgressStatus"
        Me.lblProgressStatus.Size = New System.Drawing.Size(207, 18)
        Me.lblProgressStatus.TabIndex = 4
        Me.lblProgressStatus.Text = "Fetching Files from Google Drive..."
        Me.lblProgressStatus.TextAlignment = System.Drawing.StringAlignment.Center
        Me.lblProgressStatus.Visible = False
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
        Me.listViewEx1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.FileName, Me.UploadedDate, Me.FileSize, Me.FileID, Me.FileOwner})
        Me.listViewEx1.DisabledBackColor = System.Drawing.Color.Empty
        Me.listViewEx1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.listViewEx1.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.listViewEx1.ForeColor = System.Drawing.Color.Black
        Me.listViewEx1.FullRowSelect = True
        Me.listViewEx1.GridLines = True
        Me.listViewEx1.HideSelection = False
        Me.listViewEx1.Location = New System.Drawing.Point(0, 0)
        Me.listViewEx1.MultiSelect = False
        Me.listViewEx1.Name = "listViewEx1"
        Me.listViewEx1.ShowItemToolTips = True
        Me.listViewEx1.Size = New System.Drawing.Size(802, 459)
        Me.listViewEx1.SmallImageList = Me.ImageList1
        Me.listViewEx1.TabIndex = 0
        Me.listViewEx1.UseCompatibleStateImageBehavior = False
        Me.listViewEx1.View = System.Windows.Forms.View.Details
        '
        'FileName
        '
        Me.FileName.Text = "File Name"
        Me.FileName.Width = 325
        '
        'UploadedDate
        '
        Me.UploadedDate.Text = "Uploaded Date"
        Me.UploadedDate.Width = 166
        '
        'FileSize
        '
        Me.FileSize.Text = "File Size"
        Me.FileSize.Width = 93
        '
        'FileID
        '
        Me.FileID.Text = "File ID"
        Me.FileID.Width = 0
        '
        'FileOwner
        '
        Me.FileOwner.Text = "Shared By"
        Me.FileOwner.Width = 190
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
        'SideNavPanel2
        '
        Me.SideNavPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SideNavPanel2.Location = New System.Drawing.Point(147, 31)
        Me.SideNavPanel2.Name = "SideNavPanel2"
        Me.SideNavPanel2.Size = New System.Drawing.Size(802, 459)
        Me.SideNavPanel2.TabIndex = 6
        Me.SideNavPanel2.Visible = False
        '
        'SideNavItem1
        '
        Me.SideNavItem1.IsSystemMenu = True
        Me.SideNavItem1.Name = "SideNavItem1"
        Me.SideNavItem1.Symbol = ""
        Me.SideNavItem1.Text = "Menu"
        '
        'Separator1
        '
        Me.Separator1.FixedSize = New System.Drawing.Size(3, 1)
        Me.Separator1.Name = "Separator1"
        Me.Separator1.Padding.Bottom = 2
        Me.Separator1.Padding.Left = 6
        Me.Separator1.Padding.Right = 6
        Me.Separator1.Padding.Top = 2
        Me.Separator1.SeparatorOrientation = DevComponents.DotNetBar.eDesignMarkerOrientation.Vertical
        '
        'btnLogin
        '
        Me.btnLogin.Name = "btnLogin"
        Me.btnLogin.Symbol = ""
        Me.btnLogin.Text = "Google Login"
        '
        'Separator2
        '
        Me.Separator2.FixedSize = New System.Drawing.Size(3, 1)
        Me.Separator2.Name = "Separator2"
        Me.Separator2.Padding.Bottom = 2
        Me.Separator2.Padding.Left = 6
        Me.Separator2.Padding.Right = 6
        Me.Separator2.Padding.Top = 2
        Me.Separator2.SeparatorOrientation = DevComponents.DotNetBar.eDesignMarkerOrientation.Vertical
        '
        'btnMyFiles
        '
        Me.btnMyFiles.Checked = True
        Me.btnMyFiles.Name = "btnMyFiles"
        Me.btnMyFiles.Panel = Me.SideNavPanel1
        Me.btnMyFiles.Symbol = ""
        Me.btnMyFiles.Text = "My Files"
        '
        'btnSharedWithMe
        '
        Me.btnSharedWithMe.Name = "btnSharedWithMe"
        Me.btnSharedWithMe.Panel = Me.SideNavPanel2
        Me.btnSharedWithMe.Symbol = "59603"
        Me.btnSharedWithMe.SymbolSet = DevComponents.DotNetBar.eSymbolSet.Material
        Me.btnSharedWithMe.Text = "Shared with Me"
        '
        'Separator3
        '
        Me.Separator3.FixedSize = New System.Drawing.Size(3, 1)
        Me.Separator3.Name = "Separator3"
        Me.Separator3.Padding.Bottom = 2
        Me.Separator3.Padding.Left = 6
        Me.Separator3.Padding.Right = 6
        Me.Separator3.Padding.Top = 2
        Me.Separator3.SeparatorOrientation = DevComponents.DotNetBar.eDesignMarkerOrientation.Vertical
        '
        'btnChangePassword
        '
        Me.btnChangePassword.Name = "btnChangePassword"
        Me.btnChangePassword.Symbol = "58284"
        Me.btnChangePassword.SymbolSet = DevComponents.DotNetBar.eSymbolSet.Material
        Me.btnChangePassword.Text = "Change Password"
        '
        'Bar1
        '
        Me.Bar1.AntiAlias = True
        Me.Bar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Bar1.DockSide = DevComponents.DotNetBar.eDockSide.Document
        Me.Bar1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Bar1.IsMaximized = False
        Me.Bar1.Items.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.lblItemCount, Me.lblDriveSpaceUsed, Me.lblCurrentFolderPath})
        Me.Bar1.Location = New System.Drawing.Point(0, 497)
        Me.Bar1.Name = "Bar1"
        Me.Bar1.Size = New System.Drawing.Size(960, 23)
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
        'lblDriveSpaceUsed
        '
        Me.lblDriveSpaceUsed.BorderType = DevComponents.DotNetBar.eBorderType.DoubleLine
        Me.lblDriveSpaceUsed.Name = "lblDriveSpaceUsed"
        Me.lblDriveSpaceUsed.Text = "Drive Space used: "
        Me.lblDriveSpaceUsed.Width = 250
        '
        'lblCurrentFolderPath
        '
        Me.lblCurrentFolderPath.BorderType = DevComponents.DotNetBar.eBorderType.DoubleLine
        Me.lblCurrentFolderPath.Name = "lblCurrentFolderPath"
        Me.lblCurrentFolderPath.Width = 450
        '
        'PanelEx2
        '
        Me.PanelEx2.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.PanelEx2.Controls.Add(Me.btnShareFile)
        Me.PanelEx2.Controls.Add(Me.btnRename)
        Me.PanelEx2.Controls.Add(Me.btnNewFolder)
        Me.PanelEx2.Controls.Add(Me.btnUploadFile)
        Me.PanelEx2.Controls.Add(Me.btnDownloadFile)
        Me.PanelEx2.Controls.Add(Me.btnRemoveFile)
        Me.PanelEx2.Controls.Add(Me.btnRefresh)
        Me.PanelEx2.DisabledBackColor = System.Drawing.Color.Empty
        Me.PanelEx2.Dock = System.Windows.Forms.DockStyle.Right
        Me.PanelEx2.Location = New System.Drawing.Point(960, 0)
        Me.PanelEx2.Name = "PanelEx2"
        Me.PanelEx2.Size = New System.Drawing.Size(157, 520)
        Me.PanelEx2.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx2.Style.GradientAngle = 90
        Me.PanelEx2.TabIndex = 16
        '
        'btnShareFile
        '
        Me.btnShareFile.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnShareFile.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnShareFile.Image = CType(resources.GetObject("btnShareFile.Image"), System.Drawing.Image)
        Me.btnShareFile.Location = New System.Drawing.Point(13, 298)
        Me.btnShareFile.Name = "btnShareFile"
        Me.btnShareFile.Size = New System.Drawing.Size(131, 59)
        Me.btnShareFile.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.btnShareFile.TabIndex = 5
        Me.btnShareFile.Text = "Share"
        '
        'btnRename
        '
        Me.btnRename.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnRename.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnRename.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRename.Image = CType(resources.GetObject("btnRename.Image"), System.Drawing.Image)
        Me.btnRename.Location = New System.Drawing.Point(13, 365)
        Me.btnRename.Name = "btnRename"
        Me.btnRename.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.F2)
        Me.btnRename.Size = New System.Drawing.Size(131, 59)
        Me.btnRename.TabIndex = 6
        Me.btnRename.Text = "Rename"
        '
        'btnNewFolder
        '
        Me.btnNewFolder.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnNewFolder.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnNewFolder.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNewFolder.Image = CType(resources.GetObject("btnNewFolder.Image"), System.Drawing.Image)
        Me.btnNewFolder.Location = New System.Drawing.Point(13, 97)
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
        Me.btnUploadFile.Location = New System.Drawing.Point(13, 164)
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
        Me.btnDownloadFile.Location = New System.Drawing.Point(13, 231)
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
        Me.btnRemoveFile.Location = New System.Drawing.Point(13, 432)
        Me.btnRemoveFile.Name = "btnRemoveFile"
        Me.btnRemoveFile.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.Del)
        Me.btnRemoveFile.Size = New System.Drawing.Size(131, 59)
        Me.btnRemoveFile.TabIndex = 7
        Me.btnRemoveFile.Text = "Remove"
        '
        'btnRefresh
        '
        Me.btnRefresh.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnRefresh.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.btnRefresh.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRefresh.Image = CType(resources.GetObject("btnRefresh.Image"), System.Drawing.Image)
        Me.btnRefresh.Location = New System.Drawing.Point(13, 30)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.F5)
        Me.btnRefresh.Size = New System.Drawing.Size(131, 59)
        Me.btnRefresh.TabIndex = 1
        Me.btnRefresh.Text = "Refresh List"
        '
        'bgwUpdateFileContent
        '
        Me.bgwUpdateFileContent.WorkerReportsProgress = True
        Me.bgwUpdateFileContent.WorkerSupportsCancellation = True
        '
        'bgwListFiles
        '
        Me.bgwListFiles.WorkerReportsProgress = True
        Me.bgwListFiles.WorkerSupportsCancellation = True
        '
        'bgwDownloadFile
        '
        Me.bgwDownloadFile.WorkerReportsProgress = True
        Me.bgwDownloadFile.WorkerSupportsCancellation = True
        '
        'frmPersonalFileStorage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1117, 520)
        Me.Controls.Add(Me.PanelEx1)
        Me.DoubleBuffered = True
        Me.EnableGlass = False
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmPersonalFileStorage"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Personal File Storage"
        Me.TitleText = "<b>Personal File Storage</b>"
        Me.PanelEx1.ResumeLayout(False)
        Me.PanelEx3.ResumeLayout(False)
        Me.GroupPanel1.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SideNav1.ResumeLayout(False)
        Me.SideNav1.PerformLayout()
        Me.SideNavPanel1.ResumeLayout(False)
        CType(Me.Bar1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelEx2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents bgwGetPassword As System.ComponentModel.BackgroundWorker
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents bgwUploadFile As System.ComponentModel.BackgroundWorker
    Friend WithEvents PanelEx1 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents PanelEx3 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents GroupPanel1 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents lblProgressStatus As DevComponents.DotNetBar.LabelX
    Friend WithEvents CircularProgress1 As DevComponents.DotNetBar.Controls.CircularProgress
    Private WithEvents listViewEx1 As DevComponents.DotNetBar.Controls.ListViewEx
    Private WithEvents FileName As System.Windows.Forms.ColumnHeader
    Friend WithEvents UploadedDate As System.Windows.Forms.ColumnHeader
    Friend WithEvents FileSize As System.Windows.Forms.ColumnHeader
    Friend WithEvents FileID As System.Windows.Forms.ColumnHeader
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents Bar1 As DevComponents.DotNetBar.Bar
    Friend WithEvents lblItemCount As DevComponents.DotNetBar.LabelItem
    Friend WithEvents lblDriveSpaceUsed As DevComponents.DotNetBar.LabelItem
    Friend WithEvents lblCurrentFolderPath As DevComponents.DotNetBar.LabelItem
    Friend WithEvents PanelEx2 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents btnRename As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnNewFolder As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnUploadFile As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnDownloadFile As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnRemoveFile As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnRefresh As DevComponents.DotNetBar.ButtonX
    Friend WithEvents bgwUpdateFileContent As System.ComponentModel.BackgroundWorker
    Friend WithEvents bgwListFiles As System.ComponentModel.BackgroundWorker
    Friend WithEvents bgwDownloadFile As System.ComponentModel.BackgroundWorker
    Friend WithEvents btnShareFile As DevComponents.DotNetBar.ButtonX
    Friend WithEvents SideNav1 As DevComponents.DotNetBar.Controls.SideNav
    Friend WithEvents SideNavPanel1 As DevComponents.DotNetBar.Controls.SideNavPanel
    Friend WithEvents SideNavItem1 As DevComponents.DotNetBar.Controls.SideNavItem
    Friend WithEvents Separator1 As DevComponents.DotNetBar.Separator
    Friend WithEvents btnMyFiles As DevComponents.DotNetBar.Controls.SideNavItem
    Friend WithEvents btnSharedWithMe As DevComponents.DotNetBar.Controls.SideNavItem
    Friend WithEvents SideNavPanel2 As DevComponents.DotNetBar.Controls.SideNavPanel
    Friend WithEvents Separator2 As DevComponents.DotNetBar.Separator
    Friend WithEvents FileOwner As System.Windows.Forms.ColumnHeader
    Friend WithEvents SideNavItem2 As DevComponents.DotNetBar.Controls.SideNavItem
    Friend WithEvents btnLogin As DevComponents.DotNetBar.Controls.SideNavItem
    Friend WithEvents btnChangePassword As DevComponents.DotNetBar.Controls.SideNavItem
    Friend WithEvents Separator3 As DevComponents.DotNetBar.Separator
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
End Class
