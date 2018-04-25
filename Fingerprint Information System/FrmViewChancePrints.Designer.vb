<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmViewChancePrints
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmViewChancePrints))
        Me.Bar2 = New DevComponents.DotNetBar.Bar()
        Me.lblSOCNumber = New DevComponents.DotNetBar.LabelItem()
        Me.lblFileName = New DevComponents.DotNetBar.LabelItem()
        Me.prgBar = New DevComponents.DotNetBar.ProgressBarItem()
        Me.PanelEx1 = New DevComponents.DotNetBar.PanelEx()
        Me.CPContextMenuBar = New DevComponents.DotNetBar.ContextMenuBar()
        Me.DAButtonItem1 = New DevComponents.DotNetBar.ButtonItem()
        Me.ButtonItem5 = New DevComponents.DotNetBar.ButtonItem()
        Me.btnActualSizeContext = New DevComponents.DotNetBar.ButtonItem()
        Me.btnFitImageContext = New DevComponents.DotNetBar.ButtonItem()
        Me.btnFitWidthContext = New DevComponents.DotNetBar.ButtonItem()
        Me.btnFitHeightContext = New DevComponents.DotNetBar.ButtonItem()
        Me.ButtonItem1 = New DevComponents.DotNetBar.ButtonItem()
        Me.btnFlipVertically = New DevComponents.DotNetBar.ButtonItem()
        Me.btnFlipHorizontally = New DevComponents.DotNetBar.ButtonItem()
        Me.ButtonItem2 = New DevComponents.DotNetBar.ButtonItem()
        Me.btnRotate180 = New DevComponents.DotNetBar.ButtonItem()
        Me.btnRotate90Right = New DevComponents.DotNetBar.ButtonItem()
        Me.btnRotate90Left = New DevComponents.DotNetBar.ButtonItem()
        Me.btnInvertColors = New DevComponents.DotNetBar.ButtonItem()
        Me.btnSaveImage = New DevComponents.DotNetBar.ButtonItem()
        Me.btnPrintPicContext = New DevComponents.DotNetBar.ButtonItem()
        Me.btnReset = New DevComponents.DotNetBar.ButtonItem()
        Me.picCP = New iViewCore.PictureBox()
        Me.Bar = New DevComponents.DotNetBar.Bar()
        Me.btnPrintImage = New DevComponents.DotNetBar.ButtonItem()
        Me.btnZoomIn = New DevComponents.DotNetBar.ButtonItem()
        Me.btnZoomOut = New DevComponents.DotNetBar.ButtonItem()
        Me.btnViewMode = New DevComponents.DotNetBar.ButtonItem()
        Me.btnActualSize = New DevComponents.DotNetBar.ButtonItem()
        Me.btnFitImage = New DevComponents.DotNetBar.ButtonItem()
        Me.btnFitWidth = New DevComponents.DotNetBar.ButtonItem()
        Me.btnFitHeight = New DevComponents.DotNetBar.ButtonItem()
        Me.sldrZoom = New DevComponents.DotNetBar.SliderItem()
        Me.PanelEx2 = New DevComponents.DotNetBar.PanelEx()
        Me.ListContextMenuBar = New DevComponents.DotNetBar.ContextMenuBar()
        Me.ButtonItem3 = New DevComponents.DotNetBar.ButtonItem()
        Me.btnPrintContext = New DevComponents.DotNetBar.ButtonItem()
        Me.btnLocate = New DevComponents.DotNetBar.ButtonItem()
        Me.picList = New iViewCore.PictureList()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.ButtonItem6 = New DevComponents.DotNetBar.ButtonItem()
        Me.ButtonItem7 = New DevComponents.DotNetBar.ButtonItem()
        Me.ButtonItem8 = New DevComponents.DotNetBar.ButtonItem()
        Me.ButtonItem9 = New DevComponents.DotNetBar.ButtonItem()
        Me.ButtonItem11 = New DevComponents.DotNetBar.ButtonItem()
        Me.ButtonItem12 = New DevComponents.DotNetBar.ButtonItem()
        Me.ButtonItem14 = New DevComponents.DotNetBar.ButtonItem()
        Me.ButtonItem15 = New DevComponents.DotNetBar.ButtonItem()
        Me.ButtonItem16 = New DevComponents.DotNetBar.ButtonItem()
        CType(Me.Bar2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelEx1.SuspendLayout()
        CType(Me.CPContextMenuBar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Bar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelEx2.SuspendLayout()
        CType(Me.ListContextMenuBar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.Bar2.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Bar2.GrabHandleStyle = DevComponents.DotNetBar.eGrabHandleStyle.ResizeHandle
        Me.Bar2.IsMaximized = False
        Me.Bar2.Items.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.lblSOCNumber, Me.lblFileName, Me.prgBar})
        Me.Bar2.ItemSpacing = 10
        Me.Bar2.Location = New System.Drawing.Point(0, 460)
        Me.Bar2.Name = "Bar2"
        Me.Bar2.Size = New System.Drawing.Size(1109, 34)
        Me.Bar2.Stretch = True
        Me.Bar2.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.Bar2.TabIndex = 37
        Me.Bar2.TabStop = False
        '
        'lblSOCNumber
        '
        Me.lblSOCNumber.BorderType = DevComponents.DotNetBar.eBorderType.DoubleLine
        Me.lblSOCNumber.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSOCNumber.Name = "lblSOCNumber"
        Me.lblSOCNumber.Width = 300
        '
        'lblFileName
        '
        Me.lblFileName.BorderType = DevComponents.DotNetBar.eBorderType.DoubleLine
        Me.lblFileName.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFileName.Name = "lblFileName"
        Me.lblFileName.Width = 500
        '
        'prgBar
        '
        '
        '
        '
        Me.prgBar.BackStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.prgBar.ChunkGradientAngle = 0.0!
        Me.prgBar.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far
        Me.prgBar.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways
        Me.prgBar.Name = "prgBar"
        Me.prgBar.RecentlyUsed = False
        Me.prgBar.TextVisible = True
        Me.prgBar.Width = 250
        '
        'PanelEx1
        '
        Me.PanelEx1.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.PanelEx1.Controls.Add(Me.CPContextMenuBar)
        Me.PanelEx1.Controls.Add(Me.picCP)
        Me.PanelEx1.Controls.Add(Me.Bar)
        Me.PanelEx1.Controls.Add(Me.PanelEx2)
        Me.PanelEx1.DisabledBackColor = System.Drawing.Color.Empty
        Me.PanelEx1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelEx1.Location = New System.Drawing.Point(0, 0)
        Me.PanelEx1.Name = "PanelEx1"
        Me.PanelEx1.Size = New System.Drawing.Size(1109, 460)
        Me.PanelEx1.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx1.Style.GradientAngle = 90
        Me.PanelEx1.TabIndex = 38
        '
        'CPContextMenuBar
        '
        Me.CPContextMenuBar.DockSide = DevComponents.DotNetBar.eDockSide.Document
        Me.CPContextMenuBar.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CPContextMenuBar.IsMaximized = False
        Me.CPContextMenuBar.Items.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.DAButtonItem1})
        Me.CPContextMenuBar.Location = New System.Drawing.Point(492, 219)
        Me.CPContextMenuBar.Name = "CPContextMenuBar"
        Me.CPContextMenuBar.Size = New System.Drawing.Size(125, 27)
        Me.CPContextMenuBar.Stretch = True
        Me.CPContextMenuBar.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.CPContextMenuBar.TabIndex = 45
        Me.CPContextMenuBar.TabStop = False
        Me.CPContextMenuBar.Text = "Pic Context"
        '
        'DAButtonItem1
        '
        Me.DAButtonItem1.AutoExpandOnClick = True
        Me.DAButtonItem1.Name = "DAButtonItem1"
        Me.DAButtonItem1.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.ButtonItem5, Me.ButtonItem1, Me.ButtonItem2, Me.btnInvertColors, Me.btnSaveImage, Me.btnPrintPicContext, Me.btnReset})
        Me.DAButtonItem1.Text = "ButtonItem1"
        '
        'ButtonItem5
        '
        Me.ButtonItem5.Name = "ButtonItem5"
        Me.ButtonItem5.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnActualSizeContext, Me.btnFitImageContext, Me.btnFitWidthContext, Me.btnFitHeightContext})
        Me.ButtonItem5.Text = "Image Size"
        '
        'btnActualSizeContext
        '
        Me.btnActualSizeContext.Name = "btnActualSizeContext"
        Me.btnActualSizeContext.Text = "Actual Size"
        '
        'btnFitImageContext
        '
        Me.btnFitImageContext.Name = "btnFitImageContext"
        Me.btnFitImageContext.Text = "Fit Image"
        '
        'btnFitWidthContext
        '
        Me.btnFitWidthContext.Name = "btnFitWidthContext"
        Me.btnFitWidthContext.Text = "Fit Width"
        '
        'btnFitHeightContext
        '
        Me.btnFitHeightContext.Name = "btnFitHeightContext"
        Me.btnFitHeightContext.Text = "Fit Height"
        '
        'ButtonItem1
        '
        Me.ButtonItem1.BeginGroup = True
        Me.ButtonItem1.Name = "ButtonItem1"
        Me.ButtonItem1.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnFlipVertically, Me.btnFlipHorizontally})
        Me.ButtonItem1.Text = "Flip"
        '
        'btnFlipVertically
        '
        Me.btnFlipVertically.Name = "btnFlipVertically"
        Me.btnFlipVertically.Text = "Vertically"
        '
        'btnFlipHorizontally
        '
        Me.btnFlipHorizontally.Name = "btnFlipHorizontally"
        Me.btnFlipHorizontally.Text = "Horizontally"
        '
        'ButtonItem2
        '
        Me.ButtonItem2.BeginGroup = True
        Me.ButtonItem2.Name = "ButtonItem2"
        Me.ButtonItem2.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnRotate180, Me.btnRotate90Right, Me.btnRotate90Left})
        Me.ButtonItem2.Text = "Rotate"
        '
        'btnRotate180
        '
        Me.btnRotate180.Name = "btnRotate180"
        Me.btnRotate180.Text = "180°"
        '
        'btnRotate90Right
        '
        Me.btnRotate90Right.Name = "btnRotate90Right"
        Me.btnRotate90Right.Text = "90° Clockwise"
        '
        'btnRotate90Left
        '
        Me.btnRotate90Left.Name = "btnRotate90Left"
        Me.btnRotate90Left.Text = "90° Anti Clockwise"
        '
        'btnInvertColors
        '
        Me.btnInvertColors.BeginGroup = True
        Me.btnInvertColors.Name = "btnInvertColors"
        Me.btnInvertColors.Text = "Invert Color"
        '
        'btnSaveImage
        '
        Me.btnSaveImage.BeginGroup = True
        Me.btnSaveImage.Name = "btnSaveImage"
        Me.btnSaveImage.Text = "Save Image As"
        '
        'btnPrintPicContext
        '
        Me.btnPrintPicContext.BeginGroup = True
        Me.btnPrintPicContext.Name = "btnPrintPicContext"
        Me.btnPrintPicContext.Text = "Print"
        '
        'btnReset
        '
        Me.btnReset.BeginGroup = True
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Text = "Reset"
        '
        'picCP
        '
        Me.picCP.BackColor = System.Drawing.Color.Gray
        Me.CPContextMenuBar.SetContextMenuEx(Me.picCP, Me.DAButtonItem1)
        Me.picCP.Dock = System.Windows.Forms.DockStyle.Fill
        Me.picCP.Image = Nothing
        Me.picCP.Location = New System.Drawing.Point(307, 0)
        Me.picCP.Name = "picCP"
        Me.picCP.Size = New System.Drawing.Size(802, 400)
        Me.picCP.TabIndex = 44
        Me.picCP.ViewMode = iViewCore.PictureBox.EViewMode.FitImage
        '
        'Bar
        '
        Me.Bar.AccessibleDescription = "DotNetBar Bar (Bar)"
        Me.Bar.AccessibleName = "DotNetBar Bar"
        Me.Bar.AccessibleRole = System.Windows.Forms.AccessibleRole.StatusBar
        Me.Bar.BarType = DevComponents.DotNetBar.eBarType.StatusBar
        Me.Bar.CanUndock = False
        Me.Bar.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Bar.DockSide = DevComponents.DotNetBar.eDockSide.Document
        Me.Bar.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Bar.GrabHandleStyle = DevComponents.DotNetBar.eGrabHandleStyle.ResizeHandle
        Me.Bar.IsMaximized = False
        Me.Bar.Items.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnPrintImage, Me.btnZoomIn, Me.btnZoomOut, Me.btnViewMode, Me.sldrZoom})
        Me.Bar.Location = New System.Drawing.Point(307, 400)
        Me.Bar.Name = "Bar"
        Me.Bar.Size = New System.Drawing.Size(802, 60)
        Me.Bar.Stretch = True
        Me.Bar.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.Bar.TabIndex = 43
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
        'btnZoomIn
        '
        Me.btnZoomIn.AutoCollapseOnClick = False
        Me.btnZoomIn.BeginGroup = True
        Me.btnZoomIn.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnZoomIn.Image = CType(resources.GetObject("btnZoomIn.Image"), System.Drawing.Image)
        Me.btnZoomIn.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far
        Me.btnZoomIn.Name = "btnZoomIn"
        '
        'btnZoomOut
        '
        Me.btnZoomOut.AutoCollapseOnClick = False
        Me.btnZoomOut.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText
        Me.btnZoomOut.Image = CType(resources.GetObject("btnZoomOut.Image"), System.Drawing.Image)
        Me.btnZoomOut.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far
        Me.btnZoomOut.Name = "btnZoomOut"
        '
        'btnViewMode
        '
        Me.btnViewMode.AutoCollapseOnClick = False
        Me.btnViewMode.AutoExpandOnClick = True
        Me.btnViewMode.BeginGroup = True
        Me.btnViewMode.Image = CType(resources.GetObject("btnViewMode.Image"), System.Drawing.Image)
        Me.btnViewMode.Name = "btnViewMode"
        Me.btnViewMode.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnActualSize, Me.btnFitImage, Me.btnFitWidth, Me.btnFitHeight})
        Me.btnViewMode.Text = "Fit"
        '
        'btnActualSize
        '
        Me.btnActualSize.Name = "btnActualSize"
        Me.btnActualSize.Text = "Actual Size"
        '
        'btnFitImage
        '
        Me.btnFitImage.Name = "btnFitImage"
        Me.btnFitImage.Text = "Fit Image"
        '
        'btnFitWidth
        '
        Me.btnFitWidth.Name = "btnFitWidth"
        Me.btnFitWidth.Text = "Fit Width"
        '
        'btnFitHeight
        '
        Me.btnFitHeight.Name = "btnFitHeight"
        Me.btnFitHeight.Text = "Fit Height"
        '
        'sldrZoom
        '
        Me.sldrZoom.AutoCollapseOnClick = False
        Me.sldrZoom.BeginGroup = True
        Me.sldrZoom.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far
        Me.sldrZoom.Maximum = 500
        Me.sldrZoom.Minimum = 25
        Me.sldrZoom.Name = "sldrZoom"
        Me.sldrZoom.Step = 5
        Me.sldrZoom.Text = "Zoom"
        Me.sldrZoom.TrackMarker = False
        Me.sldrZoom.Value = 25
        Me.sldrZoom.Width = 200
        '
        'PanelEx2
        '
        Me.PanelEx2.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.PanelEx2.Controls.Add(Me.ListContextMenuBar)
        Me.PanelEx2.Controls.Add(Me.picList)
        Me.PanelEx2.DisabledBackColor = System.Drawing.Color.Empty
        Me.PanelEx2.Dock = System.Windows.Forms.DockStyle.Left
        Me.PanelEx2.Location = New System.Drawing.Point(0, 0)
        Me.PanelEx2.Name = "PanelEx2"
        Me.PanelEx2.Size = New System.Drawing.Size(307, 460)
        Me.PanelEx2.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx2.Style.GradientAngle = 90
        Me.PanelEx2.TabIndex = 39
        '
        'ListContextMenuBar
        '
        Me.ListContextMenuBar.DockSide = DevComponents.DotNetBar.eDockSide.Left
        Me.ListContextMenuBar.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListContextMenuBar.IsMaximized = False
        Me.ListContextMenuBar.Items.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.ButtonItem3})
        Me.ListContextMenuBar.Location = New System.Drawing.Point(91, 219)
        Me.ListContextMenuBar.Name = "ListContextMenuBar"
        Me.ListContextMenuBar.Size = New System.Drawing.Size(125, 27)
        Me.ListContextMenuBar.Stretch = True
        Me.ListContextMenuBar.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.ListContextMenuBar.TabIndex = 46
        Me.ListContextMenuBar.TabStop = False
        Me.ListContextMenuBar.Text = "List Context"
        '
        'ButtonItem3
        '
        Me.ButtonItem3.AutoExpandOnClick = True
        Me.ButtonItem3.Name = "ButtonItem3"
        Me.ButtonItem3.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.btnPrintContext, Me.btnLocate})
        Me.ButtonItem3.Text = "ButtonItem1"
        Me.ButtonItem3.Visible = False
        '
        'btnPrintContext
        '
        Me.btnPrintContext.BeginGroup = True
        Me.btnPrintContext.Name = "btnPrintContext"
        Me.btnPrintContext.Text = "Print"
        '
        'btnLocate
        '
        Me.btnLocate.BeginGroup = True
        Me.btnLocate.Name = "btnLocate"
        Me.btnLocate.Text = "Locate"
        '
        'picList
        '
        Me.picList.AutoValidate = System.Windows.Forms.AutoValidate.Disable
        Me.picList.BackColor = System.Drawing.Color.Transparent
        Me.ListContextMenuBar.SetContextMenuEx(Me.picList, Me.ButtonItem3)
        Me.picList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.picList.Location = New System.Drawing.Point(0, 0)
        Me.picList.Name = "picList"
        Me.picList.Size = New System.Drawing.Size(307, 460)
        Me.picList.TabIndex = 0
        Me.picList.ThumbnailSize = New System.Drawing.Size(128, 96)
        '
        'ButtonItem6
        '
        Me.ButtonItem6.Name = "ButtonItem6"
        Me.ButtonItem6.Text = "Actual Size"
        '
        'ButtonItem7
        '
        Me.ButtonItem7.Name = "ButtonItem7"
        Me.ButtonItem7.Text = "Fit Image"
        '
        'ButtonItem8
        '
        Me.ButtonItem8.Name = "ButtonItem8"
        Me.ButtonItem8.Text = "Fit Width"
        '
        'ButtonItem9
        '
        Me.ButtonItem9.Name = "ButtonItem9"
        Me.ButtonItem9.Text = "Fit Height"
        '
        'ButtonItem11
        '
        Me.ButtonItem11.Name = "ButtonItem11"
        Me.ButtonItem11.Text = "Vertically"
        '
        'ButtonItem12
        '
        Me.ButtonItem12.Name = "ButtonItem12"
        Me.ButtonItem12.Text = "Horizontally"
        '
        'ButtonItem14
        '
        Me.ButtonItem14.Name = "ButtonItem14"
        Me.ButtonItem14.Text = "180°"
        '
        'ButtonItem15
        '
        Me.ButtonItem15.Name = "ButtonItem15"
        Me.ButtonItem15.Text = "90° Clockwise"
        '
        'ButtonItem16
        '
        Me.ButtonItem16.Name = "ButtonItem16"
        Me.ButtonItem16.Text = "90° Anti Clockwise"
        '
        'FrmViewChancePrints
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1109, 494)
        Me.Controls.Add(Me.PanelEx1)
        Me.Controls.Add(Me.Bar2)
        Me.DoubleBuffered = True
        Me.EnableGlass = False
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "FrmViewChancePrints"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Chance Prints"
        Me.TitleText = "<b>Chance Prints</b>"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.Bar2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelEx1.ResumeLayout(False)
        CType(Me.CPContextMenuBar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Bar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelEx2.ResumeLayout(False)
        CType(Me.ListContextMenuBar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Bar2 As DevComponents.DotNetBar.Bar
    Friend WithEvents lblSOCNumber As DevComponents.DotNetBar.LabelItem
    Friend WithEvents PanelEx1 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents PanelEx2 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents picList As iViewCore.PictureList
    Friend WithEvents lblFileName As DevComponents.DotNetBar.LabelItem
    Friend WithEvents prgBar As DevComponents.DotNetBar.ProgressBarItem
    Friend WithEvents Bar As DevComponents.DotNetBar.Bar
    Friend WithEvents btnPrintImage As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnZoomIn As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnZoomOut As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnViewMode As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnActualSize As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnFitImage As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnFitWidth As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnFitHeight As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents sldrZoom As DevComponents.DotNetBar.SliderItem
    Friend WithEvents picCP As iViewCore.PictureBox
    Friend WithEvents CPContextMenuBar As DevComponents.DotNetBar.ContextMenuBar
    Friend WithEvents DAButtonItem1 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ButtonItem5 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnActualSizeContext As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnFitImageContext As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnFitWidthContext As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnFitHeightContext As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ButtonItem1 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnFlipVertically As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnFlipHorizontally As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ButtonItem2 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnRotate180 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnRotate90Right As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnRotate90Left As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnInvertColors As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnSaveImage As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnReset As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents ListContextMenuBar As DevComponents.DotNetBar.ContextMenuBar
    Friend WithEvents ButtonItem3 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnPrintContext As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnLocate As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ButtonItem6 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ButtonItem7 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ButtonItem8 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ButtonItem9 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ButtonItem11 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ButtonItem12 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ButtonItem14 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ButtonItem15 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents ButtonItem16 As DevComponents.DotNetBar.ButtonItem
    Friend WithEvents btnPrintPicContext As DevComponents.DotNetBar.ButtonItem
End Class
