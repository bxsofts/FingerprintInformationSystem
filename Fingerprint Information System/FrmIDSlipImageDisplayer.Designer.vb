<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmIDSlipImageDisplayer
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmIDSlipImageDisplayer))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Bar1 = New DevComponents.DotNetBar.Bar()
        Me.btnSelectCPSlip = New DevComponents.DotNetBar.ButtonItem()
        Me.btnZoomInCPSlip = New DevComponents.DotNetBar.ButtonItem()
        Me.btnZoomOutCPSlip = New DevComponents.DotNetBar.ButtonItem()
        Me.btnCPViewMode = New DevComponents.DotNetBar.ButtonItem()
        Me.btnCPFullSize = New DevComponents.DotNetBar.ButtonItem()
        Me.btnCPFitImage = New DevComponents.DotNetBar.ButtonItem()
        Me.btnCPFitWidth = New DevComponents.DotNetBar.ButtonItem()
        Me.btnCPFitHeight = New DevComponents.DotNetBar.ButtonItem()
        Me.sldrZoomCPSlip = New DevComponents.DotNetBar.SliderItem()
        Me.CPContextMenuBar = New DevComponents.DotNetBar.ContextMenuBar()
        Me.CPButtonItem = New DevComponents.DotNetBar.ButtonItem()
        Me.ButtonItem4 = New DevComponents.DotNetBar.ButtonItem()
        Me.btnCPActualSizeContext = New DevComponents.DotNetBar.ButtonItem()
        Me.btnCPFitImageContext = New DevComponents.DotNetBar.ButtonItem()
        Me.btnCPFitWidthContext = New DevComponents.DotNetBar.ButtonItem()
        Me.btnCPFitHeightContext = New DevComponents.DotNetBar.ButtonItem()
        Me.ButtonItem3 = New DevComponents.DotNetBar.ButtonItem()
        Me.btnCPFlipVertically = New DevComponents.DotNetBar.ButtonItem()
        Me.btnCPFlipHorizontally = New DevComponents.DotNetBar.ButtonItem()
        Me.ButtonItem6 = New DevComponents.DotNetBar.ButtonItem()
        Me.btnCPRotate180 = New DevComponents.DotNetBar.ButtonItem()
        Me.btnCPRotate90Right = New DevComponents.DotNetBar.ButtonItem()
        Me.btnCPRotate90Left = New DevComponents.DotNetBar.ButtonItem()
        Me.btnCPInvertColors = New DevComponents.DotNetBar.ButtonItem()
        Me.btnCPSaveImage = New DevComponents.DotNetBar.ButtonItem()
        Me.btnCPReset = New DevComponents.DotNetBar.ButtonItem()
        Me.picCPSlip = New iViewCore.PictureBox()
        Me.DAContextMenuBar = New DevComponents.DotNetBar.ContextMenuBar()
        Me.DAButtonItem1 = New DevComponents.DotNetBar.ButtonItem()
        Me.ButtonItem5 = New DevComponents.DotNetBar.ButtonItem()
        Me.btnDAActualSizeContext = New DevComponents.DotNetBar.ButtonItem()
        Me.btnDAFitSizeContext = New DevComponents.DotNetBar.ButtonItem()
        Me.btnDAFitWidthContext = New DevComponents.DotNetBar.ButtonItem()
        Me.btnDAFitHeightContext = New DevComponents.DotNetBar.ButtonItem()
        Me.ButtonItem1 = New DevComponents.DotNetBar.ButtonItem()
        Me.btnDAFlipVertically = New DevComponents.DotNetBar.ButtonItem()
        Me.btnDAFlipHorizontally = New DevComponents.DotNetBar.ButtonItem()
        Me.ButtonItem2 = New DevComponents.DotNetBar.ButtonItem()
        Me.btnDARotate180 = New DevComponents.DotNetBar.ButtonItem()
        Me.btnDARotate90Right = New DevComponents.DotNetBar.ButtonItem()
        Me.btnDARotate90Left = New DevComponents.DotNetBar.ButtonItem()
        Me.btnDAInvertColors = New DevComponents.DotNetBar.ButtonItem()
        Me.btnDASaveImage = New DevComponents.DotNetBar.ButtonItem()
        Me.btnPrintContext = New DevComponents.DotNetBar.ButtonItem()
        Me.btnDAReset = New DevComponents.DotNetBar.ButtonItem()
        Me.picDASlip = New iViewCore.PictureBox()
        Me.Bar = New DevComponents.DotNetBar.Bar()
        Me.btnPrintImage = New DevComponents.DotNetBar.ButtonItem()
        Me.btnPrevious = New DevComponents.DotNetBar.ButtonItem()
        Me.btnNext = New DevComponents.DotNetBar.ButtonItem()
        Me.btnZoomInDASlip = New DevComponents.DotNetBar.ButtonItem()
        Me.btnZoomOutDASlip = New DevComponents.DotNetBar.ButtonItem()
        Me.btnDAViewMode = New DevComponents.DotNetBar.ButtonItem()
        Me.btnDAFullSize = New DevComponents.DotNetBar.ButtonItem()
        Me.btnDAFitImage = New DevComponents.DotNetBar.ButtonItem()
        Me.btnDAFitWidth = New DevComponents.DotNetBar.ButtonItem()
        Me.btnDAFitHeight = New DevComponents.DotNetBar.ButtonItem()
        Me.sldrZoomDASlip = New DevComponents.DotNetBar.SliderItem()
        Me.PanelEx1 = New DevComponents.DotNetBar.PanelEx()
        Me.lblSlNumber = New DevComponents.DotNetBar.LabelItem()
        Me.Bar2 = New DevComponents.DotNetBar.Bar()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.Bar1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CPContextMenuBar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DAContextMenuBar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Bar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelEx1.SuspendLayout()
        CType(Me.Bar2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Bar1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.CPContextMenuBar)
        Me.SplitContainer1.Panel1.Controls.Add(Me.picCPSlip)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.DAContextMenuBar)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Bar)
        Me.SplitContainer1.Panel2.Controls.Add(Me.picDASlip)
        Me.SplitContainer1.Size = New System.Drawing.Size(1196, 627)
        Me.SplitContainer1.SplitterDistance = 150
        Me.SplitContainer1.TabIndex = 33
        '
        'Bar1
        '
        Me.Bar1.AccessibleDescription = "DotNetBar Bar (Bar1)"
        Me.Bar1.AccessibleName = "DotNetBar Bar"
        Me.Bar1.AccessibleRole = System.Windows.Forms.AccessibleRole.StatusBar
        Me.Bar1.BarType = DevComponents.DotNetBar.eBarType.StatusBar
        Me.Bar1.CanUndock = False
        Me.Bar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Bar1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Bar1.GrabHandleStyle = DevComponents.DotNetBar.eGrabHandleStyle.ResizeHandle
        Me.Bar1.IsMaximized = False
        Me.Bar1.Items.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnSelectCPSlip, Me.btnZoomInCPSlip, Me.btnZoomOutCPSlip, Me.btnCPViewMode, Me.sldrZoomCPSlip})
        Me.Bar1.Location = New System.Drawing.Point(0, 563)
        Me.Bar1.Name = "Bar1"
        Me.Bar1.Size = New System.Drawing.Size(146, 60)
        Me.Bar1.Stretch = True
        Me.Bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.Bar1.TabIndex = 35
        Me.Bar1.TabStop = False
        '
        'btnSelectCPSlip
        '
        Me.btnSelectCPSlip.AutoCollapseOnClick = False
        Me.btnSelectCPSlip.BeginGroup = True
        Me.btnSelectCPSlip.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnSelectCPSlip.Image = CType(resources.GetObject("btnSelectCPSlip.Image"), System.Drawing.Image)
        Me.btnSelectCPSlip.Name = "btnSelectCPSlip"
        Me.btnSelectCPSlip.Text = "Select"
        '
        'btnZoomInCPSlip
        '
        Me.btnZoomInCPSlip.AutoCollapseOnClick = False
        Me.btnZoomInCPSlip.BeginGroup = True
        Me.btnZoomInCPSlip.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnZoomInCPSlip.Image = CType(resources.GetObject("btnZoomInCPSlip.Image"), System.Drawing.Image)
        Me.btnZoomInCPSlip.Name = "btnZoomInCPSlip"
        '
        'btnZoomOutCPSlip
        '
        Me.btnZoomOutCPSlip.AutoCollapseOnClick = False
        Me.btnZoomOutCPSlip.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnZoomOutCPSlip.Image = CType(resources.GetObject("btnZoomOutCPSlip.Image"), System.Drawing.Image)
        Me.btnZoomOutCPSlip.Name = "btnZoomOutCPSlip"
        '
        'btnCPViewMode
        '
        Me.btnCPViewMode.AutoCollapseOnClick = False
        Me.btnCPViewMode.AutoExpandOnClick = True
        Me.btnCPViewMode.BeginGroup = True
        Me.btnCPViewMode.Image = CType(resources.GetObject("btnCPViewMode.Image"), System.Drawing.Image)
        Me.btnCPViewMode.Name = "btnCPViewMode"
        Me.btnCPViewMode.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnCPFullSize, Me.btnCPFitImage, Me.btnCPFitWidth, Me.btnCPFitHeight})
        Me.btnCPViewMode.Text = "Fit"
        '
        'btnCPFullSize
        '
        Me.btnCPFullSize.Name = "btnCPFullSize"
        Me.btnCPFullSize.Text = "Actual Size"
        '
        'btnCPFitImage
        '
        Me.btnCPFitImage.Name = "btnCPFitImage"
        Me.btnCPFitImage.Text = "Fit Image"
        '
        'btnCPFitWidth
        '
        Me.btnCPFitWidth.Name = "btnCPFitWidth"
        Me.btnCPFitWidth.Text = "Fit Width"
        '
        'btnCPFitHeight
        '
        Me.btnCPFitHeight.Name = "btnCPFitHeight"
        Me.btnCPFitHeight.Text = "Fit Height"
        '
        'sldrZoomCPSlip
        '
        Me.sldrZoomCPSlip.AutoCollapseOnClick = False
        Me.sldrZoomCPSlip.BeginGroup = True
        Me.sldrZoomCPSlip.Maximum = 500
        Me.sldrZoomCPSlip.Minimum = 25
        Me.sldrZoomCPSlip.Name = "sldrZoomCPSlip"
        Me.sldrZoomCPSlip.Step = 5
        Me.sldrZoomCPSlip.Text = "Zoom"
        Me.sldrZoomCPSlip.TrackMarker = False
        Me.sldrZoomCPSlip.Value = 25
        Me.sldrZoomCPSlip.Width = 200
        '
        'CPContextMenuBar
        '
        Me.CPContextMenuBar.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CPContextMenuBar.IsMaximized = False
        Me.CPContextMenuBar.Items.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.CPButtonItem})
        Me.CPContextMenuBar.Location = New System.Drawing.Point(21, 295)
        Me.CPContextMenuBar.Name = "CPContextMenuBar"
        Me.CPContextMenuBar.Size = New System.Drawing.Size(125, 27)
        Me.CPContextMenuBar.Stretch = True
        Me.CPContextMenuBar.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.CPContextMenuBar.TabIndex = 39
        Me.CPContextMenuBar.TabStop = False
        Me.CPContextMenuBar.Text = "ContextMenuBar1"
        '
        'CPButtonItem
        '
        Me.CPButtonItem.AutoExpandOnClick = True
        Me.CPButtonItem.Name = "CPButtonItem"
        Me.CPButtonItem.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.ButtonItem4, Me.ButtonItem3, Me.ButtonItem6, Me.btnCPInvertColors, Me.btnCPSaveImage, Me.btnCPReset})
        Me.CPButtonItem.Text = "ButtonItem1"
        '
        'ButtonItem4
        '
        Me.ButtonItem4.Name = "ButtonItem4"
        Me.ButtonItem4.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnCPActualSizeContext, Me.btnCPFitImageContext, Me.btnCPFitWidthContext, Me.btnCPFitHeightContext})
        Me.ButtonItem4.Text = "Image Size"
        '
        'btnCPActualSizeContext
        '
        Me.btnCPActualSizeContext.Name = "btnCPActualSizeContext"
        Me.btnCPActualSizeContext.Text = "Actual Size"
        '
        'btnCPFitImageContext
        '
        Me.btnCPFitImageContext.Name = "btnCPFitImageContext"
        Me.btnCPFitImageContext.Text = "Fit Image"
        '
        'btnCPFitWidthContext
        '
        Me.btnCPFitWidthContext.Name = "btnCPFitWidthContext"
        Me.btnCPFitWidthContext.Text = "Fit Width"
        '
        'btnCPFitHeightContext
        '
        Me.btnCPFitHeightContext.Name = "btnCPFitHeightContext"
        Me.btnCPFitHeightContext.Text = "Fit Height"
        '
        'ButtonItem3
        '
        Me.ButtonItem3.BeginGroup = True
        Me.ButtonItem3.Name = "ButtonItem3"
        Me.ButtonItem3.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnCPFlipVertically, Me.btnCPFlipHorizontally})
        Me.ButtonItem3.Text = "Flip"
        '
        'btnCPFlipVertically
        '
        Me.btnCPFlipVertically.Name = "btnCPFlipVertically"
        Me.btnCPFlipVertically.Text = "Vertically"
        '
        'btnCPFlipHorizontally
        '
        Me.btnCPFlipHorizontally.Name = "btnCPFlipHorizontally"
        Me.btnCPFlipHorizontally.Text = "Horizontally"
        '
        'ButtonItem6
        '
        Me.ButtonItem6.BeginGroup = True
        Me.ButtonItem6.Name = "ButtonItem6"
        Me.ButtonItem6.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnCPRotate180, Me.btnCPRotate90Right, Me.btnCPRotate90Left})
        Me.ButtonItem6.Text = "Rotate"
        '
        'btnCPRotate180
        '
        Me.btnCPRotate180.Name = "btnCPRotate180"
        Me.btnCPRotate180.Text = "180°"
        '
        'btnCPRotate90Right
        '
        Me.btnCPRotate90Right.Name = "btnCPRotate90Right"
        Me.btnCPRotate90Right.Text = "90° Clockwise"
        '
        'btnCPRotate90Left
        '
        Me.btnCPRotate90Left.Name = "btnCPRotate90Left"
        Me.btnCPRotate90Left.Text = "90° Anti Clockwise"
        '
        'btnCPInvertColors
        '
        Me.btnCPInvertColors.BeginGroup = True
        Me.btnCPInvertColors.Name = "btnCPInvertColors"
        Me.btnCPInvertColors.Text = "Invert Color"
        '
        'btnCPSaveImage
        '
        Me.btnCPSaveImage.BeginGroup = True
        Me.btnCPSaveImage.Name = "btnCPSaveImage"
        Me.btnCPSaveImage.Text = "Save Image As"
        '
        'btnCPReset
        '
        Me.btnCPReset.BeginGroup = True
        Me.btnCPReset.Name = "btnCPReset"
        Me.btnCPReset.Text = "Reset"
        '
        'picCPSlip
        '
        Me.picCPSlip.AutoScroll = True
        Me.picCPSlip.BackColor = System.Drawing.Color.Gray
        Me.picCPSlip.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.CPContextMenuBar.SetContextMenuEx(Me.picCPSlip, Me.CPButtonItem)
        Me.picCPSlip.Cursor = System.Windows.Forms.Cursors.SizeAll
        Me.picCPSlip.Dock = System.Windows.Forms.DockStyle.Fill
        Me.picCPSlip.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.picCPSlip.Image = Nothing
        Me.picCPSlip.Location = New System.Drawing.Point(0, 0)
        Me.picCPSlip.Name = "picCPSlip"
        Me.picCPSlip.Size = New System.Drawing.Size(146, 623)
        Me.picCPSlip.TabIndex = 34
        Me.picCPSlip.ViewMode = iViewCore.PictureBox.EViewMode.FitImage
        '
        'DAContextMenuBar
        '
        Me.DAContextMenuBar.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DAContextMenuBar.IsMaximized = False
        Me.DAContextMenuBar.Items.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.DAButtonItem1})
        Me.DAContextMenuBar.Location = New System.Drawing.Point(576, 295)
        Me.DAContextMenuBar.Name = "DAContextMenuBar"
        Me.DAContextMenuBar.Size = New System.Drawing.Size(125, 27)
        Me.DAContextMenuBar.Stretch = True
        Me.DAContextMenuBar.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.DAContextMenuBar.TabIndex = 38
        Me.DAContextMenuBar.TabStop = False
        Me.DAContextMenuBar.Text = "ContextMenuBar1"
        '
        'DAButtonItem1
        '
        Me.DAButtonItem1.AutoExpandOnClick = True
        Me.DAButtonItem1.Name = "DAButtonItem1"
        Me.DAButtonItem1.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.ButtonItem5, Me.ButtonItem1, Me.ButtonItem2, Me.btnDAInvertColors, Me.btnDASaveImage, Me.btnPrintContext, Me.btnDAReset})
        Me.DAButtonItem1.Text = "ButtonItem1"
        '
        'ButtonItem5
        '
        Me.ButtonItem5.Name = "ButtonItem5"
        Me.ButtonItem5.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnDAActualSizeContext, Me.btnDAFitSizeContext, Me.btnDAFitWidthContext, Me.btnDAFitHeightContext})
        Me.ButtonItem5.Text = "Image Size"
        '
        'btnDAActualSizeContext
        '
        Me.btnDAActualSizeContext.Name = "btnDAActualSizeContext"
        Me.btnDAActualSizeContext.Text = "Actual Size"
        '
        'btnDAFitSizeContext
        '
        Me.btnDAFitSizeContext.Name = "btnDAFitSizeContext"
        Me.btnDAFitSizeContext.Text = "Fit Image"
        '
        'btnDAFitWidthContext
        '
        Me.btnDAFitWidthContext.Name = "btnDAFitWidthContext"
        Me.btnDAFitWidthContext.Text = "Fit Width"
        '
        'btnDAFitHeightContext
        '
        Me.btnDAFitHeightContext.Name = "btnDAFitHeightContext"
        Me.btnDAFitHeightContext.Text = "Fit Height"
        '
        'ButtonItem1
        '
        Me.ButtonItem1.BeginGroup = True
        Me.ButtonItem1.Name = "ButtonItem1"
        Me.ButtonItem1.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnDAFlipVertically, Me.btnDAFlipHorizontally})
        Me.ButtonItem1.Text = "Flip"
        '
        'btnDAFlipVertically
        '
        Me.btnDAFlipVertically.Name = "btnDAFlipVertically"
        Me.btnDAFlipVertically.Text = "Vertically"
        '
        'btnDAFlipHorizontally
        '
        Me.btnDAFlipHorizontally.Name = "btnDAFlipHorizontally"
        Me.btnDAFlipHorizontally.Text = "Horizontally"
        '
        'ButtonItem2
        '
        Me.ButtonItem2.BeginGroup = True
        Me.ButtonItem2.Name = "ButtonItem2"
        Me.ButtonItem2.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnDARotate180, Me.btnDARotate90Right, Me.btnDARotate90Left})
        Me.ButtonItem2.Text = "Rotate"
        '
        'btnDARotate180
        '
        Me.btnDARotate180.Name = "btnDARotate180"
        Me.btnDARotate180.Text = "180°"
        '
        'btnDARotate90Right
        '
        Me.btnDARotate90Right.Name = "btnDARotate90Right"
        Me.btnDARotate90Right.Text = "90° Clockwise"
        '
        'btnDARotate90Left
        '
        Me.btnDARotate90Left.Name = "btnDARotate90Left"
        Me.btnDARotate90Left.Text = "90° Anti Clockwise"
        '
        'btnDAInvertColors
        '
        Me.btnDAInvertColors.BeginGroup = True
        Me.btnDAInvertColors.Name = "btnDAInvertColors"
        Me.btnDAInvertColors.Text = "Invert Color"
        '
        'btnDASaveImage
        '
        Me.btnDASaveImage.BeginGroup = True
        Me.btnDASaveImage.Name = "btnDASaveImage"
        Me.btnDASaveImage.Text = "Save Image As"
        '
        'btnPrintContext
        '
        Me.btnPrintContext.BeginGroup = True
        Me.btnPrintContext.Name = "btnPrintContext"
        Me.btnPrintContext.Text = "Print"
        '
        'btnDAReset
        '
        Me.btnDAReset.BeginGroup = True
        Me.btnDAReset.Name = "btnDAReset"
        Me.btnDAReset.Text = "Reset"
        '
        'picDASlip
        '
        Me.picDASlip.AutoScroll = True
        Me.picDASlip.BackColor = System.Drawing.Color.Gray
        Me.picDASlip.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.DAContextMenuBar.SetContextMenuEx(Me.picDASlip, Me.DAButtonItem1)
        Me.picDASlip.Cursor = System.Windows.Forms.Cursors.SizeAll
        Me.picDASlip.Dock = System.Windows.Forms.DockStyle.Fill
        Me.picDASlip.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.picDASlip.Image = Nothing
        Me.picDASlip.Location = New System.Drawing.Point(0, 0)
        Me.picDASlip.Name = "picDASlip"
        Me.picDASlip.Size = New System.Drawing.Size(1038, 623)
        Me.picDASlip.TabIndex = 31
        Me.picDASlip.ViewMode = iViewCore.PictureBox.EViewMode.FitImage
        '
        'Bar
        '
        Me.Bar.AccessibleDescription = "DotNetBar Bar (Bar)"
        Me.Bar.AccessibleName = "DotNetBar Bar"
        Me.Bar.AccessibleRole = System.Windows.Forms.AccessibleRole.StatusBar
        Me.Bar.BarType = DevComponents.DotNetBar.eBarType.StatusBar
        Me.Bar.CanUndock = False
        Me.Bar.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Bar.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Bar.GrabHandleStyle = DevComponents.DotNetBar.eGrabHandleStyle.ResizeHandle
        Me.Bar.IsMaximized = False
        Me.Bar.Items.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnPrintImage, Me.btnPrevious, Me.btnNext, Me.btnZoomInDASlip, Me.btnZoomOutDASlip, Me.btnDAViewMode, Me.sldrZoomDASlip})
        Me.Bar.Location = New System.Drawing.Point(0, 563)
        Me.Bar.Name = "Bar"
        Me.Bar.Size = New System.Drawing.Size(1038, 60)
        Me.Bar.Stretch = True
        Me.Bar.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.Bar.TabIndex = 32
        Me.Bar.TabStop = False
        '
        'btnPrintImage
        '
        Me.btnPrintImage.AutoCollapseOnClick = False
        Me.btnPrintImage.BeginGroup = True
        Me.btnPrintImage.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnPrintImage.Image = CType(resources.GetObject("btnPrintImage.Image"), System.Drawing.Image)
        Me.btnPrintImage.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far
        Me.btnPrintImage.Name = "btnPrintImage"
        Me.btnPrintImage.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlP)
        Me.btnPrintImage.Text = "Print"
        '
        'btnPrevious
        '
        Me.btnPrevious.AutoCollapseOnClick = False
        Me.btnPrevious.BeginGroup = True
        Me.btnPrevious.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnPrevious.Image = CType(resources.GetObject("btnPrevious.Image"), System.Drawing.Image)
        Me.btnPrevious.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far
        Me.btnPrevious.Name = "btnPrevious"
        Me.btnPrevious.Text = "Previous"
        '
        'btnNext
        '
        Me.btnNext.AutoCollapseOnClick = False
        Me.btnNext.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnNext.Image = CType(resources.GetObject("btnNext.Image"), System.Drawing.Image)
        Me.btnNext.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Text = "Next"
        '
        'btnZoomInDASlip
        '
        Me.btnZoomInDASlip.AutoCollapseOnClick = False
        Me.btnZoomInDASlip.BeginGroup = True
        Me.btnZoomInDASlip.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnZoomInDASlip.Image = CType(resources.GetObject("btnZoomInDASlip.Image"), System.Drawing.Image)
        Me.btnZoomInDASlip.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far
        Me.btnZoomInDASlip.Name = "btnZoomInDASlip"
        '
        'btnZoomOutDASlip
        '
        Me.btnZoomOutDASlip.AutoCollapseOnClick = False
        Me.btnZoomOutDASlip.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnZoomOutDASlip.Image = CType(resources.GetObject("btnZoomOutDASlip.Image"), System.Drawing.Image)
        Me.btnZoomOutDASlip.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far
        Me.btnZoomOutDASlip.Name = "btnZoomOutDASlip"
        '
        'btnDAViewMode
        '
        Me.btnDAViewMode.AutoCollapseOnClick = False
        Me.btnDAViewMode.AutoExpandOnClick = True
        Me.btnDAViewMode.BeginGroup = True
        Me.btnDAViewMode.Image = CType(resources.GetObject("btnDAViewMode.Image"), System.Drawing.Image)
        Me.btnDAViewMode.Name = "btnDAViewMode"
        Me.btnDAViewMode.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnDAFullSize, Me.btnDAFitImage, Me.btnDAFitWidth, Me.btnDAFitHeight})
        Me.btnDAViewMode.Text = "Fit"
        '
        'btnDAFullSize
        '
        Me.btnDAFullSize.Name = "btnDAFullSize"
        Me.btnDAFullSize.Text = "Actual Size"
        '
        'btnDAFitImage
        '
        Me.btnDAFitImage.Name = "btnDAFitImage"
        Me.btnDAFitImage.Text = "Fit Image"
        '
        'btnDAFitWidth
        '
        Me.btnDAFitWidth.Name = "btnDAFitWidth"
        Me.btnDAFitWidth.Text = "Fit Width"
        '
        'btnDAFitHeight
        '
        Me.btnDAFitHeight.Name = "btnDAFitHeight"
        Me.btnDAFitHeight.Text = "Fit Height"
        '
        'sldrZoomDASlip
        '
        Me.sldrZoomDASlip.AutoCollapseOnClick = False
        Me.sldrZoomDASlip.BeginGroup = True
        Me.sldrZoomDASlip.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far
        Me.sldrZoomDASlip.Maximum = 500
        Me.sldrZoomDASlip.Minimum = 25
        Me.sldrZoomDASlip.Name = "sldrZoomDASlip"
        Me.sldrZoomDASlip.Step = 5
        Me.sldrZoomDASlip.Text = "Zoom"
        Me.sldrZoomDASlip.TrackMarker = False
        Me.sldrZoomDASlip.Value = 25
        Me.sldrZoomDASlip.Width = 200
        '
        'PanelEx1
        '
        Me.PanelEx1.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.PanelEx1.Controls.Add(Me.SplitContainer1)
        Me.PanelEx1.DisabledBackColor = System.Drawing.Color.Empty
        Me.PanelEx1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelEx1.Location = New System.Drawing.Point(0, 0)
        Me.PanelEx1.Name = "PanelEx1"
        Me.PanelEx1.Size = New System.Drawing.Size(1196, 627)
        Me.PanelEx1.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx1.Style.GradientAngle = 90
        Me.PanelEx1.TabIndex = 37
        Me.PanelEx1.Text = "PanelEx1"
        '
        'lblSlNumber
        '
        Me.lblSlNumber.BorderType = DevComponents.DotNetBar.eBorderType.DoubleLine
        Me.lblSlNumber.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSlNumber.Name = "lblSlNumber"
        Me.lblSlNumber.Stretch = True
        Me.lblSlNumber.Width = 150
        '
        'Bar2
        '
        Me.Bar2.AccessibleDescription = "DotNetBar Bar (Bar2)"
        Me.Bar2.AccessibleName = "DotNetBar Bar"
        Me.Bar2.AccessibleRole = System.Windows.Forms.AccessibleRole.StatusBar
        Me.Bar2.BarType = DevComponents.DotNetBar.eBarType.StatusBar
        Me.Bar2.CanUndock = False
        Me.Bar2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Bar2.DockedBorderStyle = DevComponents.DotNetBar.eBorderType.DoubleLine
        Me.Bar2.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Bar2.GrabHandleStyle = DevComponents.DotNetBar.eGrabHandleStyle.ResizeHandle
        Me.Bar2.IsMaximized = False
        Me.Bar2.Items.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.lblSlNumber})
        Me.Bar2.Location = New System.Drawing.Point(0, 627)
        Me.Bar2.Name = "Bar2"
        Me.Bar2.Size = New System.Drawing.Size(1196, 32)
        Me.Bar2.Stretch = True
        Me.Bar2.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.Bar2.TabIndex = 36
        Me.Bar2.TabStop = False
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'frmIDSlipImageDisplayer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1196, 659)
        Me.Controls.Add(Me.PanelEx1)
        Me.Controls.Add(Me.Bar2)
        Me.DoubleBuffered = True
        Me.EnableGlass = False
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "frmIDSlipImageDisplayer"
        Me.Text = "Identified Slip Image"
        Me.TitleText = "<b>Identified Slip Image</b>"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.Bar1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CPContextMenuBar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DAContextMenuBar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Bar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelEx1.ResumeLayout(False)
        CType(Me.Bar2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents Bar1 As DevComponents.DotNetBar.Bar
    Friend WithEvents btnSelectCPSlip As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnZoomInCPSlip As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnZoomOutCPSlip As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents sldrZoomCPSlip As DevComponents.DotNetBar.SliderItem
    Friend WithEvents picCPSlip As iViewCore.PictureBox
    Friend WithEvents Bar As DevComponents.DotNetBar.Bar
    Friend WithEvents btnPrintImage As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnPrevious As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnNext As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnZoomInDASlip As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnZoomOutDASlip As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents sldrZoomDASlip As DevComponents.DotNetBar.SliderItem
    Friend WithEvents picDASlip As iViewCore.PictureBox
    Friend WithEvents PanelEx1 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents lblSlNumber As DevComponents.DotNetBar.LabelItem
    Friend WithEvents Bar2 As DevComponents.DotNetBar.Bar
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents btnCPViewMode As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnCPFullSize As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnCPFitImage As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnCPFitWidth As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnCPFitHeight As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnDAViewMode As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnDAFullSize As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnDAFitImage As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnDAFitWidth As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnDAFitHeight As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents CPContextMenuBar As DevComponents.DotNetBar.ContextMenuBar
    Friend WithEvents CPButtonItem As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ButtonItem4 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnCPActualSizeContext As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnCPFitImageContext As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnCPFitWidthContext As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnCPFitHeightContext As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ButtonItem3 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnCPFlipVertically As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnCPFlipHorizontally As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ButtonItem6 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnCPRotate180 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnCPRotate90Right As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnCPRotate90Left As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnCPInvertColors As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnCPSaveImage As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnCPReset As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents DAContextMenuBar As DevComponents.DotNetBar.ContextMenuBar
    Friend WithEvents DAButtonItem1 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ButtonItem5 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnDAActualSizeContext As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnDAFitSizeContext As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnDAFitWidthContext As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnDAFitHeightContext As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ButtonItem1 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnDAFlipVertically As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnDAFlipHorizontally As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ButtonItem2 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnDARotate180 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnDARotate90Right As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnDARotate90Left As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnDAInvertColors As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnDASaveImage As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnDAReset As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents btnPrintContext As DevComponents.DotNetBar.ButtonItem
End Class
